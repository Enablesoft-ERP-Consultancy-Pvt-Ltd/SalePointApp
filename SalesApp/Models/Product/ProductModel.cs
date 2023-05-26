using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesApp.Models.Product
{
    public class ProductModel
    {
        public int ItemFinishId { get; set; }
        public int QualityId { get; set; }
        public int ColorId { get; set; }
        public int DesignId { get; set; }
        public int ShapeId { get; set; }
        public int ShadecolorId { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public string ProductCode { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string QualityName { get; set; }
        public string DesignName { get; set; }
        public string ColorName { get; set; }
        public string ShadeColorName { get; set; }
        public string ShapeName { get; set; }
        public string HSNCode { get; set; }
        public string QualityCode { get; set; }
        public string DesignCode { get; set; }
        public string ColorCode { get; set; }
        public string SizeCode { get; set; }



        public string SizeInch { get; set; }
        public string SizeFeet { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public int Status { get; set; }
        double FlagFixWeight { get; set; }
        public int StoreId { get; set; }
        public string Description { get; set; }
        public int UnitTypeId { get; set; }
        public string UnitType { get; set; }
        public decimal Price { get; set; }

        public string PrimePhoto { get; set; }
        public IList<string> ProductImages { get; set; }
        public IList<long> Stocks { get; set; }
        public IList<string> StockNos { get; set; }

        public int Quantity { get; set; }


        public DateTime CreatedOn { get; set; }

        public List<ItemModel> ItemList { get; set; }
        public List<StockModel> StockList { get; set; }

        
    }


    public class ItemModel
    {
        public int CategoryId { get; set; }

        public string Category { get; set; }
        public string ItemName { get; set; }
    }


    public class StockModel
    {
        public decimal Price { get; set; }

        public string TStockNo { get; set; }
        public string StockNo { get; set; }
    }

}
