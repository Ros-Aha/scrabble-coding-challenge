using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleCodingChallenge.Models
{
    public class Tile(char letter, int points, int count)
    {
        public char Letter { get; } = letter;
        public int Points { get; } = points;
        public int Count { get; set; } = count;
    }
}
