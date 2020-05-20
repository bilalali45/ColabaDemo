using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainMaker.Common.Util
{
    public class CalculationFormulas
    {
        public static decimal Ltv(decimal loanAmount, decimal propertyValue)
        {
            if (propertyValue <= 0) return 0;
            return ((loanAmount / propertyValue) * 100);
        }
        public static double Ltv(double loanAmount, double propertyValue)
        {
            if (propertyValue <= 0) return 0;
            return ((loanAmount / propertyValue) * 100);
        }
        public static double Cltv(double loanAmount, double propertyValue, double? secondLienBalance)
        {
            if (propertyValue <= 0) return 0;
            return ((loanAmount + (secondLienBalance ?? 0)) / propertyValue) * 100;
        }


        public static decimal Cltv(decimal loanAmount, decimal propertyValue, decimal? secondLienBalance)
        {
            if (propertyValue <= 0) return 0;
            return ((loanAmount + (secondLienBalance ?? 0)) / propertyValue) * 100;
        }


        public static double Hltv(double loanAmount, double propertyValue, double? mortgageToBeSubordinate)
        {
            if (propertyValue <= 0) return 0;
            return ((loanAmount + (mortgageToBeSubordinate ?? 0)) / propertyValue) * 100;
        }


        public static decimal Hltv(decimal loanAmount, decimal propertyValue, decimal? mortgageToBeSubordinate)
        {
            if (propertyValue <= 0) return 0;
            return ((loanAmount + (mortgageToBeSubordinate ?? 0)) / propertyValue) * 100;
        }
    }
}
