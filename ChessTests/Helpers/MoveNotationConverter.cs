using ChessTests.Helpers;
using ChessTests.Pieces;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ChessTests
{
    public static class MoveNotationConverter
    {
        public static PieceName ConvertPieceInitialFromMoveToPieceName(string pieceUppercase)
        {
            PieceName pieceName;
            string piecesNameInitials = "PRKBQN";
            var pieceNameInitial = pieceUppercase.First();
            var getNumericValueFromPieceName = char.IsUpper(pieceNameInitial) ? piecesNameInitials.IndexOf(pieceNameInitial) : 0;
            var values = Enum.GetValues(typeof(PieceName));
            pieceName = (PieceName)values.GetValue(getNumericValueFromPieceName);

            return pieceName;
        }

        public static Move ParseMoveNotation(string moveNotation, PieceColor pieceColor)
        {
            var firstChar = moveNotation.First();
            string coordinatesFromMove = "";
            char promotion = ' ';
            string checkOrCheckMate = "";
            string pieceUppercase = "";
            string pattern;

            Move move = new Move();

            if (moveNotation.Contains('x'))
            {
                pattern = char.IsUpper(firstChar) ? @"^(?<pieceUppercase>[BRQKN])(?<file>[a-h]{0,1})(?<capture>[x])(?<coordinates>[a-h][1-8])(?<checkOrCheckMate>[+]{0,2})$"
                          : @"^(?<file>[a-h])(?<capture>[x])(?<coordinates>[a-h][1-8])(?<promotion>[BRQKN]{0,1})(?<checkOrCheckMate>[+]{0,2})$";
                InterpretRegex(moveNotation, out coordinatesFromMove, out checkOrCheckMate, out pieceUppercase, move, pattern, true, out promotion);
            }
            else if (moveNotation.StartsWith("0"))
            {
                pattern = @"^(?<castling>[0]([-][0]){1,2})$";
                InterpretRegexCastling(moveNotation, pieceColor, ref coordinatesFromMove, ref pieceUppercase, pattern, move);
            }
            else
            {
                pattern = char.IsUpper(firstChar)
                    ? @"^(?<pieceUppercase>[BRQKN])(?<file>[a-h]{0,1})(?<coordinates>[a-h][1-8])(?<checkOrCheckMate>[+]{0,2})$"
                    : @"^(?<coordinates>[a-h][1-8])(?<promotion>[BRQKN]{0,1})(?<checkOrCheckMate>[+]{0,2})$";

                InterpretRegex(moveNotation, out coordinatesFromMove, out checkOrCheckMate, out pieceUppercase, move, pattern, false, out promotion);
            }

            ConvertNotationMoveIntoMove(pieceColor, coordinatesFromMove, promotion, checkOrCheckMate, pieceUppercase, move);

            return move;
        }

        private static void ConvertNotationMoveIntoMove(PieceColor pieceColor, string coordinatesFromMove, char promotion, string checkOrCheckMate, string pieceUppercase, Move move)
        {
            var coordinate = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(coordinatesFromMove);

            move.Coordinate = coordinate;
            move.Promotion = CreatePiece(promotion, pieceColor);
            move.Name = ConvertPieceInitialFromMoveToPieceName(pieceUppercase);
            move.PieceColor = pieceColor;
            move.IsCheck = checkOrCheckMate.Length == 1 ? true : false;
            move.IsCheckMate = checkOrCheckMate.Length == 2 ? true : false;
            move.Coordinates = coordinatesFromMove;
        }

        private static void InterpretRegexCastling(string moveNotation, PieceColor pieceColor, ref string coordinatesFromMove, ref string pieceUppercase, string pattern, Move move)
        {
            Match match = Regex.Match(moveNotation, pattern);
            string castling = match.Groups["castling"].Value;

            int countZerosForCastling = castling.Aggregate(0, (ac, c) =>
            {
                return ac = c == '0' ? ac += 1 : ac;
            });

            if (match.Success)
            {
                //white castling
                if (countZerosForCastling == 2)
                {
                    move.IsKingCastling = true;
                    if (pieceColor == PieceColor.White)
                    {
                        coordinatesFromMove = "g1";
                    }
                    else
                    {
                        coordinatesFromMove = "g8";
                    }
                }
                if (countZerosForCastling == 3)
                {
                    move.IsQueenCastling = true;
                    if (pieceColor == PieceColor.White)
                    {
                        coordinatesFromMove = "c1";
                    }
                    else
                    {
                        coordinatesFromMove = "c8";
                    }
                }
                pieceUppercase = "K";
            }
        }

        private static void InterpretRegex(string moveNotation, out string coordinatesFromMove,
            out string checkOrCheckMate, out string pieceUppercase, 
            Move move, string pattern, bool isCapture,out char promotion)
        {
            Match match = Regex.Match(moveNotation, pattern);
            string file;
            
            if (match.Success)
            {
                coordinatesFromMove = match.Groups["coordinates"].Value;
                checkOrCheckMate = match.Groups["checkOrCheckMate"].Value;
                pieceUppercase = match.Groups["pieceUppercase"].Value != "" ? match.Groups["pieceUppercase"].Value : "P";
                move.IsCapture = isCapture;
                file = match.Groups["file"].Value;
                if (file != "")
                {
                    move.Y = MoveNotationCoordinatesConverter.ConvertChessCoordinateFileToArrayIndex(file);
                }
                promotion = match.Groups["promotion"].Value != "" ? match.Groups["promotion"].Value.First() : ' ';
            }
            else
            {
                throw new InvalidOperationException("Invalid move!");
            }
        }

        private static Piece CreatePiece(char pieceUppercase, PieceColor pieceColor)
        {
            string piecesNameInitials = "RBQN ";
            var charExists = piecesNameInitials.Contains(pieceUppercase);
            if (!charExists)
            {
                throw new InvalidOperationException("Invalid promotion!");
            }

            if (pieceUppercase == 'R')
            {
                return new Rook(pieceColor);
            }
            if(pieceUppercase == 'Q')
            {
                return new Queen(pieceColor);
            }
            if (pieceUppercase == 'B')
            {
                return new Bishop(pieceColor);
            }
            if (pieceUppercase == 'N')
            {
                return new Knight(pieceColor);
            }

            return null;
        }

        internal static Move TransformIntoMoveInstance(Piece item, Cell currentCell)
        {
            Move move = new Move();
            var coords = MoveNotationCoordinatesConverter.ConvertChessCoordinatesToArrayIndexes(currentCell.X, currentCell.Y);
            move.Coordinate = coords;
            move.Name = item.Name;
            move.PieceColor = item.PieceColor;
            return move;
        }

    }
}
