using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Game
    {
        Func<FieldPoint> _methodGetMove;
        public Field _field;
        public ControlRule _rule;
        public IEnumerator _turn;
        public (ChessPiece, TypeMove[,]) _active;
        private Dictionary<ChessPiece, List<(FieldPoint, TypeMove)>> _allMovesPoints;
        public IUI _UI;
        Bot _bot;
        bool _autoRePlay = false;
        public Dictionary<PlayerSide, int> RelativeValueCount = new Dictionary<PlayerSide, int>()
        {
            {PlayerSide.First, 0},
            {PlayerSide.Second, 0}
        };


        public Dictionary<PlayerSide, PlayerType> _playersType { get;} = new Dictionary<PlayerSide, PlayerType>()
        {
            {PlayerSide.First, PlayerType.Human},
            {PlayerSide.Second, PlayerType.Human}
        };

        public Game(Field f)
        {
            _field = f;
            _rule = new ControlRule(this);
             _bot = new Bot();

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
                RelativeValueCount[PlayerSide.First] = 0;
                RelativeValueCount[PlayerSide.Second] = 0;

                _rule.SecurityCheckAll();
                Outcome outcome = allPossibleMoves();

                if (outcome != Outcome.Сontinue)
                {
                    _UI.FieldRender();
                    End(outcome);
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

            if (_bot != null)
            {
                _bot.MoveAdd(_active.Item1._p, p);

            }

            _rule.RestartEnPassent();


            FieldPoint beofreMovePoint = _active.Item1._p;

            switch (_active.Item2[p.y, p.x])
            {
                case TypeMove.Simple:
                    Move(_active.Item1, p);
                    _UI.SimpleMove(beofreMovePoint, _active.Item1, p);
                    break;

                case TypeMove.Attack:
                    Move(_active.Item1, p);
                    _UI.Attack(beofreMovePoint, _active.Item1,p, targetChP.ChPType);
                    break;

                case TypeMove.Сastling:
                    Сastling(_active.Item1, p);
                    _UI.Сastling(beofreMovePoint, _active.Item1, p);
                    break;

                case TypeMove.EnPassant:
                    EnPassant(_active.Item1, p);
                    _UI.Attack(beofreMovePoint, _active.Item1, p, ChPType.Pawn);
                    break;

            }

            if (_active.Item1.ChPType == ChPType.Pawn)
            {
                Pawn pawn = (Pawn)_active.Item1;
                if (_rule.PromotionAccess(pawn))
                {
                    _UI.FieldRender();
                    Promotion(pawn);

                }
                else
                {
                    _rule.ChecklongPawnMove(pawn, beofreMovePoint, p);

                }
            }

            return true;
          

        }

        private void Move(ChessPiece chP, FieldPoint MovePoint)
        {
            _field.SetChP(chP._p, null);
            _field.SetChP(MovePoint, chP);
        }

        private void Сastling(ChessPiece king, FieldPoint kingMovePoint)
        {
            int shift = _rule.ShiftRelativeRook(king,kingMovePoint);

            ChessPiece rook = _field.GetChP(new FieldPoint(king._p.y, king._p.x + shift));

              Move(rook, new FieldPoint(king._p.y, king._p.x + Math.Sign(shift)));
              Move(king, kingMovePoint);


        }
        private void EnPassant(ChessPiece thisPawn, FieldPoint pawnMovePoint)
        {
            FieldPoint targetPawnPoint = new FieldPoint(thisPawn._p.y,pawnMovePoint.x);

            _field.SetChP(targetPawnPoint, null);
            Move(thisPawn, pawnMovePoint);



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

                    //данным ходом можно атаковать
                    else if (type != TypeMove.Simple)
                    {
                        //на клетке есть вражеская фигура
                        if (cellChP != null && thisChP.Side != cellChP.Side)
                        {
                            result = TypeMove.Attack;
                        }
                        //фигура(пешка) может выполнить взятие на проходе
                        else if (_rule.EnPassentAccess(thisChP,p))
                        {
                            result = TypeMove.EnPassant;
                        }

                        else
                            break;
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

        private Outcome allPossibleMoves()
        {
            _allMovesPoints = new Dictionary<ChessPiece, List<(FieldPoint, TypeMove)>>();
            int MovesCount = 0;

            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _field.GetChP(new FieldPoint(y, x));
                    if (chP != null && chP.Side == (PlayerSide)_turn.Current)
                    {
                        _allMovesPoints[chP] = EditMoves(chP, chP.GetMoves());
                        //подсчет фигур которые имеют ходы (само количество ходов не считается)
                        MovesCount += _allMovesPoints[chP].Count;
                        RelativeValueCount[chP.Side] += chP.RelativeValue;
                    }

                }

            }

            if (MovesCount > 0)

                if (RelativeValueCount[PlayerSide.First] > 3 || RelativeValueCount[PlayerSide.Second] > 3)
                {
                    return Outcome.Сontinue;
                }
                else
                {
                    return Outcome.Draw;
                }
            else
            {
                if (_rule.IsCheckmate())
                {
                    return Outcome.Victory;
                }
                else
                {
                    return Outcome.Draw;
                }

            }

        }

        private void Promotion(Pawn pawn)
        {
            ChPType typeChoice;
            if (_playersType[(PlayerSide)_turn.Current] == PlayerType.Human)
            {
                typeChoice = _UI.Promotion();

            }
            else
            {
                typeChoice = _bot.PromotionSet();
            }


            Func<FieldPoint, PlayerSide, ChessPiece> chPClass = Data.ChPTypeToFuncClass[typeChoice];
            ChessPiece newChP = chPClass(pawn._p,pawn.Side);
           _field.SetChP(pawn._p, newChP);

            if (_bot != null)
            {
                _bot.PromotionAdd(newChP.Sign);
            }


        }

        private void End(Outcome outcome)
        {
            if (_autoRePlay)
            {
                Start();
            }
            else
            {
                switch (outcome)
                {
                    case Outcome.Victory:
                        _turn.MoveNext();
                        PlayerSide victorySide = (PlayerSide)_turn.Current;
                        _UI.Victory(victorySide);
                        break;

                    case Outcome.Draw:
                        _UI.Draw();
                        break;
                }

            }

        }


        public void PlayerTypeChange(PlayerSide side, PlayerType type)
        {
            _playersType[side] = type;
        }

    }

}
