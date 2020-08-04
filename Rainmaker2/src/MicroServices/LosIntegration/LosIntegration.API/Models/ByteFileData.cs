using LosIntegration.API.Models.ClientModels;

namespace LosIntegration.API.Models
{
    public class ByteFileData
    {
        public int LoanId { get; set; }
        public long OrganizationId { get; set; }
        public string FileName { get; set; }
        public string OccupancyType { get; set; }
        public string AgencyCaseNo { get; set; }
        public long FileDataId { get; set; }


        public FileDataEntity GetRainmakerFileData()
        {
            var fileDataEntity = new FileDataEntity();
            fileDataEntity.FileDataId = this.FileDataId;
            fileDataEntity.PropertyUsageId = GetRainMakerOccupancyTypeId(this.OccupancyType);
            fileDataEntity.ExportLoanNumber = this.AgencyCaseNo;
            return fileDataEntity;
        }


        private int? GetRainMakerOccupancyTypeId(string occupancyType)
        {
            switch (occupancyType)
            {
                case "PrimaryResidence":
                {
                    return (int)Enums.PropertyUsageType.Primary;
                }
                case "SecondaryResidence":
                {
                    return (int)Enums.PropertyUsageType.Second;

                }
                case "InvestmentProperty":
                {
                    return (int)Enums.PropertyUsageType.Rental;

                }
            }

            return -1;
        }
    }
}
