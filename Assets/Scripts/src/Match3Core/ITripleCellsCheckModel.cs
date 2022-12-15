namespace Src.Match3Core
{
    public interface ITripleCellsCheckModel
    {
        int GetColumnsCount();

        int GetRowsCount();

        Cell[] GetCollumnArray(int collumnNumber);

        Cell[] GetRowArray(int rowNumber);
    }

}
