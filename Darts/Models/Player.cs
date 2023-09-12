using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darts.Models
{
    internal class Player
    {
        private int score = 0;

        public int Score => score;
        public string Name { get; }
    }
}
