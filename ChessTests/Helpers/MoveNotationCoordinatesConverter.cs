using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests.Helpers
{
    public static class MoveNotationCoordinatesConverter
    {
        public static Coordinate ConvertChessCoordinatesToArrayIndexes(string coordsAN)
        {
            Coordinate coordinate = new Coordinate();
            if (coordsAN.Length != 2)
            {
                throw new IndexOutOfRangeException("coordonates must be exactly 2");
            }
            coordinate.X = ConvertChessCoordinateRankToArrayIndex(coordsAN);
            coordinate.Y = ConvertChessCoordinateFileToArrayIndex(coordsAN);

            return coordinate;
        }
        public static Coordinate ConvertChessCoordinatesToArrayIndexes(int x, int y)
        {
            Coordinate coordinate = new Coordinate
            {
                X = x,
                Y = y
            };

            return coordinate;
        }
        public static int ConvertChessCoordinateFileToArrayIndex(string coordsAN)
        {
            string files = "abcdefgh"; //refers to columns => x

            var file = coordsAN.First();

            return files.IndexOf(file);
        }
        public static int ConvertChessCoordinateRankToArrayIndex(string coordsAN)
        {
            string ranks = "87654321"; //refers to rows => y

            var rank = coordsAN.Last();

            return ranks.IndexOf(rank);
        }
    }
}
