using ChessTests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChessTests.Pieces;
using System.Security.Cryptography;

namespace ChessTable
{
    public class Board
    {
        public Cell[,] cells;

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
           
            if (piece.Name == PieceName.Pawn)
            {
                var previousPosition = piece.CurrentPosition;
                destinationCell.Piece = piece;
                previousPosition.Piece = null;
            }

            piece.CurrentPosition = destinationCell;
        }

        //e4
        public Piece FindPieceWhoNeedsToBeMoved(string move, PieceColor playerColor)
        {
            //must check if board is in initial state
            var destinationCell = TransformCoordonatesIntoCell(move);
            var pieceName = ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move);
            
            if (pieceName == PieceName.Pawn)
            {      
                //validate move forward

                if(playerColor == PieceColor.White)
                {
                    //check for one cell movement
                    int i = destinationCell.X + 1;
                    if (cells[i, destinationCell.Y].Piece != null &&
                    cells[i, destinationCell.Y].Piece.Name == PieceName.Pawn)
                    {
                        return cells[i, destinationCell.Y].Piece;
                    }


                    //check for two cell movements
                    i = i + 1;
                    if (cells[i, destinationCell.Y].Piece != null &&
                        cells[i, destinationCell.Y].Piece.Name == PieceName.Pawn)
                    {
                        var piece = cells[i, destinationCell.Y].Piece;
                        if(piece.IsOnInitialPosition() == false)
                        {
                            throw new InvalidOperationException("Pawn is in an invalid state!");
                        }
                        return cells[i, destinationCell.Y].Piece;
                    }


                }

                if(playerColor == PieceColor.Black)
                {
                    int i = destinationCell.X - 1;
                    if (cells[i, destinationCell.Y].Piece != null &&
                        cells[i, destinationCell.Y].Piece.Name == PieceName.Pawn)
                    {
                        return cells[i, destinationCell.Y].Piece;
                    }

                    i = i - 1;
                    if (cells[i, destinationCell.Y].Piece != null &&
                        cells[i, destinationCell.Y].Piece.Name == PieceName.Pawn)
                    {
                        return cells[i, destinationCell.Y].Piece;
                    }

                }

                throw new InvalidOperationException("Illegal move!");

            }
          
            return null;
        }

        //public Piece PromotePawn()
        //{

        //}

        public Cell TransformCoordonatesIntoCell(string move)
        {
            var coordinate = ConvertAMoveIntoACellInstance.ConvertChessCoordinatesToArrayIndexes(move);

            if (coordinate.X < 0 || coordinate.X > 7 || coordinate.Y < 0 || coordinate.Y > 7)
            {
                throw new IndexOutOfRangeException("Index out of bound");
            }

            return cells[coordinate.X, coordinate.Y];
        }

        private PieceName GetPieceNameFromMoveInFile(string move)
        {
            return ConvertAMoveIntoACellInstance.ConvertPieceInitialFromMoveToPieceName(move);
        }

        private void InitializeBoard()
        {
            //initialize black pieces
            cells[0, 0] = new Cell(0, 0, new Rook(PieceColor.Black));
            cells[0, 1] = new Cell(0, 1, new Knight(PieceColor.Black));
            cells[0, 2] = new Cell(0, 2, new Bishop(PieceColor.Black));
            cells[0, 3] = new Cell(0, 3, new Queen(PieceColor.Black));
            cells[0, 4] = new Cell(0, 4, new King(PieceColor.Black));
            cells[0, 5] = new Cell(0, 5, new Bishop(PieceColor.Black));
            cells[0, 6] = new Cell(0, 6, new Knight(PieceColor.Black));
            cells[0, 7] = new Cell(0, 7, new Rook(PieceColor.Black));

            //initialize black pawns
            for (int i = 1; i == 1 ; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Pawn(PieceColor.Black));
                }
            }

            // initialize remaining boxes without any piece 
            for (int i = 2; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i,j] = new Cell(i, j, null);
                }
            }

            //initialize white pawns
            for (int i = 6; i == 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j, new Pawn(PieceColor.White));
                }
            }

            //initialize white pieces
            cells[7, 0] = new Cell(7, 0, new Rook(PieceColor.White));
            cells[7, 1] = new Cell(7, 1,  new Knight(PieceColor.White));
            cells[7, 2] = new Cell(7, 2, new Bishop(PieceColor.White));
            cells[7, 3] = new Cell(7, 3, new Queen( PieceColor.White));
            cells[7, 4] = new Cell(7, 4, new King(PieceColor.White));
            cells[7, 5] = new Cell(7, 5, new Bishop( PieceColor.White));
            cells[7, 6] = new Cell(7, 6, new Knight(PieceColor.White));
            cells[7, 7] = new Cell(7, 7, new Rook(PieceColor.White));
        }

        private void InitializeBoardWithoutPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    cells[i, j] = new Cell(i, j);
                }
            }
        }

        internal Cell CellAt(string coordonates)
        {
            return TransformCoordonatesIntoCell(coordonates);
        }

        internal Piece AddPiece(string coordonates, Piece piece)
        {
            var cell = CellAt(coordonates);
            cell.Piece = piece;
            return cell.Piece;
        }
    }
}
