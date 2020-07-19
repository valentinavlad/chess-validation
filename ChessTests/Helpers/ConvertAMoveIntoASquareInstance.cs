using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChessTests
{
    public static class ConvertAMoveIntoACellInstance
    {

        public static void ConvertChessCoordonatesToArrayIndexes(string move, out int x, out int y)
        {

            string coordonates = TryParseChessCoordonatesToArrayIndexes(move);
            if (coordonates.Length != 2)
            {
                throw new IndexOutOfRangeException("coordonates must be exactly 2");
            }

             y = Convert.ToInt32(char.GetNumericValue(coordonates.First()));
             x = Convert.ToInt32(char.GetNumericValue(coordonates.Last()));
           
        }

        // Return the piece type from the move, that has been read from file.
        public static PieceName ConvertPieceInitialFromMoveToPieceName(string move)
        {
            PieceName pieceName;
            string piecesNameInitials = "PRKBQN";

            var pieceNameInitial = move.First();
            var getNumericValueFromPieceName = char.IsUpper(pieceNameInitial) ? piecesNameInitials.IndexOf(pieceNameInitial) : 0;

            var values = Enum.GetValues(typeof(PieceName));
            pieceName = (PieceName)values.GetValue(getNumericValueFromPieceName);
            return pieceName;
        }

        public static string TryParseChessCoordonatesToArrayIndexes(string move)
        {

            var firstChar = move.First();
            //moves with captures
            if (move.Contains('x'))
            {
               
                if (char.IsUpper(firstChar))
                {
                    //move with capture
                    
                }
                else
                {
                    //pawn move with capture and/or promotion
                }
            }
            //simple moves
            else
            {
                if (char.IsUpper(firstChar))
                {
                    //move
                    
                }
                else
                {
                    //pawn move
                    string pattern = @"^(?<coordinates>[a-h][1-8])(?<promotion>[BRQKN]{0,1})([+]{0,2})$";
                    Regex reg = new Regex(pattern);
                    Match match = Regex.Match(move,pattern);
                    //TO DO access named groups
                    if (reg.IsMatch(move))
                    {
                        Console.WriteLine("works");
                        return "yes";
                    }
                }

            }

            return "no";
            //string files = "abcdefgh";
            //string ranks = "87654321";
            //if (move.Length > 2 && move.Length <= 5)
            //{
            //    return move.Skip(1).Take(2).Aggregate("", (ac, t) =>
            //        char.IsLetter(t) ? ac += files.IndexOf(t) : ac += ranks.IndexOf(t));
            //}
            ////d1Q

            //return move.Aggregate("", (ac, t) =>
            //                  char.IsLetter(t) ? ac += files.IndexOf(t) : ac += ranks.IndexOf(t));
        }
    }
}
