using ChessTests;
using System;
using ChessTests.Pieces;
using System.Collections.Generic;

namespace ChessTable
{
    public class Board
    {
        public Cell[,] cells;
        //list active black pieces 
        //list active white pieces
        //has pieces

        //......

        public Board(bool withPieces = true)
        {
            cells = new Cell[8,8];
            if (withPieces)
            {
                InitializeBoard();
            }
            else
            {
                InitializeBoardWithoutPieces();
            }
        }


        public void Move(Piece piece, Cell destinationCell)
        {
            //make a move if move is valid

            //resets the previous cell, after the piece is moved
           
            //if (piece.Name == PieceName.Pawn)
            //{
                var previousPosition = piece.CurrentPosition;
                destinationCell.Piece = piece;
                previousPosition.Piece = null;
           // }

            piece.CurrentPosition = destinationCell;
        }

        public Piece FindPieceWhoNeedsToBeMoved(string moveAN, PieceColor playerColor)
        {
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, playerColor);
            return FindPieceWhoNeedsToBeMoved(move, playerColor);
        }
        public Piece FindPieceWhoNeedsToBeMoved(Move move, PieceColor playerColor)
        {
            //must check if board is in initial state
            var destinationCell = TransformCoordonatesIntoCell(move.Coordinate);
            var pieceName = move.PieceName;

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

        public Piece PromotePawn(Move move, Piece pawn)
        {
            pawn.CurrentPosition = null;
            var pawnPromovatesTo = AddPiece(move.Coordinate, move.Promotion);
            return pawnPromovatesTo;
        }

        public void CapturePiece(Piece attacker, Cell cellDestination)
        {
            if (CellHasOpponentPiece(attacker, cellDestination))
            {
                var opponent = cellDestination.Piece;
                opponent.CurrentPosition = null;
                cellDestination.Piece = null;
            }
            else
            {
                throw new InvalidOperationException("Invalid move!");
            }
        }

        private bool CellHasOpponentPiece(Piece attacker, Cell cellDestination) 
        {
            //cauta daca exista pion pe celula destinatie
            var opponent = cellDestination.Piece;
            return opponent != null && opponent.pieceColor != attacker.pieceColor;        
        }

        public Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        {
           
            if (coordinate.X < 0 || coordinate.X > 7 || coordinate.Y < 0 || coordinate.Y > 7)
            {
                throw new IndexOutOfRangeException("Index out of bound");
            }

            return cells[coordinate.X, coordinate.Y];
        }

        private void InitializeBoard()
        {

            //initialize black pieces
            cells[0, 0] = new Cell(0, 0, new Rook(PieceColor.Black), cells);
            cells[0, 1] = new Cell(0, 1, new Knight(PieceColor.Black), cells);
            cells[0, 2] = new Cell(0, 2, new Bishop(PieceColor.Black), cells);
            cells[0, 3] = new Cell(0, 3, new Queen(PieceColor.Black), cells);
            cells[0, 4] = new Cell(0, 4, new King(PieceColor.Black), cells);
            cells[0, 5] = new Cell(0, 5, new Bishop(PieceColor.Black), cells);
            cells[0, 6] = new Cell(0, 6, new Knight(PieceColor.Black), cells);
            cells[0, 7] = new Cell(0, 7, new Rook(PieceColor.Black), cells);

            //initialize black pawns
            for (int i = 1; i == 1 ; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Pawn(PieceColor.Black), cells);
                }
            }

