using ChessTable;
using ChessTests.Pieces;

namespace ChessTests.Helpers
{
    internal class GameValidation
    {
        internal static bool CheckIfKingIsInCheck(Piece pieceWhoMakesCheck, PieceColor currentPlayer, Move move)
        {
            bool result = false;
            if (pieceWhoMakesCheck.Name == PieceName.Bishop)
            {
                Bishop pieceWhoMakesCheckp = (Bishop)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(move);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Rook)
            {
                Rook pieceWhoMakesCheckp = (Rook)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(move);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Queen)
            {
                Queen pieceWhoMakesCheckp = (Queen)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(move);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Knight)
            {
                Knight pieceWhoMakesCheckp = (Knight)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(move);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Pawn)
            {
                Pawn pieceWhoMakesCheckp = (Pawn)pieceWhoMakesCheck;
                if (move.IsCapture)
                {
                    result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(move);
                }
               
            }
            return result;
        }




    }
}
