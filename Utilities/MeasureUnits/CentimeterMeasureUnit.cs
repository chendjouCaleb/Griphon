namespace Griphon.Utilities.MeasureUnits
{
    public class CentimeterMeasureUnit: IMeasureUnit
    {
        public string Symbol => "cm";
        public double ToPixel(double value) => value * 37.795275590551185;

        public double FromPixel(double value) => value / 37.795275590551185;
    }
}