using System;
using System.Collections.Generic;

namespace gurleenProject.Models
{
    public partial class StoreExpense
    {
        public int ExpenseId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public decimal TotalExpense { get; set; }
        public string Description { get; set; }

        public virtual Store Store { get; set; }
    }
}
