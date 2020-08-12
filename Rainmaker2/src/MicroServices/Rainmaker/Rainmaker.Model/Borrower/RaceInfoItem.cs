﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rainmaker.Model.Borrower
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
