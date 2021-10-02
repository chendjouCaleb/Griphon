namespace StyleSheet
{
    public class Margin
    {
        public Margin(uint margin)
        {
            Top = margin;
            Bottom = margin;
            Left = margin;
            Right = margin;
        }


        public Margin(uint yMargin, uint xMargin)
        {
            Top = yMargin;
            Bottom = yMargin;
            Left = xMargin;
            Right = xMargin;
        }

        public Margin(uint topMargin, uint rightMargin, uint bottomMargin, uint leftMargin)
        {
            Top = topMargin;
            Bottom = bottomMargin;
            Left = leftMargin;
            Right = rightMargin;
        }

        public Margin(uint topMargin, uint xMargin, uint bottomMargin)
        {
            Top = topMargin;
            Bottom = bottomMargin;
            Left = xMargin;
            Right = xMargin;
        }
        
        public uint Top { get; set; }

        public uint Left { get; set; }

        public uint Bottom { get; set; }

        public uint Right { get; set; }
    }
}