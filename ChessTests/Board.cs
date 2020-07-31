using ChessTests;
using System;
using ChessTests.Pieces;
using System.Collections.Generic;
using System.Linq;
using ChessTests.Helpers;

namespace ChessTable
{
    public class Board
    {
        public Cell[,] cells;
        private Cell checkMatePosition;
        private InitializeBoard initialize;
        private readonly List<Piece> whitePieces = new List<Piece>();
        public bool GetWin { get; set; }
        public Board(bool withPieces = true)
        {
            cells = new Cell[8, 8];
            initialize = new InitializeBoard(cells);
            if (withPieces)
            {
                initialize.InitializeBoardWithPieces();
            }
            else
            {
                initialize.InitializeBoardWithoutPieces();
            }
            PopulateWhiteListPiece();
        }


        public Piece FindPieceWhoNeedsToBeMoved(string moveAN, PieceColor playerColor)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, playerColor);
            return FindPieceWhoNeedsToBeMoved(move, playerColor);
        }

        public Piece FindPieceWhoNeedsToBeMoved(Move move, PieceColor playerColor)
        {
            //must check if board is in initial state
            var destinationCell = TransformCoordonatesIntoCell(move.Coordinate);
            var pieceName = move.PieceName;

            return GetPiece(move, playerColor, pieceName);
        }



        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            var destinationCell = TransformCoordonatesIntoCell(move.Coordinate);

            var piece = FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            CheckIfPieceExists(piece);

            if (move.IsKingCastling && !move.IsCheck)
            {
                if (piece.IsOnInitialPosition())
                {
                    KingCastling(piece, currentPlayer, destinationCell, move);
                    Console.WriteLine(currentPlayer + " makes king castling!");
                    return piece;
                }
                else
                {
                    throw new InvalidOperationException("Invalid castling!");
                }
            }

            if (move.IsQueenCastling && !move.IsCheck)
            {
                if (piece.IsOnInitialPosition())
                {
                    QueenCastling(piece, currentPlayer, destinationCell, move);
                    Console.WriteLine(currentPlayer + " makes quuen castling!");
                    return piece;
                }
                else
                {
                    throw new InvalidOperationException("Invalid castling!");
                }
            }

            if (move.IsCapture)
            {
                whitePieces.Remove(destinationCell.Piece);
                Console.WriteLine(currentPlayer + " capture " + destinationCell.Piece + " at " + move.Coordinates);
                move.CapturePiece(piece, destinationCell);

            }

            move.MovePiece(piece, destinationCell);

            if (move.Promotion != null)
            {
                var promotedTo = PromotePawn(move, piece);
                Console.WriteLine(currentPlayer + " pawn promoted to " + promotedTo);
            }

            if (move.IsCheck)
            {
                //verify if king is actually in check
                if (!GameValidation.CheckIfKingIsInCheck(piece, currentPlayer, move))
                {
                    move.IsCapture = false;
                }
                Console.WriteLine(currentPlayer + "puts opponent king in check!");
                //TO DO if check is bad-> throw
            }

            if (move.IsCheckMate)
            {
                GetWin = IsCheckMate(currentPlayer, move, piece);
                if (GetWin)
                {
                    Console.WriteLine(currentPlayer + " king is in checkmate!");
                }
                else
                {
                    throw new InvalidOperationException("Illegal move, !");
                }
            }

            return piece;
        }


        private bool IsCheckMate(PieceColor currentPlayer, Move move, Piece piece)
        {
            checkMatePosition = piece.CurrentPosition;
            //find king
            var king = (King)BoardAction.FindKing(checkMatePosition, currentPlayer);
            //verify if is in check mate
            move.IsCheck = true;
            return CheckIfKingIsInCheckMate(king, currentPlayer, move);
        }



        public Piece PromotePawn(Move move, Piece pawn)
        {
            pawn.CurrentPosition = null;
            var pawnPromovatesTo = AddPiece(move.Coordinate, move.Promotion);
            return pawnPromovatesTo;
        }

        public Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        {
            if (coordinate.X >= 0 && coordinate.X <= 7 && coordinate.Y >= 0 && coordinate.Y <= 7)
            {
                return cells[coordinate.X, coordinate.Y];
            }
            throw new IndexOutOfRangeException("Index out of bound");
        }

        internal Piece AddPiece(string coordsAN, Piece piece)
        {
            var coordinates = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return AddPiece(coordinates, piece);
        }

        internal Piece AddPiece(Coordinate coordinates, Piece piece)
        {
            var cell = CellAt(coordinates);
            cell.Piece = piece;
            return cell.Piece;
        }

        internal Cell CellAt(string coordsAN)
        {
            var result = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return TransformCoordonatesIntoCell(result);
        }

        internal Cell CellAt(Coordinate coordinates)
        {
            return TransformCoordonatesIntoCell(coordinates);
        }

        private static void CheckIfPieceExists(Piece piece)
        {
            if (piece == null)
            {
                throw new InvalidOperationException("Invalid move!");
            }
        }

        private bool CheckIfKingIsInCheck(Piece pieceWhoMakesCheck, PieceColor currentPlayer, Move move)
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

        private bool CheckIfKingIsInCheckMate(King king, PieceColor playerColor, Move move)
        {
            var boardState = this;
            var cellsWhereKingCanMove = new List<Cell>();

            var orientations = King.KingOrientation();
            var isCheck = new List<bool>();
            cellsWhereKingCanMove.Add(king.CurrentPosition);
            if (king.Name == PieceName.King)
            {
                Cell currentCell = null;
                //look around the king
                _ = AvailableCellsAroundKing(king, cellsWhereKingCanMove, orientations, currentCell);

                if (cellsWhereKingCanMove.Count > 0)
                {
                    for (int i = 0; i < cellsWhereKingCanMove.Count; i++)
                    {
                        currentCell = cellsWhereKingCanMove[i];

                        if (i >= 1)
                        {
                            bool result;
                            var kingCoords = king.CurrentPosition;
                            if (currentCell.Piece != null)
                            {
                                var piece = currentCell.Piece;

                                move.CapturePiece(king, currentCell);

                                move.MovePiece(king, currentCell);

                                //means that capture puts king in check
                                result = VerifyIfKingIsInCheck(playerColor, currentCell);
                                if (result)
                                {
                                    var moveOne = TransformIntoMoveInstance(piece, currentCell);
                                    boardState.AddPiece(moveOne.Coordinate, piece);

                                    var moveTwo = TransformIntoMoveInstance(king, kingCoords);
                                    boardState.AddPiece(moveTwo.Coordinate, king);

                                    whitePieces.Add(piece);
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
                            continue;
                        }

                        //verify if is in check
                        isCheck.Add(VerifyIfKingIsInCheck(playerColor, currentCell));

                    }
                }
            }

            return isCheck.All(i => i == true);
        }

        private bool VerifyIfKingIsInCheck(PieceColor playerColor, Cell currentCell)
        {
            foreach (var item in whitePieces)
            {
                Move move = TransformIntoMoveInstance(item, currentCell);

                if (item.Name == PieceName.Pawn)
                {
                    move.IsCapture = true;
                }
                var piece = FindPieceWhoNeedsToBeMoved(move, playerColor);
                if (piece != null)
                {
                    return true;
                }
            }
            return false;
        }

        private static Move TransformIntoMoveInstance(Piece item, Cell currentCell)
        {
            Move move = new Move();
            var coords = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(currentCell.X, currentCell.Y);
            move.Coordinate = coords;
            move.PieceName = item.Name;
            move.Color = item.pieceColor;
            return move;
        }

        private static Cell AvailableCellsAroundKing(King king, List<Cell> cellsWhereKingCanMove, List<Orientation> orientations, Cell currentCell)
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

        private Piece GetPiece(Move move, PieceColor playerColor, PieceName pieceName)
        {
            switch (pieceName)
            {
                case PieceName.Pawn:
                    return Pawn.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Queen:
                    return Queen.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Bishop:
                    return Bishop.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Rook:
                    return Rook.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.King:
                    return King.ValidateMovementAndReturnPiece(this, move, playerColor);
                case PieceName.Knight:
                    return Knight.ValidateMovementAndReturnPiece(this, move, playerColor);
                default:
                    return null;
            }
        }

        private void PopulateWhiteListPiece()
        {
            for (int i = 7; i >= 6; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    whitePieces.Add(cells[i, j].Piece);
                }
            }
        }


        //unde sa le mut?
        private void KingCastling(Piece king, PieceColor currentPlayer, Cell destinationCell, Move move)
        {
            var currentCell = destinationCell;
            if (currentCell.Piece != null) throw new InvalidOperationException("Invalid state!");

            currentCell = currentCell.Look(Orientation.Right);
            if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.pieceColor && king.IsOnInitialPosition())
            {
                var rook = currentCell.Piece;
                if (rook.IsOnInitialPosition())
                {
                    move.MovePiece(king, destinationCell);
                    move.MovePiece(rook, destinationCell.Look(Orientation.Left));
                }
                else
                {
                    throw new InvalidOperationException("Rook is invalid state!");
                }
            }

        }

        private void QueenCastling(Piece king, PieceColor currentPlayer, Cell destinationCell, Move move)
        {
            var currentCell = destinationCell.Look(Orientation.Right);
            if (currentCell.Piece == null)
            {
                for (int i = currentCell.Y - 1; i >= 0; i--)
                {
                    currentCell = currentCell.Look(Orientation.Left);
                    if (currentCell == null) break;
                    if (currentCell.Piece != null && currentCell.Y != 0) throw new InvalidOperationException("Invalid state!");
                    if (currentCell.Y == 0)
                    {
                        if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.pieceColor && king.IsOnInitialPosition())
                        {
                            var rook = currentCell.Piece;
                            move.MovePiece(king, destinationCell);
                            move.MovePiece(rook, destinationCell.Look(Orientation.Right));
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid state!");
            }
        }
    }
}
