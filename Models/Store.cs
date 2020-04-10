using System;
using System.Collections.Generic;

namespace gurleenProject.Models
{
    public partial class Store
    {
        public Store()
        {
            StoreExpense = new HashSet<StoreExpense>();
            StoreTravel = new HashSet<StoreTravel>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hours { get; set; }
        public string Description { get; set; }

        public virtual ICollection<StoreExpense> StoreExpense { get; set; }
        public virtual ICollection<StoreTravel> StoreTravel { get; set; }
    }
}
