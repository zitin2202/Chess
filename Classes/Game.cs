﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Game
    {
        public delegate void FieldPointDelegate(FieldPoint beforePoint, FieldPoint afterPoint);
        public event FieldPointDelegate ChessPieceSetEvent;

        public delegate void PromotionDelegate(string promotionChess);
        public event PromotionDelegate PromotionEvent;
        Func<FieldPoint> _methodGetMove;
        public Field _field;
        public ControlRule _rule;
        public IEnumerator _turn;
        public (ChessPiece, TypeMove[,]) _active;
        private Dictionary<ChessPiece, List<(FieldPoint, TypeMove)>> _allMovesPoints;
        public IUI _UI;
        Bot _bot;

        Dictionary<PlayerSide, PlayerType> _playersType = new Dictionary<PlayerSide, PlayerType>()

        {
            {PlayerSide.First, PlayerType.Human},
            {PlayerSide.Second, PlayerType.PC}
        };

        public Game(Field f)
        {
            _field = f;
            _rule = new ControlRule(this);

            if (_playersType.ContainsValue(PlayerType.PC))
            {
                _bot = new Bot();
                ChessPieceSetEvent += _bot.MoveAdd;
                PromotionEvent += _bot.PromotionAdd;
            }

        }

        public void Start()
        {
            if (_bot!=null)
            {
                _bot._moves = "";
            }
            _field.FieldReset();
            _rule = new ControlRule(this);
            _turn = TurnToGo();

            while (true)
            {
                _turn.MoveNext();
                _rule.SecurityCheckAll();
                if (!allPossibleMoves())
                {
                    _UI.FieldRender();
                    Victory();
                    break;
                }
                _active = (null, new TypeMove[Field.maxY, Field.maxX]);
                _UI.FieldRender();
                _UI.TurnReport();


                FieldPoint p;
                if (_playersType[(PlayerSide)_turn.Current] == PlayerType.Human)
                {
                    _methodGetMove = _UI.СellSelection;

                }
                else
                {
                    _methodGetMove = _bot.СhooseChpAndMove();
                }

                p = _methodGetMove();

                while (_active.Item2[p.y, p.x] == 0)
                {
                    while (!Select(p))
                    {
                        p = _methodGetMove();

                    }

                    p = _methodGetMove();

                }
               
                while (!Action(p))
                {

                }

            }

        }

        public bool Select(FieldPoint p)//выбор фигуры
        {
            _active = (null, new TypeMove[Field.maxY, Field.maxX]);
            ChessPiece chP = _field.GetChP(p);
            if (!_rule.AccessChP(chP))
            {
                _UI.NotChessPieceReport();
                return false;

            }
          


            _UI.SelectedСhessPiece(chP);

            if (_allMovesPoints[chP].Count>0)
            {
                _active.Item1 = chP;
                foreach (var i in _allMovesPoints[chP])
                {
                    int y = i.Item1.y;
                    int x = i.Item1.x;

                    _active.Item2[y,x] = i.Item2;
                    _UI.PossibleMove(i.Item1,i.Item2);
                }
            }

            else
            {
                _UI.NotChessМoveReport();
                return false;

            }

            return true;

        }

        public bool Action(FieldPoint p)//перемещение фигуры
        {

            if (_active.Item2[p.y,p.x] == 0)
            {
                _UI.HaventSuchMove();
                return false;

            }

            ChessPiece targetChP = _field.GetChP(p);

            if (ChessPieceSetEvent != null)
            {
                ChessPieceSetEvent(_active.Item1._p, p);

            }

            switch (_active.Item2[p.y, p.x])
            {
                case TypeMove.Simple:
                    Move(_active.Item1, p);
                    _UI.SimpleMove(_active.Item1,p);
                    break;

                case TypeMove.Attack:
                    Move(_active.Item1, p);
                    _UI.Attack(_active.Item1,p, targetChP);
                    break;

                case TypeMove.Сastling:
                    Сastling(_active.Item1, p);
                    break;

            }

            return true;
          

        }

        private void Move(ChessPiece chP, FieldPoint p)
        {
            _field.SetChP(chP._p, null);
            _field.SetChP(p, chP);
            if (chP.ChPType == ChPType.Pawn && _rule.PawnTransformationAccess(chP))
            {
                _UI.FieldRender();
                Promotion(chP);

            }
        }

        private void Сastling(ChessPiece king, FieldPoint p)
        {
            int shift = _rule.ShiftRelativeRook(king,p);
            ChessPiece rook;
            
            rook = _field.GetChP(new FieldPoint(king._p.y, king._p.x + shift));

            _field.SetChP(rook._p, null);
            _field.SetChP(new FieldPoint(king._p.y, king._p.x + Math.Sign(shift)), rook);
            _field.SetChP(king._p, null);
            _field.SetChP(p, king);

        }


        private List<(FieldPoint, TypeMove)> EditMoves(ChessPiece thisChP, IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> list)//добавление учета других фигур на доске в передвижениях фигуры
        {
            List<(FieldPoint, TypeMove)> movesPoints = new List<(FieldPoint,TypeMove)>();
            foreach (var line in list)
            {
                foreach (var i in line)
                {
                    FieldPoint p = i.Item1;
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

                    if (result!=0 && _rule.AccessCell(thisChP, p))
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
            _allMovesPoints = new Dictionary<ChessPiece, List<(FieldPoint, TypeMove)>>();
            int count = 0;

            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _field.GetChP(new FieldPoint(y, x));
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

        private void Promotion(ChessPiece chP)
        {
            Type classChP;
            if (_playersType[(PlayerSide)_turn.Current] == PlayerType.Human)
            {
                classChP = _UI.Promotion();

            }
            else
            {
                classChP = _bot.PromotionSet();
            }

            if (PromotionEvent!=null)
            {
                PromotionEvent(Data.ChPClassToStr[classChP]);
            }
            ChessPiece newChP = (ChessPiece)Activator.CreateInstance(classChP, chP._p,chP.Side);
           _field.SetChP(chP._p, newChP);


        }

        private void Victory()
        {
            _turn.MoveNext();
            PlayerSide victorySide = (PlayerSide)_turn.Current;
            _UI.Victory(victorySide);
        }


    }

}
