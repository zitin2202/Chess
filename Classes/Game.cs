using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Game
    {
        private Field _f;
        private (ChessPiece,TypeMove[,]) _activeChP = (null,new TypeMove[Field.maxY,Field.maxX]);
        private IEnumerator _turn = TurnToGo();
        private bool _check = false;
        private Dictionary<ChessPiece, List<Point>> _protectKing;
        private List<List<Point>> _checkLines;
        private bool[,] _unsafeCell = new bool[Field.maxY, Field.maxX];
        public Game(Field f)
        {
            _f = f;
        }

        public bool Start(Point p)
        {
            while
        }

        public bool Select(Point p)
        {

            ChessPiece chP = _f.GetChP(p);
            _turn.MoveNext();

            //if (!AccessChP(chP))
            //{
            //    Console.WriteLine("Здесь нету фигуры, которую вы могли бы взять");
            //    return false;
            //}

            //else
            //{
            //    Console.WriteLine($"Очередь игрока: {_turn.Current}");

            //}


            _activeChP.Item1 = chP;

            Console.WriteLine((chP.Side, chP.ChPType));

            var list = chP.GetMoves();

            foreach (var line in list)
            {
                foreach (var i in line)
                {
                    _activeChP.Item2[i.Item1.y, i.Item1.x] = i.Item2;
                    ChessPiece targetChP = _f.GetChP(new Point(i.Item1.y, i.Item1.x));
                    Console.WriteLine($"{(i.Item1.y, i.Item1.x)}: {((targetChP == null ? "Пусто" : targetChP.ChPType.ToString()), _activeChP.Item2[i.Item1.y, i.Item1.x])}");
                }

            }

            return true;

        }

        public bool Action(Point p)
        {

            if (_activeChP.Item1 == null || _activeChP.Item2[p.y,p.x] == 0)
            {
                Console.WriteLine("Вы не можете походить сюда");
                return false;

            }

            ChessPiece chP = _f.GetChP(p);

            switch (_activeChP.Item2[p.y, p.x])
            {
                case TypeMove.Simple:
                    Move(_activeChP.Item1,p);
                    break;

                case TypeMove.Attack:
                    Attack(_activeChP.Item1,_f.GetChP(p));
                    break;

            }
            _f.SetChP(_activeChP.Item1._p, null);
            _f.SetChP(p, _activeChP.Item1);

            _activeChP = (null, new TypeMove[Field.maxY, Field.maxX]);

            return true;
          

        }

        private void EditMoves(ChessPiece thisChP,IEnumerable<IEnumerable<(Point, TypeMove)>> list, bool afterStop=false)
        {
            foreach (var line in list)
            {
                bool check = false;
                (ChessPiece,List<Point>) interval = (null,new List<Point>());
                interval.Item2.Add(thisChP._p);
                foreach (var i in line)
                {
                    Point p = i.Item1;
                    TypeMove type = i.Item2;
                    ChessPiece cellChP = _f.GetChP(p);
                    TypeMove result = 0;

                    if (cellChP == null && type != TypeMove.Attack) //все случаи, когда на клетке нету фигуры и можно походить без атаки
                    {

                        if (afterStop) //если это пвседоход
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

                        else 
                            result = TypeMove.Simple;


                    }

                    else if (cellChP != null) //все случаи, когда на клетке есть фигура
                    {
                        if (type != TypeMove.Simple)//данным ходом можно атаковать
                        {
                            if (thisChP.Side != cellChP.Side) //фигура вражеская
                            {
                                if (afterStop)
                                {

                                    if (cellChP.ChPType == ChPType.King)
                                    {
                                        if (interval.Item1 != null)
                                        {
                                            _protectKing[interval.Item1] = interval.Item2;
                                            break;
                                        }
                                        else
                                        {
                                            _unsafeCell[p.y, p.x] = true;
                                            _checkLines.Add(interval.Item2);
                                            check = true;
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
                                    result = TypeMove.Attack;


                            }

                            else if (afterStop)
                            {
                                _unsafeCell[p.y, p.x] = true;
                                break;
                            }

                        }

                        else
                            break;


                    }

                    if (!afterStop)
                    {
                        _activeChP.Item2[p.y, p.x] = result;
                        if (result == TypeMove.Attack)
                            break;

                    }

                }

            }

        }

        private void Move(ChessPiece myChP, Point targP)
        {
            Console.WriteLine($"{myChP.Side} {(myChP._p.y, myChP._p.x)}: {myChP.ChPType} идет на {(targP.y, targP.x)}");

        }
        private void Attack(ChessPiece myChP, ChessPiece targChP)
        {
            Console.WriteLine($"{myChP.Side} {(myChP._p.y, myChP._p.x)}: {myChP.ChPType} съел {targChP.ChPType} на {(targChP._p.y, targChP._p.x)}");

        }


        static private IEnumerator TurnToGo ()
        {
            while (true)
            {
                yield return PlayerSide.First;
                yield return PlayerSide.Second;
            }



        }

        private bool AccessChP(ChessPiece chP)
        {
            PlayerSide side = (PlayerSide)_turn.Current;

            if ( chP != null && chP.Side == side)
            {
                if(!_check)
                    return true;

                else
                {

                }
            }

            else
            {
                return false;
            }

            return true;

        }

    }

}
