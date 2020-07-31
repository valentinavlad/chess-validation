using ChessTable;
using ChessTests.Helpers;
using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests.Validations
{
    internal class KingValidation
    {
        readonly Board board;

        public KingValidation(Board board)
        {
            this.board = board;
        }

        internal bool CheckIfKingIsInCheckMate(King king, PieceColor playerColor, Move move)
        {
            var cellsWhereKingCanMove = new List<Cell>();
            var isCheck = new List<bool>();
            cellsWhereKingCanMove.Add(king.CurrentPosition);
            isCheck.Add(VerifyIfKingIsInCheck(playerColor, cellsWhereKingCanMove[0]));
            Cell currentCell = null;
            _ = AvailableCellsAroundKing(king, cellsWhereKingCanMove, currentCell);
             
            for (int i = 1; i < cellsWhereKingCanMove.Count; i++)
            {
                currentCell = cellsWhereKingCanMove[i];

                var kingCoords = king.CurrentPosition;
                bool result;
                if (currentCell.Piece != null)
                {
                    Piece piece = KingMovesCapture(king, move, currentCell);
                    result = VerifyIfKingIsInCheck(playerColor, currentCell);
                    if (result)
                    {
                        UndoPlayMove(king, currentCell, kingCoords, piece);
                    }
                    else
                    {
                        throw new InvalidOperationException("Illegal move, King is not in checkmate!");
                    }
                    //else means that king is not in checkmate??
                }
                else
                {
                    move.MovePiece(king, currentCell);
                    result = VerifyIfKingIsInCheck(playerColor, currentCell);
                    if (result)
                    {
                        move.MovePiece(king, kingCoords);
                    }
                }

                isCheck.Add(result);
            }

            return isCheck.All(i => i == true);
        }

        private static Piece KingMovesCapture(King king, Move move, Cell currentCell)
        {
            var piece = currentCell.Piece;

            move.CapturePiece(king, currentCell);

            move.MovePiece(king, currentCell);
            return piece;
        }

        private void UndoPlayMove(King king, Cell currentCell, Cell kingCoords, Piece piece)
        {
            var moveOne = MoveNotationConverter.TransformIntoMoveInstance(piece, currentCell);
            board.AddPiece(moveOne.Coordinate, piece);

            var moveTwo = MoveNotationConverter.TransformIntoMoveInstance(king, kingCoords);
            board.AddPiece(moveTwo.Coordinate, king);

            board.whitePieces.Add(piece);
        }

        internal static Cell AvailableCellsAroundKing(King king, List<Cell> cellsWhereKingCanMove, Cell currentCell)
        {
            var orientations = King.KingOrientation();
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

        internal bool VerifyIfKingIsInCheck(PieceColor playerColor, Cell currentCell)
        {
            foreach (var item in board.whitePieces)
            {
                Move move = MoveNotationConverter.TransformIntoMoveInstance(item, currentCell);

                if (item.Name == PieceName.Pawn)
                {
                    move.IsCapture = true;
                }
                var piece = board.FindPieceWhoNeedsToBeMoved(move, playerColor);
                if (piece != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
