using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class ExpenseTypeMaster
    {
        public int Id { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseDesc { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }
       
    }
    public partial class CategoryMaster
    {
        public int categoryid { get; set; }
        public string categoryname { get; set; }
    }
}
