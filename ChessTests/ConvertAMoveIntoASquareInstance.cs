using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTests
{
    public static class ConvertAMoveIntoASquareInstance
    {
        public static Square ConvertChessCoordonatesToArrayIndexes(string move)
        {
            string stripColorFromMove = move.Substring(6);
            string getColorPiece = move.Substring(0, 5);

            string coordonates = TryParseChessCoordonatesToArrayIndexes(stripColorFromMove);
            if (coordonates.Length != 2)
            {
                throw new IndexOutOfRangeException("coordonates must be exactly 2");
            }

            int i = Convert.ToInt32(char.GetNumericValue(coordonates.First()));
            int j = Convert.ToInt32(char.GetNumericValue(coordonates.Last()));

            PieceColor pieceColor;
            PieceName pieceName;
            GetColorAndPieceName(stripColorFromMove, getColorPiece, out pieceColor, out pieceName);

            return new Square(i, j, new Piece(pieceColor, pieceName));
        }

        private static void GetColorAndPieceName(string stripColorFromMove, string getColorPiece, out PieceColor pieceColor, out PieceName pieceName)
        {
            string piecesNameInitials = "PRKBQN";
            pieceColor = getColorPiece == "black" ? PieceColor.Black : PieceColor.White;
            var pieceNameInitial = stripColorFromMove.First();
            var getNumericValueFromPieceName = char.IsUpper(pieceNameInitial) ? piecesNameInitials.IndexOf(pieceNameInitial) : 0;

            var values = Enum.GetValues(typeof(PieceName));
            pieceName = (PieceName)values.GetValue(getNumericValueFromPieceName);
        }

        private static string TryParseChessCoordonatesToArrayIndexes(string move)
        {
            string files = "abcdefgh";
            string ranks = "87654321";

            if (move.Length > 2)
            {
                return move.Skip(1).Take(2).Aggregate("", (ac, t) =>
                    char.IsLetter(t) ? ac += files.IndexOf(t) : ac += ranks.IndexOf(t));
            }

            return move.Aggregate("", (ac, t) =>
                              char.IsLetter(t) ? ac += files.IndexOf(t) : ac += ranks.IndexOf(t));
        }
    }
}
