using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Classes
{   
    public class Bot
    {
        Process process;
        public string _moves = "";
        string _move;
        private string _promotionChess = "";
        private int _skillLevel = 20; //min - 0, max - 20
        private int _thinkingTime = 2000;



        public Bot()
        {
            
        }
        private Queue<string> TakeAndMove()
        {
            process = Process.Start(new ProcessStartInfo
            {
                FileName = "stockfish.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                CreateNoWindow = true,

        });

            StreamWriter wr = process.StandardInput;
            wr.WriteLine("setoption name threads value 12");
            wr.WriteLine($"setoption name Skill Level value {_skillLevel}");
            wr.WriteLine("position startpos move " + _moves);
            wr.WriteLine($"go movetime {_thinkingTime}");
            Thread.Sleep(_thinkingTime);
            wr.Close();
            _move = process.StandardOutput.ReadToEnd();
            _move = _move.Substring(_move.IndexOf("bestmove") + 9, 5);

            _move = _move.Trim();

            if (_move.Length==5)
            {
               _promotionChess = _move.Substring(4, 1);
                _move = _move.Substring(0, 4);
            }
            Queue<string> chPAndMove = new Queue<string>();
            chPAndMove.Enqueue(_move.Substring(0, 2));
            chPAndMove.Enqueue(_move.Substring(2, 2));

            return chPAndMove;
        }


        public Func<FieldPoint> СhooseChpAndMove()
        {
            string movee = _move;
            string movees = _moves;
            Queue<string> chPAndMove = TakeAndMove();
            return () => { return Data.GetPointUsingName(chPAndMove.Dequeue()); };
        }
        public Type PromotionSet()
        {
            Type promotionChess = Data.StrToChPClass[_promotionChess];
            _promotionChess = "";
            return promotionChess;
        }

        public void MoveAdd(FieldPoint beforePoint, FieldPoint afterPoint)
        {
            _moves += Data.fieldPointToName[0, beforePoint.x] + Data.fieldPointToName[1, beforePoint.y];
            _moves += Data.fieldPointToName[0, afterPoint.x] + Data.fieldPointToName[1, afterPoint.y];
            _moves += " ";
        }


        public void PromotionAdd(string promotionChess)
        {
            _moves = _moves.Trim();
            _moves += promotionChess + " ";
        }

    }


    
}
