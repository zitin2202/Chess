using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Game
    {
        public Field _field;
        public ControlRule _rule;
        public IEnumerator _turn = TurnToGo();
        public (ChessPiece, TypeMove[,]) _activeChP = (null, new TypeMove[Field.maxY, Field.maxX]);
        private Dictionary<ChessPiece, List<(Point, TypeMove)>> _allMovesPoints;
        private IUI UI;

        public Game(Field f)
        {
            _field = f;
            _rule = new ControlRule(this);
            UI = new ConsoleUI(this);
        }

        public void Start()
        {
            _field.DeafultField();
            while (true)
            {
                _turn.MoveNext();

                _rule.SecurityCheckAll();
                if (!allPossibleMoves())
                {
                    UI.FieldRender();
                    Victory();
                    break;
                }
                _activeChP = (null, new TypeMove[Field.maxY, Field.maxX]);
                UI.FieldRender();
                UI.TurnReport();
                while (!Select(UI.СellSelection()))
                {

                }

                while (!Action(UI.СellSelection()))
                {

                }
            }

        }

        public bool Select(Point p)//выбор фигуры
        {
            ChessPiece chP = _field.GetChP(p);
            if (!_rule.AccessChP(chP))
            {
                UI.NotChessPieceReport();
                return false;

            }
          


            UI.SelectedСhessPiece(chP);

            if (_allMovesPoints[chP].Count>0)
            {
                _activeChP.Item1 = chP;
                foreach (var i in _allMovesPoints[chP])
                {
                    int y = i.Item1.y;
                    int x = i.Item1.x;

                    _activeChP.Item2[y,x] = i.Item2;
                    UI.PossibleMove(i.Item1,i.Item2);
                }
            }

            else
            {
                UI.NotChessМoveReport();
                return false;

            }

            return true;

        }

        public bool Action(Point p)//перемещение фигуры
        {
            var active = _activeChP;

            if (active.Item2[p.y,p.x] == 0)
            {
                UI.HaventSuchMove();
                return false;

            }

            ChessPiece targetChP = _field.GetChP(p);

            switch (active.Item2[p.y, p.x])
            {
                case TypeMove.Simple:
                    Move(active.Item1, p);
                    UI.SimpleMove(active.Item1,p);
                    break;

                case TypeMove.Attack:
                    Move(active.Item1, p);
                    UI.Attack(active.Item1,p, targetChP);
                    break;

                case TypeMove.Сastling:
                    Сastling(active.Item1, p);
                    break;

            }
            


            return true;
          

        }

        private void Move(ChessPiece chP, Point p)
        {
            _field.SetChP(chP._p, null);
            _field.SetChP(p, chP);
            if (chP.ChPType == ChPType.Pawn && _rule.PawnTransformationAccess(chP))
            {
                UI.FieldRender();
                PawnTransformation(chP);

            }
        }

        private void Сastling(ChessPiece king, Point p)
        {
            int shift = _rule.CastlingShift(king,p);
            ChessPiece rook;
            
            rook = _field.GetChP(new Point(king._p.y, king._p.x + shift));

            _field.SetChP(rook._p, null);
            _field.SetChP(new Point(king._p.y, king._p.x + Math.Sign(shift)), rook);
            _field.SetChP(king._p, null);
            _field.SetChP(p, king);

        }


        private List<(Point, TypeMove)> EditMoves(ChessPiece thisChP, IEnumerable<IEnumerable<(Point, TypeMove)>> list)//добавление учета других фигур на доске в передвижениях фигуры
        {
            List<(Point, TypeMove)> movesPoints = new List<(Point,TypeMove)>();
            foreach (var line in list)
            {
                foreach (var i in line)
                {
                    Point p = i.Item1;
                    TypeMove type = i.Item2;
                    ChessPiece cellChP = _field.GetChP(p);
                    TypeMove result = 0;

                    if (i.Item2 == TypeMove.Сastling)
                    {
                        if (_rule.AccessCastling(thisChP, p))
                        {
                            result = TypeMove.Сastling;

                        }
                    }

                    else if (cellChP == null && type != TypeMove.Attack) //на клетке нету фигуры и можно походить без атаки                     
                            result = TypeMove.Simple;

                    //на клетке есть вражеская и данным ходом можно атаковать
                    else if (cellChP != null && thisChP.Side != cellChP.Side && type != TypeMove.Simple)
                    {
                        result = TypeMove.Attack;                      
                    }
                    else
                        break;

                    if (result!=0 && _rule.AccessCell(p, thisChP))
                        movesPoints.Add((p, result));

                    if (result == TypeMove.Attack)
                        break;                 

                }

            }

            return movesPoints;

        }



        static private IEnumerator<PlayerSide> TurnToGo ()//определение очереди стороны
        {
            while (true)
            {
                yield return PlayerSide.First;
                yield return PlayerSide.Second;
            }

        }

        private bool allPossibleMoves()
        {
            _allMovesPoints = new Dictionary<ChessPiece, List<(Point, TypeMove)>>();
            int count = 0;

            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _field.GetChP(new Point(y, x));
                    if (chP != null && chP.Side == (PlayerSide)_turn.Current)
                    {
                        _allMovesPoints[chP] = EditMoves(chP, chP.GetMoves());
                        count += _allMovesPoints[chP].Count;
                    }

                }

            }

            if (count > 0)
                return true;
            else
                return false;

        }

        private void PawnTransformation(ChessPiece chP)
        {
           Type classChP = UI.СhoiceChessPiece();
           ChessPiece newChP = (ChessPiece)Activator.CreateInstance(classChP, chP._p,chP.Side);
           _field.SetChP(chP._p, newChP);


        }

        private void Victory()
        {
            _turn.MoveNext();
            PlayerSide victorySide = (PlayerSide)_turn.Current;
            UI.Victory(victorySide);
        }


    }

}
