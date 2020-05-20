using System.Collections.Generic;

namespace RainMaker.Common.Util
{
    public interface ISecurityService
    {
        List<AclWrapper> GetAclEntriesForType(int entitytypeId, int userId);
        AclWrapper GetAcLFor(int entitytypeId, int entityId, int userId);
        bool IsApplicableForAcl(int entitytypeId);
        void InsertAClEntry(AclWrapper aclWrapper);
    }
}