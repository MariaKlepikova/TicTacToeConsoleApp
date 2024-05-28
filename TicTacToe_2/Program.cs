namespace TicTacToe_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("\t КРЕСТИКИ - НОЛИКИ");
            Console.WriteLine();

            MoveType firstMoveType = MoveType.X;

            var (firstPlayer, secondPlayer) = GetPlayerNames(firstMoveType);

            Player[] players = { firstPlayer, secondPlayer };

            var ticTacToeGame = new TicTacToeGameEngine(firstMoveType);

            var (statusOfGame, winner) = ticTacToeGame.CheckGameStatus();

            while (statusOfGame == GameStatus.nextMove)
            {
                Console.Clear();

                Console.WriteLine();

                string fieldView = GetPlayingFieldView(ticTacToeGame);

                Console.WriteLine(fieldView);

                MoveType currentMoveType = ticTacToeGame.GetNextMoveType();

                Player currentPlayer = players.Where(player => player.MoveType == currentMoveType).First();

                string nameCurrentPlayer = currentPlayer.Name;

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Сейчас ходит {nameCurrentPlayer}");
                Console.ForegroundColor = ConsoleColor.Gray;


                Point playerMove;
                bool isSuccessMove;

                do
                {
                    playerMove = GetPlayerPointMove();

                    Console.WriteLine();

                    isSuccessMove = ticTacToeGame.TryDoMove(playerMove);
                    
                    if(isSuccessMove)
                    {
                        break;
                    }

                    string moveNotValidError = ConsoleAdapter.GetNotValidMoveOnFieldError();

                    Console.WriteLine();
                    Console.WriteLine(moveNotValidError);
                }
                while (isSuccessMove == false);


                (statusOfGame, winner) = ticTacToeGame.CheckGameStatus();

                if (statusOfGame == GameStatus.nextMove)
                {
                    string gameStatusView = ConsoleAdapter.GetResultOfGame(statusOfGame);

                    Console.WriteLine(gameStatusView);
                }
            }

            Console.Clear();

            Console.WriteLine();

            string gameStatusViewFinish = ConsoleAdapter.GetResultOfGame(statusOfGame);

            Console.WriteLine(gameStatusViewFinish);

            if (statusOfGame == GameStatus.win)
            {
                Player winnerPlayer = players.Where(player => player.MoveType == winner).First();

                string nameWinnerPlayer = winnerPlayer.Name;

                Console.WriteLine("\t Победил " + nameWinnerPlayer);
            }

            Console.WriteLine();

            Console.WriteLine("\t Игра окончена!");

            Console.WriteLine();

            var fieldView2 = GetPlayingFieldView(ticTacToeGame);

            Console.WriteLine(fieldView2);

            Console.ReadLine();
        }

        private static string GetPlayingFieldView(TicTacToeGameEngine engine)
        {
            CellStatus[,] fieldOfGame = engine.ShowPlayingField();

            var fieldView = ConsoleAdapter.PrintPlayingField(fieldOfGame);
            return fieldView;
        }

        private static Point GetPlayerPointMove()
        {
            string userInput;
            bool isCorrectInput;
            do
            {
                Console.WriteLine("Нужно ввести координаты клетки поля (сначала английская буква, затем цифра)");

                userInput = Console.ReadLine();

                string resultOfStructureValidation = ConsoleAdapter.CheckResultOfStructureValidation(userInput);

                isCorrectInput = string.IsNullOrEmpty(resultOfStructureValidation);

                if (!isCorrectInput)
                {
                    Console.WriteLine();
                    Console.WriteLine(resultOfStructureValidation);
                }
            }
            while (isCorrectInput == false);

            return ConsoleAdapter.GetCoordinatesMove(userInput);
        }

        private static (Player playerOne, Player playerTwo) GetPlayerNames(MoveType firstMoveType)
        {
            if (firstMoveType == MoveType.X)
            {
                Console.WriteLine("Первым ходит крестик");
            } 
            else
            {
                Console.WriteLine("Первым ходит нолик");
            }

            Console.Write("Введи имя первого игрока: ");

            string nameOneInput = Console.ReadLine();

            string namePlayerOne = ConsoleAdapter.CreatePlayerNames(nameOneInput, firstMoveType);
            Console.Write("Введи имя второго игрока: ");

            MoveType secondMoveType = firstMoveType == MoveType.X ? MoveType.O : MoveType.X;

            string nameTwoInput = Console.ReadLine();

            string namePlayerTwo = ConsoleAdapter.CreatePlayerNames(nameTwoInput, secondMoveType);

            return (
                new Player(firstMoveType, namePlayerOne),
                new Player(secondMoveType, namePlayerTwo)
                );
        }
    }
}
