namespace Griphon.Utilities.MeasureUnits
{
    public interface IMeasureUnit
    {
        string Symbol { get; }

        double ToPixel(double value);

        double FromPixel(double value);
    }
}