using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Game
    {
        private Field _field;
        private (ChessPiece, TypeMove[,]) _activeChP = (null, new TypeMove[Field.maxY, Field.maxX]);
        private IEnumerator _turn = TurnToGo();
        private Dictionary<ChessPiece, List<Point>> _protectKing;
        private List<List<Point>> _checkLines;
        private bool[,] _unsafeCell = new bool[Field.maxY, Field.maxX];
        public Game(Field f)
        {
            _field = f;
        }

        public void Start()
        {
            while (true)
            {
                _turn.MoveNext();
                SecurityCheckAll();
                _activeChP = (null, new TypeMove[Field.maxY, Field.maxX]);
                ConsoleFieldGUI();
                Console.WriteLine($"Очередь игрока: {_turn.Current}");
                do
                {
                    Console.WriteLine("Выберите фигуру");
                }
                while (!Select(ConsoleInput()));
                do
                {
                    Console.WriteLine("Выберите координаты");
                }
                while (!Action(ConsoleInput()));





            }
        }
        public void ConsoleFieldGUI()//временная функция вывода поля в консоли
        {
            string fieldGui = "  ";

            for (int x = 0; x < Field.maxX; x++)
            {
                fieldGui += $"{x}  ";
            }
            fieldGui += "\n";


            for (int y = 0; y < Field.maxY; y++)
            {

                for (int x = 0; x < Field.maxX; x++)
                {
                    ChessPiece chP = _field.GetChP(new Point(y, x));

                    fieldGui += $"{(x==0? $"{y} " : "")}{(chP == null ? "  " : chP.Side.ToString()[0].ToString() + chP.ChPType.ToString()[0].ToString())} ";



                }
                fieldGui += "\n";



            }
            Console.WriteLine(fieldGui);
        }



        public Point ConsoleInput()
        {
            string[] s = Console.ReadLine().Split(',');
            return new Point(int.Parse(s[0]), int.Parse(s[1]));

        }

        public bool Select(Point p)//выбор фигуры
        {
            int count = 0;
            ChessPiece chP = _field.GetChP(p);
            if (!AccessChP(chP))
            {
                Console.WriteLine("Здесь нету фигуры, которую вы могли бы взять, выберите другую");
                return false;

            }
          

            _activeChP.Item1 = chP;

            Console.WriteLine((chP.Side, chP.ChPType));

            EditMoves(chP,chP.GetMoves());

            for (int y = 0; y<Field.maxY;y++)
            {
                for (int x = 0; x <Field.maxY; x++)
                {
                    if (_activeChP.Item2[y, x] !=0 )
                    {
                        ChessPiece targetChP = _field.GetChP(new Point(y, x));
                        Console.WriteLine($"{(y, x)}: {((targetChP == null ? "Пусто" : targetChP.ChPType.ToString()), _activeChP.Item2[y, x])}");
                        count++;
                    }

                }

            }
            if (count==0)
            {
                Console.WriteLine("У этой фигуры нет возможных ходов");
                return false;

            }

            return true;

        }

        public bool Action(Point p)//перемещение фигуры
        {
            bool kingDead = true;

            if (_activeChP.Item1 == null || _activeChP.Item2[p.y,p.x] == 0)
            {
                Console.WriteLine("Вы не можете походить сюда");
                return false;

            }

            ChessPiece chP = _field.GetChP(p);

            switch (_activeChP.Item2[p.y, p.x])
            {
                case TypeMove.Simple:
                    Move(_activeChP.Item1,p);
                    break;

                case TypeMove.Attack:
                    Attack(_activeChP.Item1,p, chP);
                    kingDead = (chP.ChPType == ChPType.King ? true : false);
                    break;

            }
            _field.SetChP(_activeChP.Item1._p, null);
            _field.SetChP(p, _activeChP.Item1);


            return true;
          

        }

        private void Move(ChessPiece myChP, Point targP)
        {
            Console.WriteLine($"{myChP.Side} {(myChP._p.y, myChP._p.x)}: {myChP.ChPType} идет на {(targP.y, targP.x)}");

        }
        private void Attack(ChessPiece myChP, Point targP, ChessPiece targChP)
        {
            Console.WriteLine($"{myChP.Side} {(myChP._p.y, myChP._p.x)}: {myChP.ChPType} съел {targChP.ChPType} на {(targP.y, targP.x)}");

        }

        private void EditMoves(ChessPiece thisChP, IEnumerable<IEnumerable<(Point, TypeMove)>> list)//добавление учета других фигур на доске в передвижениях фигуры
        {
            foreach (var line in list)
            {
                foreach (var i in line)
                {
                    ChessPiece chP = _field.GetChP(i.Item1);
                    Point p = i.Item1;
                    TypeMove type = i.Item2;
                    ChessPiece cellChP = _field.GetChP(p);
                    TypeMove result = 0;


                    if (cellChP == null && type != TypeMove.Attack) //на клетке нету фигуры и можно походить без атаки                     
                            result = TypeMove.Simple;
                    
                    else if (cellChP != null) //на клетке есть фигура
                    {
                        if (thisChP.Side != cellChP.Side && type != TypeMove.Simple)//фигура вражеская и данным ходом можно атаковать
                            result = TypeMove.Attack;
                        else
                            break;
                    }

                    if (AccessCell(p, thisChP))
                        _activeChP.Item2[p.y, p.x] = result;

                    if (result == TypeMove.Attack)
                        break;                 

                }

            }

        }

        private void SecurityCheckChP(ChessPiece thisChP,IEnumerable<IEnumerable<(Point, TypeMove)>> list)//учитывает опасности для короля, для блокирования ходов
                                                                                                          //после которых король окажется под шахом из-за данной фигуры
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
                    ChessPiece cellChP = _field.GetChP(p);

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
                                            _protectKing[interval.Item1] = interval.Item2;//сохранение защищающей фигуры
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

        private void SecurityCheckAll()//проверка всех вражеских фигур на предмет опасности для короля
        {
            _protectKing = new Dictionary<ChessPiece, List<Point>>();
            _checkLines = new List<List<Point>>();
            _unsafeCell = new bool[Field.maxY, Field.maxX];
            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _field.GetChP(new Point(y, x));
                    if (chP!=null && chP.Side != (PlayerSide)_turn.Current)
                    {
                        SecurityCheckChP(chP, chP.GetMoves());
                    }

                }

            }
        }



        static private IEnumerator<PlayerSide> TurnToGo ()//определение очереди стороны
        {
            while (true)
            {
                yield return PlayerSide.First;
                yield return PlayerSide.Second;
            }



        }

        private bool AccessChP(ChessPiece chP)//определяет, есть ли фигура на клетке и соответствует ли она очереди
        {
            PlayerSide side = (PlayerSide)_turn.Current;

            if ( chP != null && chP.Side == side)
            {
                return true;

            }

            else
            {
                return false;
            }


        }

        private bool AccessCell(Point p,ChessPiece chP)//определяет безопасен ли данный ход для короля
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

            else//фигура не является королем шахов больше двух
                access = false;


            return access;

        }

        private bool InInterval(Point p,List<Point> interval)
        {
            foreach (Point i in interval)
            {
                if (p==i)
                {
                    return true;
                }

            }
            return false;

            
        }

        private bool VictoryCheck()
        {
            int count = 0;

            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxY; x++)
                {
                    ChessPiece chP = _field.GetChP(new Point(y, x));
                    if (chP != null && chP.Side == (PlayerSide)_turn.Current)
                    {
                        SecurityCheckChP(chP, chP.GetMoves());
                    }

                }

            }

            return true;

        }


    }

}
