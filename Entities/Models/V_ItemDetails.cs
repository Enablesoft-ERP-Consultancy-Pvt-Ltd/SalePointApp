using System;
using System.Collections.Generic;
using System.Text;

namespace SALEERP.Models
{
  public  class V_FinishedItemDetail
    {
        public int ITEM_FINISHED_ID { get; set; }
        public string CATEGORY_NAME { get; set; }

        public string ITEM_NAME { get; set; }
        public string COLORNAME { get; set; }
        public string QUALITYNAME { get; set; }
        public string qualityid { get; set; }
        public string SizeInch { get; set; }
        public string HeightInch { get; set; }
        public string ShapeName { get; set; }

    }
}
