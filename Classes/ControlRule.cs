using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class ControlRule
    {
        private Game _game;
        public bool[,] _unsafeCell = new bool[Field.maxY, Field.maxX];
        private Dictionary<ChessPiece, List<FieldPoint>> _protectKing;
        private List<List<FieldPoint>> _checkLines;
        private FieldPoint _enPassentCell;


        public ControlRule(Game game )
        {
            _game = game;
        }
        ///определяет, какие ходы опасны для короля, какие фигуры защищают короля(и их передвижение ограничено), и какие фигуры поставили шах
        private void SecurityCheckChP(ChessPiece thisChP, IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> list)
        {
            foreach (var line in list)
            {
                bool check = false;
                (ChessPiece, List<FieldPoint>) interval = (null, new List<FieldPoint>());
                interval.Item2.Add(thisChP._p);
                foreach (var i in line)
                {
                    FieldPoint p = i.Item1;
                    TypeMove type = i.Item2;
                    ChessPiece cellChP = _game._field.GetChP(p);

                    if (type == TypeMove.Simple || type == TypeMove.Сastling) //если нельзя атаковать
                        break;

                    if (cellChP == null) //все случаи, когда на клетке нету фигуры и ходом можно атаковать
                    {
                        if (interval.Item1 == null)
                        {
                            _unsafeCell[p.y, p.x] = true;

                        }
                        if (!check)
                        {
                            interval.Item2.Add(p);
                        }
                    }

                    else if (cellChP != null) //все случаи, когда на клетке есть фигура и ходом можно атаковать
                    {
                        if (thisChP.Side != cellChP.Side) //фигура вражеская
                        {
                            if (cellChP.ChPType == ChPType.King)
                            {
                                if (interval.Item1 != null)//между атакующей фигурой и королем есть еще одная фигура, защищающая короля
                                {
                                    _protectKing[interval.Item1] = interval.Item2;//сохранение защищающей фигуры с интервалом, в котором она может передвигаться
                                    break;
                                }
                                else //непосредственная угроза королю
                                {
                                    check = true;
                                    _unsafeCell[p.y, p.x] = true;
                                    _checkLines.Add(interval.Item2);//добавление отрезков между угрожающей фигурой(включительно) и королем
                                }

                            }

                            else if (interval.Item1 == null && !check)
                            {
                                _unsafeCell[p.y, p.x] = true;
                                interval.Item1 = cellChP;
                            }
                            else
                                break;
                        }

                        else
                        {
                            if (interval.Item1 == null)
                            {
                                _unsafeCell[p.y, p.x] = true; //учет небезопасных для короля клеток
                            }
                            break;
                        }

                    }

                }

            }

        }

        public void SecurityCheckAll()//проверка всех вражеских фигур на предмет опасности для короля
        {
            _protectKing = new Dictionary<ChessPiece, List<FieldPoint>>();
            _checkLines = new List<List<FieldPoint>>();
            _unsafeCell = new bool[Field.maxY, Field.maxX];
            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _game._field.GetChP(new FieldPoint(y, x));
                    if (chP != null && chP.Side != (PlayerSide)_game._turn.Current)
                    {
                        SecurityCheckChP(chP, chP.GetMoves());
                    }

                }

            }
        }

        public bool AccessChP(ChessPiece chP)//определяет, есть ли фигура на клетке и соответствует ли она очереди
        {
            PlayerSide side = (PlayerSide)_game._turn.Current;

            if (chP != null && chP.Side == side)
            {
                return true;

            }

            else
            {
                return false;
            }


        }
        public bool AccessCell(ChessPiece chP, FieldPoint p)//определяет безопасен ли данный ход для короля
        {
            bool access = true;
            int lenCheckLines = (_checkLines != null ? _checkLines.Count : 0);


            if (chP.ChPType == ChPType.King)//фигура - король
            {
                access = !_unsafeCell[p.y, p.x];//клетка безопасна
            }

            else if (lenCheckLines < 2)//меньше двух шахов
            {
                if (lenCheckLines == 1)//один шах
                {
                    access = InInterval(p, _checkLines[0]);//если между королем и урожающей фигурой(включительно) есть данная клетка, то true
                }

                else if (_protectKing != null && _protectKing.ContainsKey(chP))//защищает ли данная фигура короля
                {
                    access = InInterval(p, _protectKing[chP]);//если между королем и урожающей фигурой(включительно) есть данная клетка, то true
                }


            }

            else//фигура не является королем и шахов больше двух, то сделать она ничего не сможет
                access = false;


            return access;

        }


        public bool AccessCastling(ChessPiece king, FieldPoint targetP)
        {
            int shift = ShiftRelativeRook(king, targetP);

            int direction = Math.Sign(shift);

            ChessPiece chPCastling = _game._field.GetChP(new FieldPoint(king._p.y, king._p.x + shift));
            if (chPCastling != null && chPCastling.ChPType == ChPType.Rook && king.StartPosition && chPCastling.StartPosition && _checkLines.Count == 0)
            {
                FieldPoint cell;
                for (int i = 1; i < Math.Abs(shift); i++)
                {
                    cell = new FieldPoint(king._p.y, king._p.x + i * direction);
                    if (_game._field.GetChP(cell)!=null|| (i<3 && _unsafeCell[cell.y,cell.x]))
                    {
                        return false;
                    }
                }

                return true;

            }

            return false;

        }

        public int ShiftRelativeRook(ChessPiece king, FieldPoint targetP)
        {
            int shift;

            if (targetP.x > king._p.x)
            {
                shift = 3;
            }
            else
            {
                shift = -4;
            }

            return shift;
        }


        private bool InInterval(FieldPoint p, List<FieldPoint> interval)
        {
            foreach (FieldPoint i in interval)
            {
                if (p == i)
                {
                    return true;
                }

            }
            return false;


        }

        public bool PromotionAccess(Pawn pawn)
        {
            int requiredLine = (pawn.Side == PlayerSide.First ? 0 : Field.maxY - 1);
            return (pawn._p.y == requiredLine ? true : false);
        }

        public void ChecklongPawnMove(Pawn pawn,FieldPoint beforeMovePoint, FieldPoint afterMovePoint)
        {
            if (Math.Abs(beforeMovePoint.y - afterMovePoint.y) == 2)
            {
                int yBackCell = beforeMovePoint.y + pawn.yDirect;
                _enPassentCell = new FieldPoint(yBackCell, beforeMovePoint.x);
            }
        }

        public void RestartEnPassent()
        {
            _enPassentCell = null;
        }

        public bool EnPassentAccess(ChessPiece chP, FieldPoint targetP)
        {
            if (chP.ChPType == ChPType.Pawn && targetP == _enPassentCell)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

    }
}
