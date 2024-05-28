namespace TicTacToe_2
{
    public class Player
    {
        public MoveType MoveType { get; set; }
        public string Name { get; set; }

        public Player(MoveType moveType, string name)
        {
            MoveType = moveType;
            Name = name;
        }
    }
}
