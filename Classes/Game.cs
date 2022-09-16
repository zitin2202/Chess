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
        private Dictionary<ChessPiece, List<Point>> _checkPoints; //Используется во время шаха. Клетки на шахматной доске, которые фигура должна пройти, чтобы дойти до короля.
        
        public Game(Field f)
        {
            _f = f;
        }

        public bool Select(Point p)
        {

            ChessPiece chP = _f.GetChP(p);

            if (!AccessChP(chP))
            {
                Console.WriteLine("Здесь нету фигуры, которую вы могли бы взять");
                return false;
            }

            _turn.MoveNext();
            Console.WriteLine($"Очередь игрока: {_turn.Current}");


            _activeChP.Item1 = chP;

            Console.WriteLine((chP.Side, chP.ChPType));

            var list = chP.GetMoves();

            foreach (var i in list)
            {
                _activeChP.Item2[i.Item1.y, i.Item1.x] = i.Item2;

                ChessPiece targetChP = _f.GetChP(new Point(i.Item1.y, i.Item1.x));
                Console.WriteLine($"{(i.Item1.y,i.Item1.x)}: {((targetChP == null ? "Пусто": targetChP.ChPType.ToString()),_activeChP.Item2[i.Item1.y, i.Item1.x])}");
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
