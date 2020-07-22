using ChessTests.Pieces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChessTests
{
    public static class ConvertAMoveIntoACellInstance
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

        // Return the piece type from the move, that has been read from file.
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
            string file = "";
            Move move = new Move();
            //moves with captures
            if (moveNotation.Contains('x'))
            {
               
                if (char.IsUpper(firstChar))
                {
                    //move with capture

                    string pattern = @"^(?<pieceUppercase>[BRQKN])(?<capture>[x])(?<coordinates>[a-h][1-8])(?<checkOrCheckMate>[+]{0,2})$";
                    Regex reg = new Regex(pattern);
                    Match match = Regex.Match(moveNotation, pattern);

                    if (match.Success)
                    {
                        // ... Get groups by name.

                        coordinatesFromMove = match.Groups["coordinates"].Value;
                        checkOrCheckMate = match.Groups["checkOrCheckMate"].Value;

                        pieceUppercase = match.Groups["pieceUppercase"].Value;
                        move.IsCapture = true;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid move!");
                    }
                }
                else
                {
                    //pawn move with capture and/or promotion
                    string pattern = @"^(?<file>[a-h])(?<capture>[x])(?<coordinates>[a-h][1-8])(?<promotion>[BRQKN]{0,1})(?<checkOrCheckMate>[+]{0,2})$";
                    Regex reg = new Regex(pattern);
                    Match match = Regex.Match(moveNotation, pattern);

                    if (match.Success)
                    {
                        // ... Get groups by name.
                        coordinatesFromMove = match.Groups["coordinates"].Value;
                        if (match.Groups["promotion"].Value != "")
                        {
                            promotion = match.Groups["promotion"].Value.First();
                        }
                        
                        pieceUppercase = "P";
                        file = match.Groups["file"].Value;
                        checkOrCheckMate = match.Groups["checkOrCheckMate"].Value;
                        move.Y = ConvertChessCoordinateFileToArrayIndex(file);
                        move.IsCapture = true;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid move!");
                    }
                }
            }
            //simple moves
            else
            {
                
                //
                if (char.IsUpper(firstChar))
                {

                    if (moveNotation.Skip(1).Take(2).All(c => char.IsLetter(c) && (c >= 'a' && c <= 'h')))
                    {
                        //Ambiguous Move
                        string pattern = @"^(?<pieceUppercase>[BRQKN])(?<file>[a-h])(?<coordinates>[a-h][1-8])(?<checkOrCheckMate>[+]{0,2})$";
                        Regex reg = new Regex(pattern);
                        Match match = Regex.Match(moveNotation, pattern);
                        if (match.Success)
                        {
                            // ... Get groups by name.

                            coordinatesFromMove = match.Groups["coordinates"].Value;
                            pieceUppercase = match.Groups["pieceUppercase"].Value;
                            checkOrCheckMate = match.Groups["checkOrCheckMate"].Value;
                            file = match.Groups["file"].Value;                         
                            move.Y = ConvertChessCoordinateFileToArrayIndex(file);
                        }
                    }
                    else
                    {
                        //move
                        string pattern = @"^(?<pieceUppercase>[BRQKN])(?<coordinates>[a-h][1-8])(?<checkOrCheckMate>[+]{0,2})$";
                        Regex reg = new Regex(pattern);
                        Match match = Regex.Match(moveNotation, pattern);

                        if (match.Success)
                        {
                            // ... Get groups by name.

                            coordinatesFromMove = match.Groups["coordinates"].Value;
                            pieceUppercase = match.Groups["pieceUppercase"].Value;
                            checkOrCheckMate = match.Groups["checkOrCheckMate"].Value;
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid move!");
                        }
                    }
                   

                }
                else
                {
                    //pawn move
                    string pattern = @"^(?<coordinates>[a-h][1-8])(?<promotion>[BRQKN]{0,1})(?<checkOrCheckMate>[+]{0,2})$";
                    Regex reg = new Regex(pattern);
                    Match match = Regex.Match(moveNotation, pattern);
               
                    if (match.Success)
                    {
                        // ... Get groups by name.
                        coordinatesFromMove = match.Groups["coordinates"].Value;
                        if(match.Groups["promotion"].Value != "")
                        {
                            promotion = match.Groups["promotion"].Value.First();
                        }
                       
                        pieceUppercase = "P";
                        checkOrCheckMate = match.Groups["checkOrCheckMate"].Value;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid move!");
                    }
                    
                }

            }
            var coordinate = ConvertChessCoordinatesToArrayIndexes(coordinatesFromMove);
          
            move.Coordinate = coordinate;
            move.Promotion = CreatePiece(promotion, pieceColor);
            move.PieceName = ConvertPieceInitialFromMoveToPieceName(pieceUppercase);
            move.Color = pieceColor;
          

            if (checkOrCheckMate.Length == 1)
            {
                move.IsCheck = true; 
            }
            if (checkOrCheckMate.Length == 2)
            {
                move.IsCheckMate = true;
            }
            return move;       
        }

        public static Piece CreatePiece(char pieceUppercase, PieceColor pieceColor)
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

    }
}
