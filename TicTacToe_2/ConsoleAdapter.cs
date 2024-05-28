using System.Text;

namespace TicTacToe_2
{
    public class ConsoleAdapter
    {
        public static string CreatePlayerNames(string name, MoveType moveType)
        {
            name = !string.IsNullOrEmpty(name)
                           ? name
                           : moveType is MoveType.X
                               ? "Крестоносец"
                               : "Ноленосец";
            return name;
        }

        public static string PrintPlayingField(CellStatus[,] field)
        {
            var stringBuilder = new StringBuilder();

            int axisYLength = field.GetLength(0);
            int axisXLenght = field.GetLength(1);

            // нарисуем легенду оси Х
            stringBuilder.Append("\t 1 2 3");
            stringBuilder.AppendLine();

            // нарисуем верхнюю границу таблицы
            stringBuilder.Append("\t _ _ _");
            stringBuilder.AppendLine();

            for (int indexY = 0; indexY < axisYLength; indexY++)
            {
                stringBuilder.Append("     ");
                stringBuilder.Append(DescribeAxisYLegend(indexY));
                stringBuilder.Append("\t");
                stringBuilder.Append("|");

                for (int indexX = 0; indexX < axisXLenght; indexX++)
                {
                    CellStatus cellValue = field[indexY, indexX];

                    string cellView = DescribeCellValue(cellValue);

                    stringBuilder.Append(cellView);
                    stringBuilder.Append("|");
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        static string DescribeCellValue(CellStatus statusOfCell)
        {
            return statusOfCell switch
            {
                CellStatus.Empty => "_",
                CellStatus.X => "X",
                CellStatus.O => "O",
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        static string DescribeAxisYLegend(int index)
        {
            return index switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public static string WhoMove(uint countMove, string namePlayerOne, string namePlayerTwo)
        {
            if (countMove % 2 == 0)
            {
                return namePlayerTwo;
            }
            return namePlayerOne;
        }

        public static string CheckResultOfStructureValidation(string userInput)
        {
            bool isCorrectInput = StructureValidation.UserInputValidation(userInput);

            if (!isCorrectInput)
            {
                return "Ошибка! Введи координаты клетки правильно";
            }
            return "";
        }

        public static Point GetCoordinatesMove(string userInput)
        {
            userInput = userInput.ToUpper();

            var yAxis = ConvertLetterCoordinate(userInput);
            var xAxis = ConvertNumberCoordinate(userInput);

            return new Point()
            {
                valueAxisX = xAxis,
                valueAxisY = yAxis
            };
        }

        static int ConvertLetterCoordinate(string userInput)
        {
            return userInput[0] switch
            {
                'A' => 0,
                'B' => 1,
                'C' => 2,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        static int ConvertNumberCoordinate(string userInput)
        {
            return userInput[1] switch
            {
                '1' => 0,
                '2' => 1,
                '3' => 2,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public static string GetNotValidMoveOnFieldError()
        {
            return "Эта клетка занята! Выбери другой ход";
        }

        public static string GetResultOfGame(GameStatus gameStatus)
        {
            return gameStatus switch
            {
                GameStatus.win => "\t Ура! Победа!",
                GameStatus.nextMove => "\t Играем дальше",
                GameStatus.draw => "\t Ура! Ничья!",
                _ => throw new Exception(),
            };
        }
    }
}
