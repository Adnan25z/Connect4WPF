using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    /// <summary>
    /// Human Player class inherits from player and is used to set the human player 
    /// </summary>
    public class HumanPlayer:Player
    {
        public HumanPlayer(string name):base(name)
        {

        }
    }
}
