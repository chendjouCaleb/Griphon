namespace Griphon.Utilities.MeasureUnits
{
    public class InchMeasureUnit: IMeasureUnit
    {
        public string Symbol => "inch";

        public double ToPixel(double value) => value * 96;

        public double FromPixel(double value) => value / 96;
    }
}