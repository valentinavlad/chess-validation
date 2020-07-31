using ChessTable;
using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Text;

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
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Rook)
            {
                Rook pieceWhoMakesCheckp = (Rook)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Queen)
            {
                Queen pieceWhoMakesCheckp = (Queen)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Knight)
            {
                Knight pieceWhoMakesCheckp = (Knight)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Pawn)
            {
                Pawn pieceWhoMakesCheckp = (Pawn)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer, move);
            }
            //TO DO for all other pieces
            return result;
        }

        internal static Cell AvailableCellsAroundKing(King king, List<Cell> cellsWhereKingCanMove, List<Orientation> orientations, Cell currentCell)
        {
            foreach (var orientation in orientations)
            {
                currentCell = king.CurrentPosition;

                //there is no piece on the cells
                currentCell = currentCell.Look(orientation);

                //Search looks out of board
                if (currentCell == null) continue;

                if (currentCell.Piece == null || currentCell.Piece.pieceColor != king.pieceColor)
                {
                    cellsWhereKingCanMove.Add(currentCell);
                    continue;
                }

            }

            return currentCell;
        }


    }
}
