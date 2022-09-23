using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class RuleControl
    {
        private (ChessPiece, TypeMove[,]) _activeChP = (null, new TypeMove[Field.maxY, Field.maxX]);
        private bool[,] _unsafeCell = new bool[Field.maxY, Field.maxX];
        private Dictionary<ChessPiece, List<Point>> _protectKing;
        private List<List<Point>> _checkLines;
        private Game _game;


        public RuleControl(Game game )
        {
            _game = game;
        }
        ///определяет, какие ходы опасны для короля, какие фигуры защищают короля(и их передвижение ограничено), и какие фигуры поставили шах
        private void SecurityCheckChP(ChessPiece thisChP, IEnumerable<IEnumerable<(Point, TypeMove)>> list)
        {
            foreach (var line in list)
            {
                bool check = false;
                (ChessPiece, List<Point>) interval = (null, new List<Point>());
                interval.Item2.Add(thisChP._p);
                foreach (var i in line)
                {
                    Point p = i.Item1;
                    TypeMove type = i.Item2;
                    ChessPiece cellChP = _game._field.GetChP(p);

                    if (cellChP == null && type != TypeMove.Attack) //все случаи, когда на клетке нету фигуры и можно походить без атаки
                    {
                        if (interval.Item1 == null)
                        {
                            _unsafeCell[p.y, p.x] = true;

                        }
                        if (!check)
                        {
                            interval.Item2.Add(p);
                        }
                        continue;
                    }

                    else if (cellChP != null) //все случаи, когда на клетке есть фигура
                    {
                        if (type != TypeMove.Simple)//данным ходом можно атаковать
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
                                        _checkLines.Add(interval.Item2);//добавление отрезков между угрожающей фигурой(включительно) и королем
                                    }

                                }

                                else if (interval.Item1 == null && !check)
                                {
                                    interval.Item1 = cellChP;
                                }
                                else
                                    break;
                            }

                            else
                            {
                                _unsafeCell[p.y, p.x] = true;//учет не безопасных для короля клеток
                                break;
                            }

                        }

                        else
                            break;

                    }

                }

            }

        }

        public void SecurityCheckAll()//проверка всех вражеских фигур на предмет опасности для короля
        {
            _protectKing = new Dictionary<ChessPiece, List<Point>>();
            _checkLines = new List<List<Point>>();
            _unsafeCell = new bool[Field.maxY, Field.maxX];
            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _game._field.GetChP(new Point(y, x));
                    if (chP != null && chP.Side != (PlayerSide)_game._turn.Current)
                    {
                        SecurityCheckChP(chP, chP.GetMoves());
                    }

                }

            }
        }

        public bool AccessChP(ChessPiece chP)//определяет, есть ли фигура на клетке и соответствует ли она очереди
        {
            PlayerSide side = (PlayerSide)_turn.Current;

            if (chP != null && chP.Side == side)
            {
                return true;

            }

            else
            {
                return false;
            }


        }
        public bool AccessCell(Point p, ChessPiece chP)//определяет безопасен ли данный ход для короля
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


        private bool InInterval(Point p, List<Point> interval)
        {
            foreach (Point i in interval)
            {
                if (p == i)
                {
                    return true;
                }

            }
            return false;


        }
    }
}
