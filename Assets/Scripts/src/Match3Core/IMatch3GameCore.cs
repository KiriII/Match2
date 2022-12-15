
namespace Src.Match3Core
{
    public interface IMatch3GameCore
    {
        void Turn((int x1, int y1) switchCell1, (int x2, int y2) switchCell2);

        // ----- debug -----
        void PrintCurrnetBoard();
    }
}
