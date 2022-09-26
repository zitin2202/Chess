using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    static class Data
    {
        public static Dictionary<string, Type> StrToChPType = new Dictionary<string, Type>()
        {   {"B", typeof(Bishop)},
            {"K", typeof(Knight)},
            {"R", typeof(Rook)},
            {"Q", typeof(Queen)},
        };


    }
}
