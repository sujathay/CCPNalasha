using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCP_Nalashaa_WebAPI.Models
{
    public static class NumberExtensions
    {
        public static double TruncateDecimalPlaces(this double val, int places)
        {
            if (places < 0)
            {
                throw new ArgumentException("places");
            }
            return Math.Round(val - Convert.ToDouble((0.5 / Math.Pow(10, places))), places);
        }
    }
}