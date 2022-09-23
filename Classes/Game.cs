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
        public IEnumerator _turn = TurnToGo();
        private Dictionary<ChessPiece, List<(Point, TypeMove)>> _allMovesPoints;
        private RuleControl _rule;


        public Game(Field f)
        {
            _field = f;
            _rule = new RuleControl(this);
        }

        public void Start()
        {
            while (true)
            {
                _turn.MoveNext();
                _rule.SecurityCheckAll();
                if (!allPossibleMoves())
                {
                    Victory();
                    break;
                }
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

            if (_allMovesPoints[chP].Count>0)
            {
                foreach (var i in _allMovesPoints[chP])
                {
                    int y = i.Item1.y;
                    int x = i.Item1.x;

                    _activeChP.Item2[y,x] = i.Item2;
                    Console.WriteLine($"{(y, x)}: {(_field.GetChP(i.Item1) == null ? "Пусто" : i.Item1)}, {i.Item2}");
                }
            }

            else
            {
                Console.WriteLine("У этой фигуры нет возможных ходов");
                return false;

            }

            return true;

        }

        public bool Action(Point p)//перемещение фигуры
        {

            if (_activeChP.Item2[p.y,p.x] == 0)
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

        private List<(Point, TypeMove)> EditMoves(ChessPiece thisChP, IEnumerable<IEnumerable<(Point, TypeMove)>> list)//добавление учета других фигур на доске в передвижениях фигуры
        {
            List<(Point, TypeMove)> movesPoints = new List<(Point,TypeMove)>();
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

                    //на клетке есть вражеская и данным ходом можно атаковать
                    else if (cellChP != null && thisChP.Side != cellChP.Side && type != TypeMove.Simple)
                    {
                        result = TypeMove.Attack;                      
                    }
                    else
                        break;

                    if (result!=0 && AccessCell(p, thisChP))
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

        private void Victory()
        {
            _turn.MoveNext();
            PlayerSide victorySide = (PlayerSide)_turn.Current;
            Console.WriteLine($"Шах и мат! Победила сторона {victorySide}!"); ;
        }


    }

}
