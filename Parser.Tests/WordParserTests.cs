using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Parser.Tests
{
    [TestFixture]
    public class WordParserTests
    {
        private WordParser _wordParser;

        [SetUp]
        public void SetUp()
        {
            _wordParser = new WordParser();
        }

        [Test]
        public void Should_Create_An_Empty_List_When_Provided_With_An_Empty_String()
        {
            string content = "";
            var uniqueWords = _wordParser.GetUniqueWords(content);
            var expected = new List<string>();
            Assert.AreEqual(expected, uniqueWords);
        }

        [Test]
        public void Should_Create_A_List_Of_Words_When_Provided_With_A_Non_Empty_String()
        {
            string content = "sells";
            var words = _wordParser.GetUniqueWords(content);
            var expected = new List<string> { "sells" };
            Assert.AreEqual(expected, words);
        }

        [Test]
        public void Should_Remove_Special_Characters_When_Provided_With_Them()
        {
            string content = "she sells seashells, by the seashore. the shells she sells, are surely seashells!";
            var wordsWithoutSpecialCharacters = _wordParser.GetUniqueWords(content);
            Assert.That(!wordsWithoutSpecialCharacters.Contains("seashells."));
            Assert.That(!wordsWithoutSpecialCharacters.Contains("seashells,"));
        }

        [Test]
        public void Should_Create_A_List_Of_Unique_Words_When_Provided_With_A_String()
        {
            string content = "she sells seashells, she sells seashells. seashore ";
            var uniqueWords = _wordParser.GetUniqueWords(content);
            var expected = new List<string> { "she", "sells", "seashells", "seashore" };

            Assert.AreEqual(expected, uniqueWords);
        }

        [Test]
        public void Should_Remove_Words_From_IgnoreList_When_Provided_With_IgnoreList()
        {
            string content = "she sells seashells, she sells seashells. seashore ";
            var ignoreWords = new List<string> { "sells" };
            var expected = new List<string> { "she", "seashells", "seashore" };

            var uniqueWords = _wordParser.GetUniqueWords(content);
            var ignoreWordsRemoved = _wordParser.RemoveIgnoreWords(uniqueWords.ToList(), ignoreWords);

            Assert.AreEqual(expected, ignoreWordsRemoved);
        }

        [Test]
        public void Should_Remove_Words_From_IgnoreList_When_Provided_With_Case_Insenstive_Words()
        {
            //example 
            //If ignore word has "she", then "She" should be removed from list
            string content = "she sells seashells. She sells seashells. seashore ";
            var ignoreWords = new List<string> { "she" };

            var expected = new List<string> { "sells", "seashells", "seashore" };

            var uniqueWords = _wordParser.GetUniqueWords(content);
            var result = _wordParser.RemoveIgnoreWords(uniqueWords, ignoreWords);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Should_Return_Same_Unique_Words_When_IgnoreList_Is_Empty()
        {
            string content = "she sells seashells, she sells seashells. seashore ";
            var ignoreWords = new List<string>();
            var expected = new List<string> { "she", "sells", "seashells", "seashore" };
            var uniqueWords = _wordParser.GetUniqueWords(content);

            var ignoreWordsRemoved = _wordParser.RemoveIgnoreWords(uniqueWords.ToList(), ignoreWords);
            Assert.AreEqual(expected, ignoreWordsRemoved);
        }
        [Test]
        public void Should_Remove_Numbers_When_Provided_With_A_String()
        {
            var content = "she sells 1 seashells, she sells 9999 seashells. seashore 22";
            var results = _wordParser.GetUniqueWords(content);

            var expected = new List<string> { "she", "sells", "seashells", "seashore" };

            Assert.AreEqual(expected, results);
        }

        [Test]
        public void Should_Create_A_List_Of_Words_In_Alphabetical_Order_When_Provided_With_A_List_Of_Strings()
        {
            string content = "she sells seashells, she sells seashells. seashore ";
            var result = _wordParser.GetUniqueWordsInAlphabeticalOrder(content);
            var expected = new List<string> { "she", "sells", "seashells", "seashore" };

            expected.Sort(string.Compare);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Should_Remove_DuplicateWords_When_Provided_With_A_List_Of_strings_With_Same_Word_In_Different_Word_Casing()
        {
            string content = "she sells seashells, She sells seashells. seashore ";
            var result = _wordParser.GetUniqueWords(content);
            var expected = new List<string> { "she", "sells", "seashells", "seashore" };

            Assert.AreEqual(expected, result);
        }

        [TearDown]
        public void TearDown()
        {
            _wordParser = null;
        }
    }
}
