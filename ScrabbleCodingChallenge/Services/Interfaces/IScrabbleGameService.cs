using ScrabbleCodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleCodingChallenge.Services.Interfaces
{
    public interface IScrabbleGameService
    {
        public void RunGame();
        //public List<Tile> GetAllTilesShuffled();
        //public HashSet<string> GetAllTwoLetterWords();
        //public void FindAndDisplayTwoLetterWords(List<Tile> tiles, HashSet<string> twoLetterWords);
        //public void DealSevenTiles(List<Tile> tiles, List<Tile> playerOneTiles, List<Tile> playerTwoTiles);
    }
}
