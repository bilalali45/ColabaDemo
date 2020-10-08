using System;
using System.Collections.Generic;
using System.Linq;

namespace RainMaker.Common.Util
{
    public class AmortizationSchedule
    {
        public int PaymentNo { get; set; }
        public int PaymentDate { get; set; }
        public double Rate { get; set; }
        public double Pi { get; set; }
        public double MinPi { get; set; }
        public double MaxPi { get; set; }
        public double Mi { get; set; }
        public double Escrow { get; set; }
        public double TotalPayment { get; set; }
        public double Balance { get; set; }
        public double PrincipalPaid { get; set; }
        public double InterestPaid { get; set; }
        public int PaymentGroupNo { get; set; }
    }

    public class PaymentGroup
    {
        public int PaymentNoFrom { get; set; }
        public int PaymentNoTo { get; set; }
        public double Rate { get; set; }
        public double Pi { get; set; }
        public double MinPi { get; set; }
        public double MaxPi { get; set; }
        public double Mi { get; set; }
        public double Escrow { get; set; }
        public double TotalPayment { get; set; }
        public double PrincipalPaid { get; set; }
        public double InterestPaid { get; set; }
        public int PaymentGroupNo { get; set; }
    }

    public static class MortgageCalculator
    {
        private static IList<AmortizationSchedule> _paymentSchedule;
        public static double GetPayment(double principal, int months, double interest)
        {

            var rate = interest / 1200;
            var monthly = rate > 0 ? ((rate + rate / (Math.Pow(1 + rate, months) - 1)) * principal) : principal / months;
            return monthly;
        }

        public static void GenratePaymentSchedule(double propertyValue, double loanAmount, double interest, int months,double escrow = 0, double mi = 0, int miCutoff = 78, int monthBeforeFirstAdjustment = 0, double indexRate = 0, double lifeTimeCap = 0, double margin = 0)
        {
            double pi = GetPayment(loanAmount, months, interest);
            int group = 1;
            bool isFixed = true;
            double interestArm = 0;
            double minPi = 0;
            double maxPi = 0;
            double minInt = 0;
            double maxInt = 0;
            if (monthBeforeFirstAdjustment > 0 && monthBeforeFirstAdjustment < months)
            {
                isFixed = false;
                interestArm = indexRate + margin;
                var interest3 = interest + lifeTimeCap;
                if (interest3 < interestArm)
                    interestArm = interest3;
                minInt = margin;
                maxInt = interest3;

            }
            _paymentSchedule = new List<AmortizationSchedule>();
            var endingBalance = loanAmount;
            var rate = interest / 1200.0;
            var count = 1;
            while (count <= months)
            {
                if (!isFixed && count == (monthBeforeFirstAdjustment + 1))
                {
                    pi = GetPayment(endingBalance, (months - (count - 1)), interestArm);
                    minPi = GetPayment(endingBalance, (months - (count - 1)), minInt);
                    maxPi = GetPayment(endingBalance, (months - (count - 1)), maxInt);
                    interest = interestArm;
                    rate = interest / 1200;
                    group++;
                }
                if (mi > 0)
                {
                    double pv = endingBalance / propertyValue * 100;
                    if (pv < miCutoff)
                    {
                        mi = 0;
                        group++;
                    }
                }
                var interestPaid = endingBalance * rate;
                var principlePaid = pi - interestPaid;
                endingBalance -= principlePaid;

                _paymentSchedule.Add(new AmortizationSchedule { PaymentNo = count, Rate = interest, Pi = pi, Mi = mi, Escrow = escrow, TotalPayment = pi + mi + escrow, Balance = endingBalance, InterestPaid = interestPaid, PrincipalPaid = principlePaid, PaymentGroupNo = group,MinPi = minPi,MaxPi = maxPi});
                count++;
            }
        }
        //public static double CalculateApr(double loanAmount, double totalAprableFees)
        //{
        //    var cashFlow = _paymentSchedule.Select(x => x.TotalPayment).ToList();
        //    cashFlow.Insert(0, (loanAmount - totalAprableFees) * -1);
        //    double[] cashFlowArray = cashFlow.ToArray();
        //    double tmpIrr = Financial.IRR(ref cashFlowArray, 0.001);
        //    return tmpIrr * 1200;
        //}

        public static IList<PaymentGroup> GetPaymentGroup()
        {
            var cashFlow =
                _paymentSchedule.GroupBy(p => p.PaymentGroupNo).Select(s => new PaymentGroup
                {
                    PaymentGroupNo = s.Key,
                    PaymentNoFrom = s.Min(m => m.PaymentNo),
                    PaymentNoTo = s.Max(m => m.PaymentNo),
                    MinPi = s.Min(m => m.MinPi),
                    MaxPi = s.Max(m => m.MaxPi),
                    Pi = s.Max(m => m.Pi),
                    Mi = s.Max(m => m.Mi),
                    Escrow = s.Max(m => m.Escrow),
                    TotalPayment = s.Max(m => m.TotalPayment),
                    Rate = s.Max(m => m.Rate),
                    PrincipalPaid = s.Sum(m => m.PrincipalPaid),
                    InterestPaid = s.Sum(m => m.InterestPaid)
                });

            return cashFlow.ToList();

        }
    }
}
