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
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public short Status { get; set; }
        double FlagFixWeight { get; set; }
        public string StoreId { get; set; }
        public string Description { get; set; }
        public string UnitTypeId { get; set; }
        public string UnitType { get; set; }
    }
}
