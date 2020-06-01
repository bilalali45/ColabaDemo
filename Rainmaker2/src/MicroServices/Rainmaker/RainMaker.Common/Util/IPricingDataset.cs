using System.Collections.Generic;
using System.Data;

namespace RainMaker.Common.Util
{
    public interface IPricingDataset
    {
        Dictionary<string, string> FillGfeData(int priceId, int oppId, int loanRequestId);
        Dictionary<string, string> FillTilData(int priceId, int oppId, int loanRequestId);
        Dictionary<string, string> FillLeData(int priceId, int oppId, int loanRequestId);
        List<PlotFeeItem> CalPlotFeeItems(int documentId, string key, int priceId);
        DataTable GetPriceDataListbyPriceId(int priceId);
    }
}