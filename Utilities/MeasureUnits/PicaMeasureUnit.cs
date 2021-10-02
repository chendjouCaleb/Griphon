namespace Griphon.Utilities.MeasureUnits
{
    public class PicaMeasureUnit:IMeasureUnit
    {
        public string Symbol => "pc";
        public double ToPixel(double value) => value * 16;

        public double FromPixel(double value) => value / 16;
    }
}