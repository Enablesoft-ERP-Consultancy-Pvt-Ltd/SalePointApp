namespace SalesApp.Models.Product
{
    public class ProductModel
    {
        int ItemFinishId { get; set; }
        int QualityId { get; set; }
        int ColorId { get; set; }
        int DesignId { get; set; }
        int ShapeId { get; set; }
        int ShadecolorId { get; set; }
        int CategoryId { get; set; }
        int ItemId { get; set; }
        string ProductCode { get; set; }
        string CategoryName { get; set; }
        string ItemName { get; set; }
        string QualityName { get; set; }
        string DesignName { get; set; }
        string ColorName { get; set; }
        string ShadeColorName { get; set; }
        string ShapeName { get; set; }
        string HSNCode { get; set; }
        string QualityCode { get; set; }
        string DesignCode { get; set; }
        string ColorCode { get; set; }
        string SizeCode { get; set; }
        double Width { get; set; }
        double Length { get; set; }
        double Height { get; set; }
        short Status { get; set; }
        double FlagFixWeight { get; set; }
        string StoreId { get; set; }
        string Description { get; set; }
        string UnitTypeId { get; set; }
        string UnitType { get; set; }
    }
}
