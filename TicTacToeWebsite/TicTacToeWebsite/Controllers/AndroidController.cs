using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicTacToeWebsite.Data;
using TicTacToeWebsite.Models;

namespace TicTacToeWebsite.Controllers
{
    public class AndroidController : ControllerBase
    {
        private readonly UserAccountDbContext _userContext;

        private IHostingEnvironment _env;

        public AndroidController(UserAccountDbContext context,IHostingEnvironment environment)
        {
            _userContext = context;
            _env = environment;
        }

        [HttpPost]
        public async Task<string> SignUpUser(string UserName,string FullName,string EmailID,string Password)
        {
            var user = _userContext.UserAccount.FirstOrDefault(m => m.UserName == UserName);
            if(user!=null)
            {
                return "UserNameExists";
            }
            else
            {
                user = _userContext.UserAccount.FirstOrDefault(m => m.EmailID == EmailID);
                if(user!=null)
                {
                    return "EmailIDExists";
                }
                else
                {
                    UserAccount newUser = new UserAccount
                    {
                        UserName = UserName,
                        FullName = FullName,
                        EmailID = EmailID,
                        Password = Password,
                        ConfirmPassword = Password,
                        SingleEasyLoses = 0,
                        SingleEasyTies = 0,
                        SingleEasyWins = 0,
                        SingleHardLoses = 0,
                        SingleHardTies = 0,
                        SingleHardWins = 0,
                        DoubleLoses = 0,
                        DoubleTies = 0,
                        DoubleWins = 0
                    };
                    _userContext.UserAccount.Add(newUser);
                    var result = await _userContext.SaveChangesAsync();
                    return "Success";
                }
            }
        }

        [HttpPost]
        public string LogInUser(string UserName,string Password)
        {
            var user = _userContext.UserAccount.FirstOrDefault(m => m.UserName == UserName);
            if(user!=null)
            {
                if(user.Password==Password)
                {
                    return JsonConvert.SerializeObject(user);
                }
                else
                {
                    return "PasswordWrong";
                }
            }
            else
            {
                return "UserNameWrong";
            }
        }

        [HttpPost]
        public async Task<string> UpdateStatsMob(string UserName, string Password,string SingleEasyWinsCount,string SingleEasyLosesCount,string SingleEasyTiesCount,string SingleHardWinsCount,string SingleHardLosesCount,string SingleHardTiesCount,string DoubleWinsCount,string DoubleLosesCount,string DoubleTiesCount)
        {
            var user = _userContext.UserAccount.FirstOrDefault(m => (m.UserName == UserName && m.Password == Password));
            if(user!=null)
            {
                user.SingleEasyWins += long.Parse(SingleEasyWinsCount);
                user.SingleEasyLoses += long.Parse(SingleEasyLosesCount);
                user.SingleEasyTies += long.Parse(SingleEasyTiesCount);
                user.SingleHardWins += long.Parse(SingleHardWinsCount);
                user.SingleHardLoses += long.Parse(SingleHardLosesCount);
                user.SingleHardTies += long.Parse(SingleHardTiesCount);
                user.DoubleWins += long.Parse(DoubleWinsCount);
                user.DoubleLoses += long.Parse(DoubleLosesCount);
                user.DoubleTies += long.Parse(DoubleTiesCount);

                _userContext.UserAccount.Update(user);
                await _userContext.SaveChangesAsync();
                return "True";
            }
            else
            {
                return "False";
            }
        }

        [HttpPost]
        public async Task<string> DeActivateAccount(string UserName, string Password)
        {
            var user = _userContext.UserAccount.FirstOrDefault(m => (m.UserName == UserName && m.Password == Password));
            if(user!=null)
            {
                _userContext.UserAccount.Remove(user);
                await _userContext.SaveChangesAsync();
                return "True";
            }
            else
            {
                return "False";
            }
        }

