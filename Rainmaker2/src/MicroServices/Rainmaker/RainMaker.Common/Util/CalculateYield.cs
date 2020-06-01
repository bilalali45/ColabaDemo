//using System;

//namespace RainMaker.Common.Util
//{
//    public class CalculateYield
//    {
//        public static double GetYieldRate(DateTime settlementDateValue, int maturityYears, double rate, double price)
//        {
//            var xl = new Microsoft.Office.Interop.Excel.Application();
//            //var wsf = xl.WorksheetFunction;
            
//            var settlementDate = xl.Evaluate("=DATEVALUE(" + '"' + settlementDateValue.ToShortDateString() + '"' + ")");

//            var maturityDate = xl.Evaluate("=" + '"' + settlementDateValue + '"' + "+(365*" + maturityYears + ")");

//            const int redemption = 100;
//            const int frequency = 1;

//            var resultInPercentage =
//                xl.Evaluate(string.Format("=YIELD({0},{1},{2},{3},{4},{5})", settlementDate, maturityDate, rate / 100, price, redemption, frequency, 0)) * 100;

//            return resultInPercentage;
//        }
//    }
//}
