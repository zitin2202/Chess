using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public enum TypeMove
    {
        Simple=1,
        Attack,
        All, //Для всех фигур кроме пешек. Далее, TypeMove клетки, на которой стоит другая фигура нужно будет менять на Attack и на Simple без клеток соотвественно
        Сastling
    }
}
