using System.Collections.Generic;

namespace RainMaker.Common.Util
{
    /// <summary>
    /// Handler providing functionality for blocking user access 
    /// to a web site based on IP-address.
    /// </summary>
    /// 
    /// 

    public class BlockedIpHandler
    {
        public bool IsIpBlocked(string ip, List<QueryTemplate.BlacListIPs> blockList)
        {
            var retType = false;

            var ipAdd = CommonHelper.IpToInt(ip);
            
            if (ipAdd > 0)
            {
                retType = !blockList.Exists(x =>x.IsAllow && x.IpFrom <= ipAdd && x.IpTo >= ipAdd);

                if(retType)
                    retType = blockList.Exists(x => x.IsAllow == false && x.IpFrom <= ipAdd && x.IpTo >= ipAdd);    
            }

            return retType;
        }


    }
}


