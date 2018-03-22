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
    public class HomeMController : Controller
    {

        private IHostingEnvironment _env;

        public HomeMController(IHostingEnvironment env)
        {
            _env = env;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string PlayerName, string PlayerID)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, PlayerID + ".json")));
            ViewBag.MyName = Player.MyName;
            ViewBag.MyID = Player.MyID;
            ViewBag.MySymbol = Player.MySymbol;
            ViewBag.OpponentName = Player.OpponentName;
            ViewBag.Turn = Player.Turn;

            return View();
        }

        [HttpPost]
        public string checkIfOpponentAlive(string ID)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));
            if (Opponent.IfAlive == "True")
            {
                return "Tru";
            }
            else if (Opponent.IfAlive == "False")
            {
                System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
                System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
                return "Fals";
            }
            else
            {
                return "Fals";
            }
        }

        [HttpPost]
        public JsonResult checkStatus(string ID)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, ID + ".json")));
            string s = JsonConvert.SerializeObject(Player);
            var teRes = Json(Player);
            return teRes;
        }

        [HttpPost]
        public string updateStatus(string updatedJson)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(updatedJson);
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));

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
            System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"), gamedataPlayer);



            string gamedataOpponent = JsonConvert.SerializeObject(Opponent);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
            System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"), gamedataOpponent);

            return "Okay";
        }

        [HttpPost]
        public string Delete(string id)
        {
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, id + ".json"));
            return "Deleted";
        }

        [HttpPost]
        public string DeleteQuit(string id)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, id + ".json")));
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
            return "Deleted";
        }

        [HttpPost]
        public string PlayerQuit(string id)
        {
            var Player = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, id + ".json")));
            Player.IfQuit = "True";
            var Opponent = JsonConvert.DeserializeObject<GameDataClass>(System.IO.File.ReadAllText(System.IO.Path.Combine(_env.WebRootPath, Player.OpponentID + ".json")));
            Opponent.IfQuit = "True";

            string gamedataOpponent = JsonConvert.SerializeObject(Opponent);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"));
            System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, Opponent.MyID + ".json"), gamedataOpponent);

            string gamedataPlayer = JsonConvert.SerializeObject(Player);
            System.IO.File.Delete(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"));
            System.IO.File.WriteAllText(System.IO.Path.Combine(_env.WebRootPath, Player.MyID + ".json"), gamedataPlayer);
            return "Deleted";
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}