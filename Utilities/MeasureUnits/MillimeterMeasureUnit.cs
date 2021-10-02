namespace Griphon.Utilities.MeasureUnits
{
    public class MillimeterMeasureUnit: IMeasureUnit
    {
        public string Symbol => "mm";
        public double ToPixel(double value) => value * 3.7795275590551185;

        public double FromPixel(double value) => value / 3.7795275590551185;
    }
}