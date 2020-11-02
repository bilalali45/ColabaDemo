using System.ComponentModel;

namespace ByteWebConnector.Model.Enums.Rainmaker
{
    public enum JobTypeEnum
    {
        [Description("Full-Time")]
        FullTime = 1,
        [Description("Part-Time")]
        PartTime = 2,
        [Description("Seasonal")]
        Seasonal = 3
    }
}