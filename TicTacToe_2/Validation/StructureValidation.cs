namespace TicTacToe_2
{
    public class StructureValidation  
    {
        public static bool UserInputValidation(string userInput) 
        {
            userInput = userInput.ToUpper();

            return
                 CheckNumberOfSymbolsInInput(userInput) &&
                 CheckValueOfLetter(userInput) &&
                 CheckValueOfNumber(userInput);
        }

        private static bool CheckNumberOfSymbolsInInput(string userInput)
        {
            int numberOfSimpols = userInput.Length;

            if (numberOfSimpols == 2)
            {
                return true;
            }
            return false;
        }

        private static bool CheckValueOfLetter(string userInput)
        {
            char[] correctYValues = { 'A', 'B', 'C' };

            if (correctYValues.Contains(userInput[0]))
            {
                return true;
            }
            return false;
        }

        private static bool CheckValueOfNumber(string userInput)
        {
            char[] correctXValues = {'1', '2', '3'};

            if (correctXValues.Contains(userInput[1]))
            {
                return true;
            }
            return false;
        }
    }
}
