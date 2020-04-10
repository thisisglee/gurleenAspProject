using System;
using System.Collections.Generic;

namespace gurleenProject.Models
{
    public partial class StoreTravel
    {
        public int TravelId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string Description { get; set; }

        public virtual Store Store { get; set; }
    }
}
