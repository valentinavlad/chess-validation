﻿using ChessTable;
using System;

namespace ChessTests.Helpers
{
    internal class Action
    {
        private readonly Board board;
        private Cell cell;
        public Action(Board board)
        {
            this.board = board;
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
            return board.TransformCoordonatesIntoCell(result);
        }

        internal Cell CellAt(Coordinate coordinates)
        {
            return board.TransformCoordonatesIntoCell(coordinates);
        }

        //public Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        //{
        //    if (coordinate.X >= 0 && coordinate.X <= 7 && coordinate.Y >= 0 && coordinate.Y <= 7)
        //    {
        //        return cell.cells[coordinate.X, coordinate.Y];
        //        //return cells[coordinate.X, coordinate.Y];
        //    }
        //    throw new IndexOutOfRangeException("Index out of bound");
        //}
    }
}
