﻿namespace Rainmaker.Model.Borrower
{
    public class EthnicInfoItem
    {
        public int? EthnicId { get; }
        public int? EthnicDetailId { get; }


        public EthnicInfoItem(int? ethnicId,
                              int? ethnicDetailId)
        {
            EthnicId = ethnicId;
            EthnicDetailId = ethnicDetailId;
        }

    }
}
