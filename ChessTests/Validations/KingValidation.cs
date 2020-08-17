using ChessTable;
using ChessTests.Helpers;
using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests.Validations
{
    internal class KingValidation
    {
        private readonly List<Orientation> orientations = new List<Orientation>
        {
            Orientation.Up,  Orientation.DownLeft,
            Orientation.UpRight, Orientation.Right,
            Orientation.DownRight, Orientation.Down,
            Orientation.Left,  Orientation.UpLeft
        };

        private readonly Board board;
        private readonly Helpers.Action action;
        internal BoardAction boardAction = new BoardAction();

        public KingValidation(Board board)
        {
            this.board = board;
            action = new Helpers.Action(board);
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

        private Piece KingMovesCapture(King king, Move move, Cell currentCell)
        {
            var piece = currentCell.Piece;

            move.CapturePiece(king, currentCell);

            move.MovePiece(king, currentCell);
            return piece;
        }

        private void UndoPlayMove(King king, Cell currentCell, Cell kingCoords, Piece piece)
        {
            var moveOne = MoveNotationConverter.TransformIntoMoveInstance(piece, currentCell);
            action.AddPiece(moveOne.Coordinate, piece);

            var moveTwo = MoveNotationConverter.TransformIntoMoveInstance(king, kingCoords);
            action.AddPiece(moveTwo.Coordinate, king);

            board.whitePieces.Add(piece);
        }

        internal Cell AvailableCellsAroundKing(King king, List<Cell> cellsWhereKingCanMove, Cell currentCell)
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

        internal bool VerifyIfKingIsInCheck(PieceColor playerColor, Cell currentCell)
        {
            var isPiece = false;
            foreach (var item in board.whitePieces)
            {
                Move move = MoveNotationConverter.TransformIntoMoveInstance(item, currentCell);

                if (item.Name == PieceName.Pawn)
                {
                    move.IsCapture = true;
                }
                isPiece = board.FindPieceWhoNeedsToBeMoved(move);
            }
            return isPiece;
        }

        internal bool IsCheckMate(PieceColor currentPlayer, Move move)
        {
            var king = (King)boardAction.FindPieces(move, move.PieceName, orientations).First();
            return CheckIfKingIsInCheckMate(king, currentPlayer, move);
        }
    }
}