            // initialize remaining boxes without any piece 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i,j] = new Cell(i, j, null, cells);
                }
            }

            //initialize white pawns
            for (int i = 6; i == 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Pawn(PieceColor.White), cells);
                }
            }

            //initialize white pieces
            cells[7, 0] = new Cell(7, 0, new Rook(PieceColor.White), cells);
            cells[7, 1] = new Cell(7, 1,  new Knight(PieceColor.White), cells);
            cells[7, 2] = new Cell(7, 2, new Bishop(PieceColor.White), cells);
            cells[7, 3] = new Cell(7, 3, new Queen( PieceColor.White), cells);
            cells[7, 4] = new Cell(7, 4, new King(PieceColor.White), cells);
            cells[7, 5] = new Cell(7, 5, new Bishop( PieceColor.White), cells);
            cells[7, 6] = new Cell(7, 6, new Knight(PieceColor.White), cells);
            cells[7, 7] = new Cell(7, 7, new Rook(PieceColor.White), cells);
        }

        private void InitializeBoardWithoutPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, cells);
                }
            }
        }

        internal Cell CellAt(string coordsAN)
        {
            var result = ConvertAMoveIntoACellInstance.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return TransformCoordonatesIntoCell(result);
        }

        internal Cell CellAt(Coordinate coordinates)
        {
            return TransformCoordonatesIntoCell(coordinates);
        }

        internal Piece AddPiece(string coordsAN, Piece piece)
        {
            var coordinates = ConvertAMoveIntoACellInstance.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return AddPiece(coordinates, piece);
        }

        internal Piece AddPiece(Coordinate coordinates, Piece piece)
        {
            var cell = CellAt(coordinates);
            cell.Piece = piece;
            return cell.Piece;
        }

        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            //interpretarea mutarii
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

            //executia
            //TO DO if is castling fix errors on destination cell, dont have coords
            var destinationCell = TransformCoordonatesIntoCell(move.Coordinate);

            //find king
            
            var piece = FindPieceWhoNeedsToBeMoved(move, currentPlayer);
            if (piece == null)
            {
                throw new InvalidOperationException("Invalid move!");
            }

            if (move.IsKingCastling )
            {
                if (piece.IsOnInitialPosition())
                {
                    KingCastling(piece, currentPlayer, destinationCell);
                    return piece;
                }
                else
                {
                    throw new InvalidOperationException("Invalid castling!");
                }


            }

            if (move.IsQueenCastling)
            {
                if(piece.IsOnInitialPosition())
                {
                    QueenCastling(piece, currentPlayer, destinationCell);
                    return piece;
                }
                else
                {
                    throw new InvalidOperationException("Invalid castling!");
                }
            }

            if (move.IsCapture)
            {
                CapturePiece(piece, destinationCell);
            }

            Move(piece, destinationCell);
            //TO DO promote pawn if necessary
            //TO DO verify if move makes check
            //TO DO verify if move makes check mate
            
            return piece;
        }

        private void KingCastling(Piece king, PieceColor currentPlayer, Cell destinationCell)
        {
            var currentCell = destinationCell;
            for (int i = currentCell.Y; i > 0; i--)
            {
                currentCell = currentCell.Look(Orientation.Left);
                if (currentCell.Piece == null) continue;
                if (currentCell.Piece.Name != PieceName.Rook) throw new InvalidOperationException("Invalid move!");
                if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.pieceColor && king.IsOnInitialPosition())
                {
                    var rook = currentCell.Piece;
                    Move(king, destinationCell);
                    Move(rook, destinationCell.Look(Orientation.Right));
                }
            }
        }

        private void QueenCastling(Piece king, PieceColor currentPlayer, Cell destinationCell)
        {
            var currentCell = destinationCell;
            //for (int i = currentCell.Y; i <= 7; i++)
            //{
                currentCell = currentCell.Look(Orientation.Right);
                //if (currentCell.Piece == null) continue;
                if (currentCell.Piece.Name != PieceName.Rook) throw new InvalidOperationException("Invalid move!");
                if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.pieceColor && king.IsOnInitialPosition())
                {
                    var rook = currentCell.Piece;
                    if (rook.IsOnInitialPosition())
                    {
                        Move(king, destinationCell);
                        Move(rook, destinationCell.Look(Orientation.Left));
                    }
                    else
                    {
                        throw new InvalidOperationException("Rook is invalid state!");
                    }
                }
            //}
        }
    }
}
