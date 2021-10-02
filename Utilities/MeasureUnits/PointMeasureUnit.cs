namespace Griphon.Utilities.MeasureUnits
{
    public class PointMeasureUnit: IMeasureUnit
    {
        public string Symbol => "pt";
        public double ToPixel(double value) => value * 1.333333333;
        public double FromPixel(double value) => value /1.333333333;
    }
}