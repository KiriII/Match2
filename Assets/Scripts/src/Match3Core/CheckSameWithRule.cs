using System.Collections.Generic;

namespace Src.Match3Core
{
    public abstract class CheckSameWithRule
    {
        public ITripleCellsCheckModel _match3Model;

        public CheckSameWithRule(ITripleCellsCheckModel match3Model)
        {
            _match3Model = match3Model;
        }

        public abstract void CheckSame(ref List<(int, int)> findedCells);
    }
}
