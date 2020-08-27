using ChessTests;
using System;
using ChessTests.Pieces;
using System.Collections.Generic;
using ChessTests.Helpers;
using ChessTests.Validations;
using ChessTests.GameAction;
using ChessTests.Interfaces;
using System.Linq;

namespace ChessTable
{
    public class Board : IBoard
    {
        private Cell[,] cells;
        private IEnumerable<IPiece> pieces = new List<IPiece>();
 
        private readonly InitializeBoard initialize;
        private readonly KingValidation kingValidation;
        internal readonly List<Piece> whitePieces = new List<Piece>();
        private readonly CastlingHelpers castling = new CastlingHelpers();

        public Board(bool withPieces = true)
        {
            cells = new Cell[8, 8];
            initialize = new InitializeBoard(cells);
            kingValidation = new KingValidation(this);

            if (withPieces)
            {
                initialize.InitializeBoardWithPieces();
            }
            else
            {
                initialize.InitializeBoardWithoutPieces();
            }  
        }

        public bool GetWin { get; set; }
        public IEnumerable<IPiece> Pieces
        {
            get => pieces = PutPiecesIntoList();

            set => pieces = value;
        }

        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            GetPiece(moveAN, currentPlayer, out Move move, out Piece piece);
            if (move.IsKingCastling && !move.IsCheck) return castling.TryKingCastling(currentPlayer, move, piece);

            if (move.IsQueenCastling && !move.IsCheck) return castling.TryQueenCastling(currentPlayer, move, piece);
           
            if (move.IsCapture)
            {
                pieces.ToList().Remove(move.DestinationCell.Piece);
               
                move.CapturePiece(piece, move.DestinationCell);
            }

            move.MovePiece(piece, move.DestinationCell);

            if (move.Promotion != null) PromotePawn(move, piece);

            IsKingInCheckMate(currentPlayer, move);

            return piece;
        }


        public bool FindPieceWhoNeedsToBeMoved(string moveAN, PieceColor playerColor)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, playerColor);
            move.DestinationCell = CellAt(move.Coordinate);
            return FindPieceWhoNeedsToBeMoved(move);
        }

        public bool FindPieceWhoNeedsToBeMoved(Move move)
        {
            move.DestinationCell = CellAt(move.Coordinate);
            return IsPiece(move);
        }

        public Piece PromotePawn (Move move, Piece pawn)
        {
            pawn.DestinationCell = null;
            var pawnPromovatesTo = AddPiece(move.Coordinate, move.Promotion);
            pieces.Append(pawnPromovatesTo);
            
            return pawnPromovatesTo;
        }


        private void IsKingInCheckMate(PieceColor currentPlayer, Move move)
        {
            if (move.IsCheckMate)
            {
                GetWin = kingValidation.IsCheckMate(currentPlayer, move);
                if (!GetWin) throw new InvalidOperationException("Illegal win, king is not in checkmate!");
            }
        }

        private void GetPiece(string moveAN, PieceColor currentPlayer, out Move move, out Piece piece )
        {
            move = GetMoveFromNotation(moveAN, currentPlayer);
            FindPieceWhoNeedsToBeMoved(move);
            piece = move.PiecePosition.Piece;
         
        }

        private Move GetMoveFromNotation(string moveAN, PieceColor currentPlayer)
        {
            var move = MoveNotationConverter.ParseMoveNotation(moveAN, currentPlayer);

            move.DestinationCell = CellAt(move.Coordinate);
            return move;
        }

        public IEnumerable<Piece> FindPieces(IMove move, IEnumerable<Orientation> orientations)
        {    
            foreach (var orientation in orientations)
            {
                var currentCell = TransformCoordonatesIntoCell(move.Coordinate);
               // var currentCell = move.CurrentPosition;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;

                    if (currentCell.Piece.Name == move.Name && move.PieceColor == currentCell.Piece.PieceColor)
                    {
                        yield return currentCell.Piece;
                    }
                    if (currentCell.Piece.Name == PieceName.King && move.PieceColor != currentCell.Piece.PieceColor)
                    {
                        yield return currentCell.Piece;
                    }
                    break;
                }
            }
            
        }

        public Piece FoundedPiece(IMove move, IEnumerable<Piece> findPieces)
        {
            IEnumerable<Piece> list = findPieces.Where(x => x.Name == move.Name);
            if (list.Count() == 1)
            {
                return list.First();
            }
            else if (list.Count() > 1)
            {
                return list.Where(q => q.DestinationCell.Y == move.Y).First();
            }
            return null;
        }

   
        internal bool IsPiece(Move move)
        {
            IPiece piece = PieceFactory.CreatePiece(move.Name, move.PieceColor);
            return piece.ValidateMovement(this, move);
        }

        private IEnumerable<IPiece> PutPiecesIntoList()
        {
            foreach (var cell in cells)
            {
                if (cell.Piece != null)
                {
                    yield return cell.Piece;
                }
            }
        }
        //Created for testing purpose
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

        public Cell CellAt(Coordinate coordinates)
        {
            return TransformCoordonatesIntoCell(coordinates);
        }
        //Created for testing purpose

        //board specific methods
        private Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        {
            if (coordinate.X >= 0 && coordinate.X <= 7 && coordinate.Y >= 0 && coordinate.Y <= 7)
            {
                return cells[coordinate.X, coordinate.Y];
            }
            throw new IndexOutOfRangeException("Index out of bound");
        }

    }
}
