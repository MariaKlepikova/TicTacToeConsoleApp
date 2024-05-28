using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_2
{
    public class BusinessValidation
    {
        public static bool CellStatusValidation(CellStatus[,] _playingField, Point playerMove)
        {
            if (_playingField[playerMove.valueAxisY, playerMove.valueAxisX] == CellStatus.Empty)
            {
                return true;
            }
            return false;
        }
    }
}
