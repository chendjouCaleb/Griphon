namespace Griphon.Parser
{
    public static class StringHelpers
    {
        public static bool IsLetter(char value)
        {
            return (value >= 'a' && value <= 'z') || (value >= 'A' && value <= 'Z');
        }
    }
}