namespace Griphon.Parser.Tokens
{
    public class CharToken:Token
    {
        public CharToken(char character, TokenIndex index)
        {
            Char = character;
            Index = index;
        }
        public char Char { get; set; }
    }
}