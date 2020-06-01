using Twilio.Rest.Api.V2010.Account;

namespace RainMaker.Common.Extensions.Twilio
{
    public static class CallResourceExtensions
    {
        public static bool IsInbound(this CallResource call)
        {
            if (call.Direction.Contains("inbound")) return true;

            return false;
        }
    }
}