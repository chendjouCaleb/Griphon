using System;
using Griphon.Parser;
using NUnit.Framework;

namespace ParserTest
{
    public class ParseStreamTests
    {
        [Test]
        public void Create()
        {
            ParseStream stream = new ParseStream("/text");
            
            Assert.AreEqual(stream.Text, "/text");
            Assert.AreEqual(stream.Index.Line, 0);
            Assert.AreEqual(stream.Index.Index, 0);
            Assert.AreEqual(stream.Current, '/');
            Assert.False(stream.Launched);
        }

        [Test]
        public void NextShouldIncrementIndex()
        {
            ParseStream stream = new ParseStream("/text");
            stream.Next();
            
            Assert.AreEqual(stream.Index.Index, 1);
        }

        [Test]
        public void CurrentCharShouldTheCharOfTheIndex()
        {
            ParseStream stream = new ParseStream("/text");
            stream.Next();
            
            Assert.AreEqual(stream.Current, 't');
        }


        [Test]
        public void HasMoreShouldBeTrue_WhenIndex_IsLowerThanLength()
        {
            ParseStream stream = new ParseStream("/text");
            stream.Next();
            Assert.True(stream.HasMore);
        }
        
        [Test]
        public void HasMoreShouldBeFalse_WhenIndex_IsAtTheLastCharacter()
        {
            ParseStream stream = new ParseStream("/text");
            stream.Next();
            stream.Next();
            stream.Next();
            stream.Next();

            Console.WriteLine(stream.Text.Length + "=" + stream.Index.Index);
            Assert.AreEqual(stream.Current, 't');
            Assert.False(stream.HasMore);
        }

        [Test]
        public void NextCantMove_When_LastCharacterIsReached()
        {
            ParseStream stream = new ParseStream("/t");
            stream.Next();
            stream.Next();
            stream.Next();
            
            Assert.AreEqual(stream.Index.Index, 1);
        }

        [Test]
        public void Forward_ShouldReturn_TheCurrentCharacter()
        {
            ParseStream stream = new ParseStream("/text");
            Assert.AreEqual('t', stream.Forward());
        }
        
        [Test]
        public void Forward_ShouldReturnNullChar_When_LastCharacterIsReached()
        {
            ParseStream stream = new ParseStream("/t");
            stream.Forward();
            Assert.AreEqual('\0', stream.Forward());
        }
    }
}