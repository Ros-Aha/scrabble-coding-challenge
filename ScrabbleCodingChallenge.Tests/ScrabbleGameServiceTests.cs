namespace ScrabbleCodingChallenge.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using FluentAssertions;
    using ScrabbleCodingChallenge.Services;
    using ScrabbleCodingChallenge.Models;

    namespace ScrabbleGame.Tests
    {
        public class ScrabbleGameServiceTests
        {
            private readonly ScrabbleGameService _scrabbleGameService;
            private readonly List<Tile> _tiles;
            private readonly HashSet<string> _twoLetterWords;

            public ScrabbleGameServiceTests()
            {
                _scrabbleGameService = new ScrabbleGameService();
                _tiles = _scrabbleGameService.GetAllTilesShuffled();
                _twoLetterWords = _scrabbleGameService.GetAllTwoLetterWords();
            }

            [Fact]
            public void GetAllTilesShuffled_ShouldReturnShuffledList()
            {
                //act
                var actualTiles = _scrabbleGameService.GetAllTilesShuffled();

                //assert
                actualTiles.Should().NotBeNull();
                actualTiles.Count.Should().Be(100);

                //an assertion to check each tile has the correct count should be here too.
            }

            [Fact]
            public void GetAllTwoLetterWords_ShouldReturnAllExpectedWords()
            {
                //Arrange
                var scrabbleGameService = new ScrabbleGameService();

                var expectedWords = new HashSet<string>
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

                //act
                var result = scrabbleGameService.GetAllTwoLetterWords();

                //assert
                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(expectedWords);
            }

            [Fact]
            public void DealSevenTiles_ShouldGiveSevenTilesToEachPlayer()
            {
                //arrange
                List<Tile> playerOneTiles = new List<Tile>();
                List<Tile> playerTwoTiles = new List<Tile>();

                //act
                _scrabbleGameService.DealSevenTiles(_tiles, playerOneTiles, playerTwoTiles);

                //assert
                playerOneTiles.Should().HaveCount(7);
                playerTwoTiles.Should().HaveCount(7);
            }

            [Fact]
            public void FindValidTwoLetterWords_ShouldFindValidWordsForGivenTiles()
            {
                //arrange
                List<Tile> tiles = new List<Tile>
                {
                    new Tile('A', 1, 1),
                    new Tile('B', 3, 1)
                };

                //act
                var foundWords = _scrabbleGameService.FindValidTwoLetterWords(tiles, _twoLetterWords);

                //assert
                foundWords.Should().Contain("ab (4)");
            }

            [Fact]
            public void FindValidTwoLetterWords_ShouldIncludeWildcardCombinations()
            {
                //arrange
                List<Tile> tiles = new List<Tile>
                {
                    new Tile('?', 0, 1),
                    new Tile('B', 3, 1)
                };

                //act
                var foundWords = _scrabbleGameService.FindValidTwoLetterWords(tiles, _twoLetterWords);

                //assert
                foundWords.Should().Contain("ab (3)");
                foundWords.Should().Contain("ba (3)");
                foundWords.Should().Contain("be (3)");
                foundWords.Should().Contain("bi (3)");
                foundWords.Should().Contain("bo (3)");
                foundWords.Should().Contain("by (3)");
                foundWords.Should().NotContain("?? (0)");
            }

            //more tests needed
        }
    }

}