using System;

namespace Match3Core
{
    [Flags]
    public enum Conditions
    {
        None = 0,
        ColorCounter = 1 << 0,
        Special = 1 << 1,
        Unblock = 1 << 2,
        Shape = 1 << 3
    }
}