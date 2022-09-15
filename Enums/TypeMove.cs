using System;
using System.Collections.Generic;
using System.Text;

namespace Enums
{
    public enum TypeMove
    {
        Simple,
        Attack,
        All //Для всех фигур кроме пешек. Далее, TypeMove клетки, на которой стоит другая фигура нужно будет менять на Attack (по крайней мере, таков план)
    }
}