        [HttpPost]
        public async Task<string> OnlinePlayers()
        {
            List<PlayerClass> playerList = JsonConvert.DeserializeObject<List<PlayerClass>>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, "Players.json")));
            if (playerList == null)
            {
                return "NoPlayers";
            }
            else
            {
                var player = playerList.FirstOrDefault();
                return player.PlayerName;
            }
        }

        [HttpPost]
        public string ConnectToGame(string PlayerName, string PlayerID)
        {
            PlayerClass temp = new PlayerClass() { PlayerName = PlayerName, PlayerID = PlayerID };

            List<PlayerClass> playerList = JsonConvert.DeserializeObject<List<PlayerClass>>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, "Players.json")));
            if (playerList == null)
            {
                playerList = new List<PlayerClass>();
                playerList.Add(temp);


                string edata = JsonConvert.SerializeObject(playerList);
                System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, "Players.json"), edata);


                GameDataClass tempPlayer1 = new GameDataClass() { MyName = temp.PlayerName, MyID = temp.PlayerID, OpponentName = temp.PlayerName, OpponentID = temp.PlayerID, IfClicked = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, SymbolPresent = new string[] { "b", "b", "b", "b", "b", "b", "b", "b", "b" }, Turn = "Mine", MySymbol = "X", OpponentSymbol = "O", IfConnected = "False", Status = "Wait", IfAlive = "False", IfQuit = "False" };
                string gamedataPlayer1 = JsonConvert.SerializeObject(tempPlayer1);
                System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, temp.PlayerID + ".json"), gamedataPlayer1);
                return "WaitingForPlayer";

            }
            else
            {

                var Player1 = playerList.LastOrDefault();
                var Player2 = temp;


                System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, "Players.json"));
                System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, "Players.json"), "");


                GameDataClass tempPlayer1 = new GameDataClass() { MyName = Player1.PlayerName, MyID = Player1.PlayerID, OpponentName = Player2.PlayerName, OpponentID = Player2.PlayerID, IfClicked = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, SymbolPresent = new string[] { "b", "b", "b", "b", "b", "b", "b", "b", "b" }, Turn = "Mine", MySymbol = "X", OpponentSymbol = "O", IfConnected = "True", Status = "Wait", IfAlive = "False", IfQuit = "False" };
                GameDataClass tempPlayer2 = new GameDataClass() { OpponentName = Player1.PlayerName, OpponentID = Player1.PlayerID, MyName = Player2.PlayerName, MyID = Player2.PlayerID, IfClicked = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, SymbolPresent = new string[] { "b", "b", "b", "b", "b", "b", "b", "b", "b" }, Turn = "Opponent", OpponentSymbol = "X", MySymbol = "O", IfConnected = "True", Status = "Wait", IfAlive = "True", IfQuit = "False" };


                string gamedataPlayer1 = JsonConvert.SerializeObject(tempPlayer1);
                System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player1.PlayerID + ".json"));
                System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, Player1.PlayerID + ".json"), gamedataPlayer1);



                string gamedataPlayer2 = JsonConvert.SerializeObject(tempPlayer2);
                System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, temp.PlayerID + ".json"), gamedataPlayer2);
                return "WaitingForPlayer";
            }
        }

        [HttpPost]
        public async Task<string> EmptyThePlayersFile(string id)
        {
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, "Players.json"));
            await System.IO.File.WriteAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, "Players.json"), "");
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, id + ".json"));
            return "Deleted";
        }

        [HttpPost]
        public string DeleteOnlyMe(string id)
        {
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, id + ".json"));
            return "Deleted";
        }

        [HttpPost]
        public async Task<string> WaitingForPlayer(string ID)
        {
            var temp = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
            if (temp.IfConnected == "True")
            {
                temp.IfAlive = "True";
                string gamedata = JsonConvert.SerializeObject(temp);
                await System.IO.File.WriteAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, temp.MyID + ".json"), gamedata);
                return "True";
            }
            else
                return "False";
        }

        [HttpPost]
        public async Task<string> ReturnMyData(string ID)
        {
            try
            {
                var Player = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
                return JsonConvert.SerializeObject(Player);
            }
            catch (Exception ex)
            {
                return "False";
            }
        }

        [HttpPost]
        public async Task<string> CheckIfOpponentAlive(string ID)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));
            if (Opponent.IfAlive == "True")
            {
                return "True";
            }
            else if (Opponent.IfAlive == "False")
            {
                System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
                System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
                return "False";
            }
            else
            {
                return "False";
            }
        }

        [HttpPost]
        public async Task<string> CheckStatus(string ID)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
            return JsonConvert.SerializeObject(Player);
            
        }

        [HttpPost]
        public async Task<string> UpdateStatus(string updatedJson)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(updatedJson);
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));

            if (Opponent.IfQuit == "True")
            {
                Player.IfQuit = "True";
            }

            for (int i = 0; i <= 8; i++)
            {
                Opponent.IfClicked[i] = Player.IfClicked[i];
                Opponent.SymbolPresent[i] = Player.SymbolPresent[i];

            }



            Player.Turn = "Opponent";
            Opponent.Turn = "Mine";



            var tempSymbol = Player.MySymbol;
            if (Player.SymbolPresent[0] == tempSymbol && Player.SymbolPresent[1] == tempSymbol && Player.SymbolPresent[2] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[3] == tempSymbol && Player.SymbolPresent[4] == tempSymbol && Player.SymbolPresent[5] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[6] == tempSymbol && Player.SymbolPresent[7] == tempSymbol && Player.SymbolPresent[8] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[0] == tempSymbol && Player.SymbolPresent[3] == tempSymbol && Player.SymbolPresent[6] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[1] == tempSymbol && Player.SymbolPresent[4] == tempSymbol && Player.SymbolPresent[7] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[2] == tempSymbol && Player.SymbolPresent[5] == tempSymbol && Player.SymbolPresent[8] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[0] == tempSymbol && Player.SymbolPresent[4] == tempSymbol && Player.SymbolPresent[8] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.SymbolPresent[2] == tempSymbol && Player.SymbolPresent[4] == tempSymbol && Player.SymbolPresent[6] == tempSymbol)
            {
                Player.Status = "Won";
                Opponent.Status = "Lost";
            }
            else if (Player.IfClicked[0] != 0 && Player.IfClicked[1] != 0 && Player.IfClicked[2] != 0 && Player.IfClicked[3] != 0 && Player.IfClicked[4] != 0 && Player.IfClicked[5] != 0 && Player.IfClicked[6] != 0 && Player.IfClicked[7] != 0 && Player.IfClicked[8] != 0)
            {
                Player.Status = "Tie";
                Opponent.Status = "Tie";
            }


            string gamedataPlayer = JsonConvert.SerializeObject(Player);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
            await System.IO.File.WriteAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"), gamedataPlayer);



            string gamedataOpponent = JsonConvert.SerializeObject(Opponent);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
            await System.IO.File.WriteAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"), gamedataOpponent);

            return "Okay";
        }

        [HttpPost]
        public async Task<string> DeleteQuitted(string id)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, id + ".json")));
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
            return "Deleted";
        }

        [HttpPost]
        public async Task<string> IQuit(string id)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, id + ".json")));
            Player.IfQuit = "True";
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(await System.IO.File.ReadAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));
            Opponent.IfQuit = "True";

            string gamedataOpponent = JsonConvert.SerializeObject(Opponent);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
            await System.IO.File.WriteAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"), gamedataOpponent);

            string gamedataPlayer = JsonConvert.SerializeObject(Player);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
            await System.IO.File.WriteAllTextAsync(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"), gamedataPlayer);
            return "Deleted";
        }
    }
}