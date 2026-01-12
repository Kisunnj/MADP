using Microsoft.AspNetCore.SignalR;
using WEB_253551_Levchuk.BlazorUI.Models.Game;

namespace WEB_253551_Levchuk.BlazorUI.Hubs
{
    public class GameHub : Hub
    {
        private static Dictionary<string, GameRoom> _rooms = new();
        private static Dictionary<string, string> _userRooms = new(); // connectionId -> roomId

        public async Task JoinRoom(string roomId, string playerName)
        {
            if (!_rooms.ContainsKey(roomId))
            {
                _rooms[roomId] = new GameRoom
                {
                    RoomId = roomId,
                    State = GameState.Waiting
                };
            }

            var room = _rooms[roomId];
            
            // Проверка лимита игроков
            if (room.Players.Count >= 2)
            {
                await Clients.Caller.SendAsync("Error", "Комната заполнена");
                return;
            }

            var player = new Player
            {
                ConnectionId = Context.ConnectionId,
                Name = playerName,
                Balance = 1000
            };

            room.Players.Add(player);
            _userRooms[Context.ConnectionId] = roomId;

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("PlayerJoined", player.Name, room.Players.Count);
            await Clients.Group(roomId).SendAsync("UpdateRoom", room);
        }

        public async Task PlaceBet(int betNumber, int betAmount)
        {
            if (!_userRooms.ContainsKey(Context.ConnectionId))
            {
                await Clients.Caller.SendAsync("Error", "Вы не в комнате");
                return;
            }

            var roomId = _userRooms[Context.ConnectionId];
            var room = _rooms[roomId];
            var player = room.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);

            if (player == null || player.Balance < betAmount)
            {
                await Clients.Caller.SendAsync("Error", "Недостаточно средств");
                return;
            }

            player.Balance -= betAmount;
            room.CurrentBet = betNumber;
            room.State = GameState.Rolling;

            await Clients.Group(roomId).SendAsync("BetPlaced", player.Name, betNumber, betAmount);
            
            // Бросаем кости
            await RollDice(roomId);
        }

        private async Task RollDice(string roomId)
        {
            await Task.Delay(1000); // Задержка для эффекта

            var room = _rooms[roomId];
            var random = new Random();
            
            room.DiceResults = new int[]
            {
                random.Next(1, 7),
                random.Next(1, 7),
                random.Next(1, 7)
            };

            await Clients.Group(roomId).SendAsync("DiceRolled", room.DiceResults);

            // Подсчет выигрыша
            await CalculateWinnings(roomId);
        }

        private async Task CalculateWinnings(string roomId)
        {
            var room = _rooms[roomId];
            var player = room.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);

            if (player == null) return;

            int matches = room.DiceResults.Count(d => d == room.CurrentBet);
            int winAmount = 0;

            if (matches == 1)
            {
                winAmount = room.CurrentBet * 2;
            }
            else if (matches == 2)
            {
                winAmount = room.CurrentBet * 3;
            }
            else if (matches == 3)
            {
                winAmount = room.CurrentBet * 10;
            }

            player.Balance += winAmount;
            room.State = GameState.Finished;

            await Clients.Group(roomId).SendAsync("GameResult", matches, winAmount, player.Balance);
            await Clients.Group(roomId).SendAsync("UpdateRoom", room);
            
            // Сброс для новой игры
            await Task.Delay(3000);
            room.State = GameState.Waiting;
            await Clients.Group(roomId).SendAsync("UpdateRoom", room);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (_userRooms.ContainsKey(Context.ConnectionId))
            {
                var roomId = _userRooms[Context.ConnectionId];
                var room = _rooms[roomId];
                var player = room.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);

                if (player != null)
                {
                    room.Players.Remove(player);
                    await Clients.Group(roomId).SendAsync("PlayerLeft", player.Name);
                    
                    if (room.Players.Count == 0)
                    {
                        _rooms.Remove(roomId);
                    }
                }

                _userRooms.Remove(Context.ConnectionId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

