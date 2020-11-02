using System.ComponentModel;

namespace ByteWebConnector.Model.Enums.Rainmaker
{
    public enum OwnershipTypeEnum
    {
        [Description(description: "Own")] Own = 1,
        [Description(description: "Rent")] Rent = 2,
        [Description(description: "Others")] Other = 3
    }
}