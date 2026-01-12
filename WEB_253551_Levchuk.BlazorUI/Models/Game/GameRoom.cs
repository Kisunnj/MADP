namespace WEB_253551_Levchuk.BlazorUI.Models.Game
{
    public class GameRoom
    {
        public string RoomId { get; set; } = string.Empty;
        public List<Player> Players { get; set; } = new();
        public GameState State { get; set; } = GameState.Waiting;
        public int[] DiceResults { get; set; } = new int[3];
        public int CurrentBet { get; set; }
    }

    public class Player
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Balance { get; set; } = 1000;
    }

    public enum GameState
    {
        Waiting,
        Betting,
        Rolling,
        Finished
    }
}

