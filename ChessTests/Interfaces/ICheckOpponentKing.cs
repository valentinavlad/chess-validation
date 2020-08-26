namespace ChessTests.Interfaces
{
    public interface ICheckOpponentKing
    {
        public bool CheckForOpponentKingOnSpecificRoutes(IBoard board, Move move);
 
    }
}
