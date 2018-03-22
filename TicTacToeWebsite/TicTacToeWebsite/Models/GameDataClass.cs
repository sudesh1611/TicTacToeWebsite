using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeWebsite.Models
{
    public class GameDataClass
    {
        public string MyName { get; set; }
        public string MyID { get; set; }
        public string OpponentName { get; set; }
        public string OpponentID { get; set; }
        public int[] IfClicked { get; set; }
        public string[] SymbolPresent { get; set; }
        public string MySymbol { get; set; }
        public string OpponentSymbol { get; set; }
        public string IfConnected { get; set; }
        public string Turn { get; set; }
        public string Status { get; set; }
        public string IfAlive { get; set; }
        public string IfQuit { get; set; }
    }
}
