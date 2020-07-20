﻿using ChessTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessTests
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor pieceColor) : base(pieceColor)
        {
            Name = PieceName.Pawn;
        }


        public override bool PieceCanMove(Board board, Cell start, Cell end)
        {
            throw new NotImplementedException();
        }

        public static Piece ValidateMovementAndReturnPiece (Board board, Move move, PieceColor playerColor)
        {
            var destinationCell = board.TransformCoordonatesIntoCell(move.Coordinate);
            var cells = board.cells;
            if (playerColor == PieceColor.White && !move.IsCapture)
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
                    if (piece.IsOnInitialPosition() == false)
                    {
                        throw new InvalidOperationException("Pawn is in an invalid state!");
                    }
                    return cells[i, destinationCell.Y].Piece;
                }
            }

            if (playerColor == PieceColor.Black && !move.IsCapture)
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

            if (playerColor == PieceColor.White && move.IsCapture)
            {
                //destination cell ->c4
                int x = destinationCell.X + 1;
                int jRight = destinationCell.Y + 1;
                int jLeft = destinationCell.Y - 1;

                int y = move.Y == jRight ? jRight : jLeft;

                if (cells[x, y].Piece != null && cells[x, y].Piece.Name == PieceName.Pawn)
                {
                    return cells[x, y].Piece;
                }

                throw new InvalidOperationException("Illegal move!");
            }

            if (playerColor == PieceColor.Black && move.IsCapture)
            {
                //destination cell ->d3
                int x = destinationCell.X - 1;
                int jRight = destinationCell.Y + 1;
                int jLeft = destinationCell.Y - 1;

                int y = move.Y == jRight ? jRight : jLeft;

                if (cells[x, y].Piece != null && cells[x, y].Piece.Name == PieceName.Pawn)
                {
                    return cells[x, y].Piece;
                }

                throw new InvalidOperationException("Illegal move!");
            }
            throw new InvalidOperationException("Illegal move!");
        }

    }
}
