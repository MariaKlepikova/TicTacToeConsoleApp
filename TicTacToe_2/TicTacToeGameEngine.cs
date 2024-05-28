using System.Reflection;

namespace TicTacToe_2
{

    public class TicTacToeGameEngine
    {
        private CellStatus[,] _playingField;
        private MoveType _firstMoveType;

        public TicTacToeGameEngine(MoveType firstMoveType) 
        {
            _playingField = new CellStatus[3, 3];
            
            _firstMoveType = firstMoveType;
        }

        public CellStatus[,] ShowPlayingField()   
        {
            var axisYLength = _playingField.GetLength(0);
            var axisXLength = _playingField.GetLength(1);

            CellStatus[,] field = new CellStatus[axisYLength, axisXLength];

            for (int indexAxisY = 0; indexAxisY < axisYLength; indexAxisY++)
            {
                for (int indexAxisX = 0; indexAxisX < axisXLength; indexAxisX++)
                {
                    field[indexAxisY, indexAxisX] = _playingField[indexAxisY, indexAxisX];
                }
            }
            return field;
        }

        public bool TryDoMove(Point playerMove) 
        {
            bool isMovePossible = BusinessValidation.CellStatusValidation(_playingField, playerMove);
            if (isMovePossible is false)
            {
                return false;
            }

            MoveType nextMoveType = GetNextMoveType();

            PutShapeOnField(playerMove, nextMoveType);

            return true;
        }

        private void PutShapeOnField(Point playerMove, MoveType moveType)
        {
            CellStatus move = moveType == MoveType.X ? CellStatus.X : CellStatus.O;

            _playingField[playerMove.valueAxisY, playerMove.valueAxisX] = move;
        }

        public MoveType GetNextMoveType()
        {
            int movesCount = GetCountOfMoves();

            bool isLastMoveEven = movesCount % 2 == 0;

            if (isLastMoveEven)
            {
                return _firstMoveType;
            }
            else
            {
                return 
                    _firstMoveType == MoveType.X 
                    ? MoveType.O 
                    : MoveType.X;
            }
        }

        public (GameStatus status, MoveType winner) CheckGameStatus()
        {
            int countOfMoves = GetCountOfMoves();
            int maxCountOfMoves = _playingField.Length;
            if (countOfMoves >= maxCountOfMoves)
            {
                return (GameStatus.draw, MoveType.Unknown);
            }

            CellStatus winnerCell;
            bool isWin = CheckWinsCombination(out winnerCell);

            if (isWin)
            {
                MoveType winner = winnerCell == CellStatus.X ? MoveType.X : MoveType.O;

                return (GameStatus.win, winner);
            }
            return (GameStatus.nextMove, MoveType.Unknown);
        }

        private int GetCountOfMoves()
        {
            int movesCount = 0;

            foreach (var move in _playingField)
            {
                if (move != CellStatus.Empty)
                {
                    movesCount++;
                }
            }

            return movesCount;
        }

        private bool CheckWinsCombination(out CellStatus winner)
        {
            return
                CheckWinByVertical(out winner) ||

                CheckWinByHorizontal(out winner) ||

                CheckWinByLeftDiagonal(out winner) ||

                CheckWinByRightDiagonal(out winner);
        }

        private bool CheckWinByVertical(out CellStatus winner)
        {
            int countOfColumns = _playingField.GetLength(1);

            for (int columnIndex = 0; columnIndex < countOfColumns; columnIndex++)
            {
                bool isWinInCurrentVertical = CheckWinInCurrentVertical(columnIndex, out winner);

                if (isWinInCurrentVertical)
                {
                    return true;
                }
            }

            winner = CellStatus.Empty;
            return false;
        }

        private bool CheckWinInCurrentVertical(int columnIndex, out CellStatus winner)
        {
            int countOfRows = _playingField.GetLength(0);

            CellStatus sampleCellValue = _playingField[columnIndex, 0];

            winner = sampleCellValue;

            if (sampleCellValue == CellStatus.Empty)
            {
                return false;
            }

            CellStatus currentCell;

            for (int rowIndex = 1; rowIndex < countOfRows; rowIndex++)
            {
                currentCell = _playingField[rowIndex, columnIndex];

                if (currentCell != sampleCellValue)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckWinByHorizontal(out CellStatus winner)
        {
            int countOfRows = _playingField.GetLength(0);

            for (int rowIndex = 0; rowIndex < countOfRows; rowIndex++)
            {
                bool isWinInCurrentRow = CheckWinInCurrentHorizontal(rowIndex, out winner);
                if (isWinInCurrentRow)
                {
                    return true;
                }
            }
            winner = CellStatus.Empty;
            return false;
        }

        private bool CheckWinInCurrentHorizontal(int indexOfRow, out CellStatus winner)
        {
            int countOfColumns = _playingField.GetLength(1);

            CellStatus sampleCellValue = _playingField[indexOfRow, 0];

            winner = sampleCellValue;

            if (sampleCellValue == CellStatus.Empty)
            {
                return false;
            }

            for (int indexOfColumn = 1; indexOfColumn < countOfColumns; indexOfColumn++)
            {
                CellStatus currentCell = _playingField[indexOfRow, indexOfColumn];

                if (currentCell != sampleCellValue)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckWinByLeftDiagonal(out CellStatus winner)
        {
            int countOfColumns = _playingField.GetLength(1);

            int index = 0;

            CellStatus sampleCellValue = _playingField[index, index];

            winner = sampleCellValue;

            if (sampleCellValue == CellStatus.Empty)
            {
                return false;
            }

            for (index = 1; index < countOfColumns; index++)
            {
                CellStatus currentCell = _playingField[index, index];

                if (currentCell != sampleCellValue)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckWinByRightDiagonal(out CellStatus winner)
        {
            int countOfColumns = _playingField.GetLength(1);

            int rowIndex = 0;
            int columnIndex = countOfColumns - 1;

            CellStatus sampleCellValue = _playingField[rowIndex, columnIndex];

            winner = sampleCellValue;

            if (sampleCellValue == CellStatus.Empty)
            {
                return false;
            }

            for (rowIndex = 1, columnIndex--; columnIndex >= 0; rowIndex++, columnIndex--)
            {
                CellStatus currentCell = _playingField[rowIndex, columnIndex];

                if (currentCell != sampleCellValue)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
