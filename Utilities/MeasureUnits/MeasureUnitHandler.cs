using System;
using System.Collections.Generic;

namespace Griphon.Utilities.MeasureUnits
{
    public class MeasureUnitHandler
    {
        private List<IMeasureUnit> measureUnits = new List<IMeasureUnit>();

        public MeasureUnitHandler()
        {
            measureUnits.AddRange(new IMeasureUnit[]
            {
                new CentimeterMeasureUnit(), 
                new MillimeterMeasureUnit(), 
                new InchMeasureUnit(),
                new PicaMeasureUnit(), 
                new PointMeasureUnit()
            });
        }


        public IEnumerable<string> GetAllMeasure()
        {
            return measureUnits.ConvertAll(e => e.Symbol);
        }


        public double ConvertToPixel(string value)
        {
            throw new NotImplementedException();
        }


        public double ConvertToPixel(int value, string measureUnit)
        {
            throw new NotImplementedException();
        }


        public double Convert(double value, string from, string to)
        {
            var fromUnit = measureUnits.Find(l => l.Symbol == from);
            var toUnit = measureUnits.Find(l => l.Symbol == to);

            double toPixel = fromUnit.ToPixel(value);
            double result = toUnit.FromPixel(toPixel);

            return result;
        }
    }
}