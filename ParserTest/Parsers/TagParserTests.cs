using System;
using Griphon.Parser;
using Griphon.Parser.Parsers;
using Griphon.Parser.Tokens;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ParserTest.Parsers
{
    
    public class TagParserTests
    {
        private string text = "/text";
        private ParseStream stream;

        [SetUp]
        public void BeforeEach()
        {
            stream = new ParseStream(text);
        }
        
        [Test]
        [TestCase("/text", "/text", "text", 4)]
        [TestCase("/text1", "/text1", "text1", 5)]
        [TestCase("/text-1", "/text-1", "text-1", 6)]
        [TestCase("/text-1[]", "/text-1", "text-1", 7)]
        [TestCase("/text-1{}", "/text-1", "text-1", 7)]
        [TestCase("/text-1{} ", "/text-1", "text-1", 7)]
        [TestCase("/text-1{}\n", "/text-1", "text-1", 7)]
        [TestCase("/text-1{}\t", "/text-1", "text-1", 7)]
        [TestCase("/text-1{}\r", "/text-1", "text-1", 7)]
        public void Create(string codeText, string value, string tagName, int index)
        {
            ParseStream parseStream = new ParseStream(codeText);
            TagParser parser = new TagParser(parseStream);
            TagToken token = parser.Parse();
            
            Assert.AreEqual(value, token.Value);
            Assert.AreEqual(tagName, token.TagName);
            Assert.AreEqual(index, parseStream.Index.Index);
        }

        [Test]
        public void TokenShouldHaveCorrectValue()
        {
            TagParser parser = new TagParser(stream);
            Assert.AreEqual("/text", parser.Parse().Value);
        }

        [Test]
        public void TokenShouldHaveCorrectTagName()
        {
            TagParser parser = new TagParser(stream);
            Assert.AreEqual("text", parser.Parse().TagName);
        }

        [Test]
        public void TokenShouldHaveHaveStartIndexHasIndex()
        {
            TagParser parser = new TagParser(stream);
            TokenIndex startIndex = parser.Index;

            TagToken token = parser.Parse();
            
            Assert.AreEqual(startIndex.Index, token.Index.Index);
        }
        

        [Test(Description = "Should throw exception if the current char is not /")]
        public void ShouldThrow_WhenCurrentCharIsIncorrect()
        {
            //Move cursor on char 't' after the /.
            stream.Next();
            TagParser parser = new TagParser(stream);

            Assert.Catch<InvalidOperationException>(() => parser.Parse());
        }

        [Test]
        public void ShouldThrowError_WhenTheSecondCharIsNot_Letter()
        {
            ParseStream parseStream = new ParseStream("/1ab");
            TagParser parser = new TagParser(parseStream);

            UnexpectedTokenException ex = Assert.Throws<UnexpectedTokenException>(() => parser.Parse());
            
            Assert.AreEqual(1, ex.TokenIndex.Index);
            Assert.AreEqual('1', ex.Character);
        }
    }
}