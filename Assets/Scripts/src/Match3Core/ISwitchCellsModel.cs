namespace Match3Core
{
    public interface ISwitchCellsModel
    {
        void SwitchTwoCells((int x, int y) switchCell1, (int x, int y) switchCell2);

        Cell[,] GetBoard();

        string Print();
    }
}