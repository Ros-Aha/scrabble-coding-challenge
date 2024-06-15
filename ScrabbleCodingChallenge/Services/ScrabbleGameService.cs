using ScrabbleCodingChallenge.Extensions;
using ScrabbleCodingChallenge.Models;
using ScrabbleCodingChallenge.Services.Interfaces;

namespace ScrabbleCodingChallenge.Services
{
    public class ScrabbleGameService : IScrabbleGameService
    {
        public ScrabbleGameService() { }

        public void RunGame()
        {
            var tiles = GetAllTilesShuffled();
            var allTwoLetterWords = GetAllTwoLetterWords();

            List<Tile> playerOneTiles = new List<Tile>();
            List<Tile> playerTwoTiles = new List<Tile>();

            DealSevenTiles(tiles, playerOneTiles, playerTwoTiles);

            //display the tiles for each player
            Console.WriteLine("Player 1: " + string.Join("", playerOneTiles.ConvertAll(tile => tile.Letter)));
            Console.WriteLine("Player 2: " + string.Join("", playerTwoTiles.ConvertAll(tile => tile.Letter)));

            //find and display valid two-letter words and their scores for each player
            Console.WriteLine("Player 1 two-letter words:");
            var playerOneWords = FindValidTwoLetterWords(playerOneTiles, allTwoLetterWords);
            Console.WriteLine(string.Join(" ", playerOneWords));

            Console.WriteLine("Player 2 two-letter words:");
            var playerTwoWords = FindValidTwoLetterWords(playerTwoTiles, allTwoLetterWords);
            Console.WriteLine(string.Join(" ", playerTwoWords));
        }
        public List<Tile> GetAllTilesShuffled()
        {
            var tiles = new List<Tile>()
            {
                new('A', 1, 9), new('B', 3, 2), new('C', 3, 2),
                new('D', 2, 4), new('E', 1, 12), new ('F', 4, 2),
                new('G', 2, 3), new('H', 4, 2), new ('I', 1, 9),
                new('J', 8, 1), new('K', 5, 1), new ('L', 1, 4),
                new('M', 3, 2), new('N', 1, 6), new ('O', 1, 8),
                new('P', 3, 2), new('Q', 10, 1), new ('R', 1, 6),
                new('S', 1, 4), new('T', 1, 6), new ('U', 1, 4),
                new('V', 4, 2), new('W', 4, 2), new ('X', 8, 1),
                new('Y', 4, 2), new('Z', 10, 1),
                new('?', 0, 2)
            };

            //create the pool of tiles based on the count
            List<Tile> tilePool = new();
            foreach (var tile in tiles)
            {
                for (int i = 0; i < tile.Count; i++)
                {
                    tilePool.Add(new Tile(tile.Letter, tile.Points, 1));
                }
            }

            // Shuffle the tiles
            tilePool.Shuffle();

            return tilePool;
        }

        public HashSet<string> GetAllTwoLetterWords()
        {
            return new HashSet<string>
            {
                "AA", "AB", "AD", "AE", "AG", "AH", "AI", "AL", "AM", "AN", "AR", "AS", "AT", "AW", "AX", "AY",
                "BA", "BE", "BI", "BO", "BY",
                "DA", "DO",
                "ED", "EF", "EH", "EL", "EM", "EN", "ER", "ES", "ET", "EW", "EX",
                "FA", "GO", "HA", "HE", "HI", "HO", "ID", "IF", "IN", "IS", "IT",
                "JO", "KA", "LA", "LI", "LO",
                "MA", "ME", "MI", "MU", "MY",
                "NA", "NO",
                "OD", "OE", "OF", "OH", "OI", "OM", "ON", "OP", "OR", "OS", "OW", "OX",
                "PA", "PE", "PI",
                "RE", "SH", "SI", "SO",
                "TA", "TO", "UH", "UM", "UN", "UP", "US", "UT",
                "WE", "WO",
                "XI",
                "YE"
            };
        }
        
        //It is not clear this method should exclude duplicates
        // e.g for ?NETGIN you can create it (2) and it (1)
        // the example output in the task however seems to remove duplicates even if a combination is possible with a wildcard
        public List<string> FindValidTwoLetterWords(List<Tile> tiles, HashSet<string> twoLetterWords)
        {
            List<string> validWords = new List<string>();

            for (int i = 0; i < tiles.Count; i++)
            {
                //loop through each combination
                for (int j = 0; j < tiles.Count; j++)
                {
                    if (i != j)
                    {
                        string word = $"{tiles[i].Letter}{tiles[j].Letter}".ToUpper();
                        if (twoLetterWords.Contains(word))
                        {
                            int score = tiles[i].Points + tiles[j].Points;
                            validWords.Add($"{word.ToLower()} ({score})");
                        }

                        //if either tile is a blank tile, replace it with each letter from A to Z
                        if (tiles[i].Letter == '?')
                        {
                            for (char c = 'A'; c <= 'Z'; c++)
                            {
                                word = $"{c}{tiles[j].Letter}".ToUpper();
                                if (twoLetterWords.Contains(word))
                                {
                                    validWords.Add($"{word.ToLower()} ({tiles[j].Points})");
                                }
                            }
                        }
                        if (tiles[j].Letter == '?')
                        {
                            for (char c = 'A'; c <= 'Z'; c++)
                            {
                                word = $"{tiles[i].Letter}{c}".ToUpper();
                                if (twoLetterWords.Contains(word))
                                {
                                    validWords.Add($"{word.ToLower()} ({tiles[i].Points})");
                                }
                            }
                        }
                    }
                }
            }

            return validWords;
        }

        public void DealSevenTiles(List<Tile> tiles, List<Tile> playerOneTiles, List<Tile> playerTwoTiles)
        {
            for (int i = 0; i < 7; i++)
            {
                playerOneTiles.Add(tiles[0]);
                tiles.RemoveAt(0);

                playerTwoTiles.Add(tiles[0]);
                tiles.RemoveAt(0);
            }
        }
    }
}
