using ChessTests;
using System;
using ChessTests.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessTable
{
    public class Board
    {
        public Cell[,] cells;
        private bool BlackCheck;
        private Cell checkMatePosition;
        private Cell checkPosition;
        private bool WhiteCheck;
        private List<Piece> whitePieces = new List<Piece>();
        private bool win;
        public Board(bool withPieces = true)
        {
            win = false;    
            cells = new Cell[8,8];
            if (withPieces)
            {
                InitializeBoard();
            }
            else
            {
                InitializeBoardWithoutPieces();
            }
            PopulateWhiteListPiece();
        }

        public void CapturePiece(Piece attacker, Cell cellDestination)
        {
            if (CellHasOpponentPiece(attacker, cellDestination))
            {
                var opponent = cellDestination.Piece;
                opponent.CurrentPosition = null;
                cellDestination.Piece = null;

                whitePieces.Remove(opponent);
            }
            else
            {
                throw new InvalidOperationException("Invalid move!");
            }
        }

        public King FindKing(Cell checkMatePosition, PieceColor playerColor)
        {
            //var currentCell = checkMatePosition;
            //starting from this position, search 180 degree for opponent king
            List<Orientation> orientations = new List<Orientation>
            {
                Orientation.Up,  Orientation.DownLeft,
                Orientation.UpRight, Orientation.Right,
                Orientation.DownRight, Orientation.Down,
                Orientation.Left,  Orientation.UpLeft
            };
            foreach (var orientation in orientations)
            {
                //var loop = true;
                var currentCell = checkMatePosition;
                while (true)
                {
                    //there is no piece on the cells
                    currentCell = currentCell.Look(orientation);

                    //Search looks out of board
                    if (currentCell == null) break;

                    if (currentCell.Piece == null) continue;


                    if (currentCell.Piece.Name == PieceName.King && playerColor != currentCell.Piece.pieceColor)
                    {
                        return (King)currentCell.Piece;
                    }

                    //there is an obstacle in the way, must throw exception or return
                    break;
                }
            }

            return null;
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

            return GetPiece(move, playerColor, pieceName);
        }

        public void Move(Piece piece, Cell destinationCell)
        {
            var previousPosition = piece.CurrentPosition;
            destinationCell.Piece = piece;
            previousPosition.Piece = null;
            piece.CurrentPosition = destinationCell;
        }

        public Piece PlayMove(string moveAN, PieceColor currentPlayer)
        {
            //interpretarea mutarii
            var move = ConvertAMoveIntoACellInstance.ParseMoveNotation(moveAN, currentPlayer);

            //executia
            //TO DO if is castling fix errors on destination cell, dont have coords
            var destinationCell = TransformCoordonatesIntoCell(move.Coordinate);


            var piece = FindPieceWhoNeedsToBeMoved(move, currentPlayer);

            CheckIfPieceExists(piece);

            if (move.IsKingCastling && !move.IsCheck)
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

            if (move.IsQueenCastling && !move.IsCheck)
            {
                if (piece.IsOnInitialPosition())
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
            if (move.Promotion != null)
            {
                PromotePawn(move, piece);
            }

            if (checkPosition != null)
            {
                //this means that the piece who made the check is been removed
                if (currentPlayer == checkPosition.Piece.pieceColor)
                {
                    checkPosition = null;
                    WhiteCheck = false;
                    BlackCheck = false;
                }
            }

            if (move.IsCheck)
            {
                //verify if king is actually in check
                if (CheckIfKingIsInCheck(piece, currentPlayer, move))
                {
                    if (piece.pieceColor == PieceColor.Black)
                    {
                        this.WhiteCheck = true;
                    }
                    else
                    {
                        this.BlackCheck = true;
                    }
                    checkPosition = piece.CurrentPosition;
                }
            }

            if (move.IsCheckMate)
            {
                checkMatePosition = piece.CurrentPosition;
                //find king
                King king = FindKing(checkMatePosition, currentPlayer);
                //verify if is in check mate
                move.IsCheck = true;
                if (CheckIfKingIsInCheckMate(king, currentPlayer, move))
                {
                    //game over
                    win = true;
                    checkMatePosition = piece.CurrentPosition;
                }
            }

            return piece;
        }
        public bool GetWin()
        {
            return win;
        }
        public Piece PromotePawn(Move move, Piece pawn)
        {
            pawn.CurrentPosition = null;
            var pawnPromovatesTo = AddPiece(move.Coordinate, move.Promotion);
            return pawnPromovatesTo;
        }

        public Cell TransformCoordonatesIntoCell(Coordinate coordinate)
        {

            if (coordinate.X < 0 || coordinate.X > 7 || coordinate.Y < 0 || coordinate.Y > 7)
            {
                throw new IndexOutOfRangeException("Index out of bound");
            }

            return cells[coordinate.X, coordinate.Y];
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

        internal Cell CellAt(string coordsAN)
        {
            var result = ConvertAMoveIntoACellInstance.ConvertChessCoordinatesToArrayIndexes(coordsAN);
            return TransformCoordonatesIntoCell(result);
        }

        internal Cell CellAt(Coordinate coordinates)
        {
            return TransformCoordonatesIntoCell(coordinates);
        }

        private static void CheckIfPieceExists(Piece piece)
        {
            if (piece == null)
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

        private bool CheckIfKingIsInCheck(Piece pieceWhoMakesCheck, PieceColor currentPlayer, Move move)
        {
            bool result = false;
            if (pieceWhoMakesCheck.Name == PieceName.Bishop)
            {
                Bishop pieceWhoMakesCheckp = (Bishop)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Rook)
            {
                Rook pieceWhoMakesCheckp = (Rook)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Queen)
            {
                Queen pieceWhoMakesCheckp = (Queen)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Knight)
            {
                Knight pieceWhoMakesCheckp = (Knight)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer);
            }
            if (pieceWhoMakesCheck.Name == PieceName.Pawn)
            {
                Pawn pieceWhoMakesCheckp = (Pawn)pieceWhoMakesCheck;
                result = pieceWhoMakesCheckp.CheckForOpponentKingOnSpecificRoutes(pieceWhoMakesCheck.CurrentPosition, currentPlayer, move);
            }
            //TO DO for all other pieces
            return result;
        }

        private bool CheckIfKingIsInCheckMate(King king, PieceColor playerColor, Move move)
        {
            var boardState = this;
            var cellsWhereKingCanMove = new List<Cell>();
          
            var orientations = King.KingOrientation();
            var isCheck = new List<bool>();
            cellsWhereKingCanMove.Add(king.CurrentPosition);
            if (king.Name == PieceName.King)
            {
                Cell currentCell = null;
                //return Test(piece.CurrentPosition, currentPlayer);
                //look around the king
                _ = AvailableCellsAroundKing(king, cellsWhereKingCanMove, orientations, currentCell);
              
                if (cellsWhereKingCanMove.Count > 0)
                {
                    //TO DO if a move takes off the king mate
                    for (int i = 0; i < cellsWhereKingCanMove.Count; i++)
                    {
                        currentCell = cellsWhereKingCanMove[i];

                        if (i >= 1)
                        {
                            bool result;
                            var kingCoords = king.CurrentPosition;
                            if (currentCell.Piece != null)
                            {
                                var piece = currentCell.Piece;

                                boardState.CapturePiece(king, currentCell);

                                boardState.Move(king, currentCell);

                                //means that capture puts king in check
                                result = VerifyIfKingIsInCheck(playerColor, currentCell);
                                if (result)
                                {
                                    var moveOne = TransformIntoMoveInstance(piece, currentCell);
                                    boardState.AddPiece(moveOne.Coordinate, piece);

                                    var moveTwo = TransformIntoMoveInstance(king, kingCoords);
                                    boardState.AddPiece(moveTwo.Coordinate, king);

                                    whitePieces.Add(piece);
                                }
                            }
                            else
                            {
                                boardState.Move(king, currentCell);
                                result = VerifyIfKingIsInCheck(playerColor, currentCell);
                                if (result)
                                {         
                                    boardState.Move(king, kingCoords);
                                }
                            }

                            isCheck.Add(result);
                            continue;
                        }

                        //verify if is in check
                        isCheck.Add(VerifyIfKingIsInCheck(playerColor, currentCell));
                        //moved king in empty cell

                        //check if is in check from this position
                        //iterate from current position of King on all routes to see if there is opponent piece who can attack king

                    }
                }
            }
            
                return isCheck.All(i => i == true);
            
          
        }

     

        private bool VerifyIfKingIsInCheck(PieceColor playerColor, Cell currentCell)
        {
            foreach (var item in whitePieces)
            {
                Move move2 = TransformIntoMoveInstance(item, currentCell);

                if (item.Name == PieceName.Pawn)
                {
                    move2.IsCapture = true;
                }
                var piece = FindPieceWhoNeedsToBeMoved(move2, playerColor);
                if (piece != null)
                {
                    return true;
                }


            }
            return false;
        }

        private static Move TransformIntoMoveInstance(Piece item, Cell currentCell)
        {
            Move move2 = new Move();
            var coords = ConvertAMoveIntoACellInstance.ConvertChessCoordinatesToArrayIndexes(currentCell.X, currentCell.Y);
            move2.Coordinate = coords;
            move2.PieceName = item.Name;
            move2.Color = item.pieceColor;
            return move2;
        }

        private static Cell AvailableCellsAroundKing(King king, List<Cell> cellsWhereKingCanMove, List<Orientation> orientations, Cell currentCell)
        {
            foreach (var orientation in orientations)
            {
                currentCell = king.CurrentPosition;

                //there is no piece on the cells
                currentCell = currentCell.Look(orientation);

                //Search looks out of board
                if (currentCell == null) continue;

                if (currentCell.Piece == null || currentCell.Piece.pieceColor != king.pieceColor)
                {
                    cellsWhereKingCanMove.Add(currentCell);
                    continue;
                }

            }

            return currentCell;
        }

        private Piece GetPiece(Move move, PieceColor playerColor, PieceName pieceName)
        {
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
            cells[7, 1] = new Cell(7, 1, new Knight(PieceColor.White), cells);
            cells[7, 2] = new Cell(7, 2, new Bishop(PieceColor.White), cells);
            cells[7, 3] = new Cell(7, 3, new Queen( PieceColor.White), cells);
            cells[7, 4] = new Cell(7, 4, new King(PieceColor.White), cells);
            cells[7, 5] = new Cell(7, 5, new Bishop( PieceColor.White), cells);
            cells[7, 6] = new Cell(7, 6, new Knight(PieceColor.White), cells);
            cells[7, 7] = new Cell(7, 7, new Rook(PieceColor.White), cells);
        }
        private void PopulateWhiteListPiece()
        {
            for (int i = 7; i >= 6; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    whitePieces.Add(cells[i, j].Piece);
                }
            }
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
       
        private void KingCastling(Piece king, PieceColor currentPlayer, Cell destinationCell)
        {
            var currentCell = destinationCell;
            if (currentCell.Piece != null) throw new InvalidOperationException("Invalid state!");

            currentCell = currentCell.Look(Orientation.Right);
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

        }

        private void QueenCastling(Piece king, PieceColor currentPlayer, Cell destinationCell)
        {
            var currentCell = destinationCell.Look(Orientation.Right);
            if (currentCell.Piece == null)
            {
                for (int i = currentCell.Y - 1; i >= 0; i--)
                {
                    currentCell = currentCell.Look(Orientation.Left);
                    if (currentCell == null) break;
                    if (currentCell.Piece != null && currentCell.Y != 0) throw new InvalidOperationException("Invalid state!");
                    if (currentCell.Y == 0)
                    {
                        if (currentCell.Piece.Name == PieceName.Rook && currentPlayer == currentCell.Piece.pieceColor && king.IsOnInitialPosition())
                        {
                            var rook = currentCell.Piece;
                            Move(king, destinationCell);
                            Move(rook, destinationCell.Look(Orientation.Right));
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid state!");
            }
        }
    }
}
