namespace LosIntegration.API.Models
{
    public class RaceInfoItem
    {
        public int? RaceId { get; }
        public int? RaceDetailId { get; }


        public RaceInfoItem(int? raceId,
                            int? raceDetailId)
        {
            RaceId = raceId;
            RaceDetailId = raceDetailId;
        }

    }
}