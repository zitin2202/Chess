using System;
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
        private string _promotionChess = "";


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
            wr.WriteLine("setoption name Skill Level value 20");
            wr.WriteLine("position startpos move " + _moves);
            wr.WriteLine("go movetime 2000");
            Thread.Sleep(2000);
            wr.Close();
            string str = process.StandardOutput.ReadToEnd();
            str = str.Substring(str.IndexOf("bestmove") + 9, 5);

            str = str.Trim();
            if (str == "e1c1" && str == "e1g1")
            {

            }
            if (str.Length==5)
            {
               _promotionChess = str.Substring(4, 1);
                str = str.Substring(0, 4);
            }
            Queue<string> chPAndMove = new Queue<string>();
            chPAndMove.Enqueue(str.Substring(0, 2));
            chPAndMove.Enqueue(str.Substring(2, 2));
            return chPAndMove;
        }


        public Func<FieldPoint> СhooseChpAndMove()
        {
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
