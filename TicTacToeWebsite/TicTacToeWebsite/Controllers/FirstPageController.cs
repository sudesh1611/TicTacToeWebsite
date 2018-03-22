using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicTacToeWebsite.Models;

namespace TicTacToeWebsite.Controllers
{
    public class FirstPageController : Controller
    {
        private IHostingEnvironment _env;

        public FirstPageController(IHostingEnvironment env)
        {
            _env = env;
        }


        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentPlayerName = User.Identity.Name;
            }
            else
            {
                ViewBag.CurrentPlayerName = "";

            }


            ViewBag.Warning = "";
            return View();
        }

        [HttpPost]
        public string OnlinePlayers(string id)
        {
            List<PlayerClass> playerList = JsonConvert.DeserializeObject<List<PlayerClass>>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, "Players.json")));
            if (playerList == null)
            {
                return "noplayers";
            }
            else
            {
                var player = playerList.FirstOrDefault();
                return player.PlayerName;
            }
        }


        [HttpPost]
        public IActionResult Index(string PlayerName)
        {

            if (PlayerName == null)
            {
                ViewBag.Warning = "Dude! A Cool name is all we are asking";
                return View();
            }

            PlayerClass temp = new PlayerClass() { PlayerName = PlayerName, PlayerID = DateTime.Now.Ticks.ToString() };

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



                ViewBag.PlayerName = temp.PlayerName;
                ViewBag.PlayerID = temp.PlayerID.ToString();
                ViewBag.IfConnected = "False";
                return View("WaitingForPlayer");

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


                ViewBag.PlayerName = temp.PlayerName;
                ViewBag.PlayerID = temp.PlayerID.ToString();
                ViewBag.IfConnected = "True";
                return View("WaitingForPlayer");
            }
        }


        [HttpPost]
        public string Delete(string id)
        {
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, "Players.json"));
            System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, "Players.json"), "");
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, id + ".json"));
            return "Deleted";
        }

        [HttpPost]
        public string DeleteMe(string id)
        {
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, id + ".json"));
            return "Deleted";
        }


        [HttpPost]
        public string WaitingForPlayer(string ID)
        {
            var temp = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
            if (temp.IfConnected == "True")
            {
                temp.IfAlive = "True";
                string gamedata = JsonConvert.SerializeObject(temp);
                System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, temp.MyID + ".json"), gamedata);
                return "Tru";
            }
            else
                return "Fals";
        }


    }
}