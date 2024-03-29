﻿using Dapper;
using SalesApp.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Models;
using SalesApp.Models.Product;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using SalesApp.Utility;
using Microsoft.AspNetCore.Http;

using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using System.Drawing.Drawing2D;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace SalesApp.WebAPI.Data
{
    public class ProductData : IProductData
    {
        private IConfiguration configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        string NoImage = string.Empty;
        string BaseUrl = string.Empty;
        public ProductData(IConfiguration _configuration, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            configuration = _configuration;
            this._hostEnvironment = hostEnvironment;
            var request = httpContextAccessor.HttpContext.Request;
            this.BaseUrl = $"{request.Scheme}://{configuration.GetConnectionString("ImageHost").ToString()}";
            this.NoImage = Path.Combine(this.BaseUrl, "no-image.png");
        }


        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId)
        {
            try
            {
                ServiceResponse<IEnumerable<ProductModel>> obj = new ServiceResponse<IEnumerable<ProductModel>>();
                using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {

                    string sql = @"SELECT
IPM.ITEM_FINISHED_ID as ItemFinishId,IM.MasterCompanyId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,
IPM.Shadecolor_Id ShadeColorId,ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode,sz.SizeInch, sz.SizeFt,IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
z.DESCRIPTION,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,
IsNUll(stock.Quantity,0) Quantity,IsNUll(stock.Price,IsNUll(cost.Price,0.00)) Price,
IsNUll(cost.Discount,0.00) Discount,IsNUll(cost.IsCall,0) IsCall, IsNUll(cost.IsArrival,0) IsArrival,
(Select y.AttributeId as  '@AttributeId',y.AttributeName as  '@Name', x.AttributeValue as ItemName    from  tblItemAttributes x 
Inner Join tblItemAttributeMaster y on x.AttributeId=y.AttributeId
Where x.ItemFinishId=IPM.ITEM_FINISHED_ID
FOR XML PATH('Item'), ROOT('ItemList'), type) as AttributeList,
(Select IsNull(img.Remarks,'MIRZAPUR KALEEN AND RUGS') as '@PhotoRemarks',img.IsPrime as '@IsPrime',img.PhotoName  From 
tblItemPhoto img(Nolock)
Where img.ItemFinishId=IPM.ITEM_FINISHED_ID 
FOR XML PATH('Photo'), ROOT('PhotoList'), type) as PhotoList
FROM  ITEM_MASTER IM(Nolock) 
Inner JOIN ITEM_PARAMETER_MASTER IPM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID 
Left Join ITEM_PARAMETER_OTHER z on IPM.ITEM_FINISHED_ID = z.ITEM_FINISHED_ID
Left Join (select x.Item_Finished_Id,Avg(IsNUll(x.Price,0)) Price,Count(*) Quantity from CarpetNumber x
Group BY x.Item_Finished_Id,x.CurrentProStatus,x.Pack
Having x.CurrentProStatus=1 and x.Pack=0
) stock ON IPM.ITEM_FINISHED_ID  = stock.Item_Finished_Id
LEFT JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
LEFT JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
Left Join tblItemCosting(Nolock) cost on IPM.ITEM_FINISHED_ID =cost.ItemFinishId 
Where IM.MasterCompanyId=@StoreId";
                    var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId }));

                    var objItem = result.Select(x => new ProductModel
                    {

                        ItemFinishId = x.ItemFinishId,
                        QualityId = x.QualityId != null ? x.QualityId : 0,
                        ColorId = x.ColorId != null ? x.ColorId : 0,
                        DesignId = x.DesignId != null ? x.DesignId : 0,
                        ShapeId = x.ShapeId != null ? x.ShapeId : 0,
                        ShadecolorId = x.ShadecolorId != null ? x.ShadecolorId : 0,
                        CategoryId = x.CategoryId != null ? x.CategoryId : 0,
                        ItemId = x.ItemId != null ? x.ItemId : 0,
                        ProductCode = x.ProductCode,
                        CategoryName = x.CategoryName,
                        ItemName = x.ItemName,
                        QualityName = x.QualityName,
                        DesignName = x.DesignName,
                        ColorName = x.ColorName,
                        ShadeColorName = x.ShadeColorName,
                        ShapeName = x.ShapeName,
                        HSNCode = x.HSNCode,
                        QualityCode = x.QualityCode,
                        Width = x.WidthINCH != null ? x.WidthINCH : 0,
                        Length = x.LengthINCH != null ? x.LengthINCH : 0,
                        Height = x.HeightINCH != null ? x.HeightINCH : 0,
                        Status = x.Status != null ? x.Status : 0,
                        StoreId = x.MasterCompanyId != null ? x.MasterCompanyId : 0,
                        Description = x.DESCRIPTION != null ? x.DESCRIPTION : "Product Description not available",
                        UnitTypeId = x.UnitTypeId != null ? x.UnitTypeId : 0,
                        UnitType = x.UnitType,
                        PrimePhoto = this.GetMainImage(x.PhotoList),
                        ProductImages = this.BindImageList(x.PhotoList),
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Discount = x.Discount,
                        IsCall = x.IsCall,
                        IsArrival = x.IsArrival,

                        CreatedOn = x.ReceiveDate != null ? x.ReceiveDate : DateTime.Now,
                        SizeInch = x.SizeInch != null ? x.SizeInch : "",
                        SizeFeet = x.SizeFt != null ? x.SizeFt : "",
                        ItemList = this.BindItemList(x.AttributeList),


                    });

                    obj.Data = objItem;
                    obj.Result = obj.Data.Count() > 0 ? true : false;
                    obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
                }
                return obj;


            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId, int Count)
        {
            try
            {
                ServiceResponse<IEnumerable<ProductModel>> obj = new ServiceResponse<IEnumerable<ProductModel>>();
                using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {

                    string sql = @"SELECT IPM.ITEM_FINISHED_ID as ItemFinishId,IM.MasterCompanyId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,
IPM.Shadecolor_Id ShadeColorId,ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode,sz.SizeInch, sz.SizeFt,IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
z.DESCRIPTION,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,
IsNUll(stock.Quantity,0) Quantity,IsNUll(stock.Price,IsNUll(cost.Price,0.00)) Price,
IsNUll(cost.Discount,0.00) Discount,IsNUll(cost.IsCall,0) IsCall, IsNUll(cost.IsArrival,0) IsArrival,
(Select y.AttributeId as  '@AttributeId',y.AttributeName as  '@Name', x.AttributeValue as ItemName    from  tblItemAttributes x 
Inner Join tblItemAttributeMaster y on x.AttributeId=y.AttributeId
Where x.ItemFinishId=IPM.ITEM_FINISHED_ID
FOR XML PATH('Item'), ROOT('ItemList'), type) as AttributeList,
(Select IsNull(img.Remarks,'MIRZAPUR KALEEN AND RUGS') as '@PhotoRemarks',img.IsPrime as '@IsPrime',img.PhotoName  From 
tblItemPhoto img(Nolock)
Where img.ItemFinishId=IPM.ITEM_FINISHED_ID 
FOR XML PATH('Photo'), ROOT('PhotoList'), type) as PhotoList
FROM  ITEM_MASTER IM(Nolock) 
Inner JOIN ITEM_PARAMETER_MASTER IPM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID 
Left Join ITEM_PARAMETER_OTHER z on IPM.ITEM_FINISHED_ID = z.ITEM_FINISHED_ID
Left Join (select x.Item_Finished_Id,Avg(IsNUll(x.Price,0)) Price,Count(*) Quantity from CarpetNumber x
Group BY x.Item_Finished_Id,x.CurrentProStatus,x.Pack
Having x.CurrentProStatus=1 and x.Pack=0
) stock ON IPM.ITEM_FINISHED_ID  = stock.Item_Finished_Id
LEFT JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
LEFT JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
Left Join tblItemCosting(Nolock) cost on IPM.ITEM_FINISHED_ID =cost.ItemFinishId 
Where IM.MasterCompanyId=@StoreId";
                    var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId }));

                    var objItem = result.Select(x => new ProductModel
                    {

                        ItemFinishId = x.ItemFinishId,
                        QualityId = x.QualityId != null ? x.QualityId : 0,
                        ColorId = x.ColorId != null ? x.ColorId : 0,
                        DesignId = x.DesignId != null ? x.DesignId : 0,
                        ShapeId = x.ShapeId != null ? x.ShapeId : 0,
                        ShadecolorId = x.ShadecolorId != null ? x.ShadecolorId : 0,
                        CategoryId = x.CategoryId != null ? x.CategoryId : 0,
                        ItemId = x.ItemId != null ? x.ItemId : 0,
                        ProductCode = x.ProductCode,
                        CategoryName = x.CategoryName,
                        ItemName = x.ItemName,
                        QualityName = x.QualityName,
                        DesignName = x.DesignName,
                        ColorName = x.ColorName,
                        ShadeColorName = x.ShadeColorName,
                        ShapeName = x.ShapeName,
                        HSNCode = x.HSNCode,
                        QualityCode = x.QualityCode,
                        Width = x.WidthINCH != null ? x.WidthINCH : 0,
                        Length = x.LengthINCH != null ? x.LengthINCH : 0,
                        Height = x.HeightINCH != null ? x.HeightINCH : 0,
                        Status = x.Status != null ? x.Status : 0,
                        StoreId = x.MasterCompanyId != null ? x.MasterCompanyId : 0,
                        Description = x.DESCRIPTION != null ? x.DESCRIPTION : "Product Description not available",
                        UnitTypeId = x.UnitTypeId != null ? x.UnitTypeId : 0,
                        UnitType = x.UnitType,
                        PrimePhoto = this.GetMainImage(x.PhotoList),
                        ProductImages = this.BindImageList(x.PhotoList),

                        Quantity = x.Quantity,

                        Price = x.Price,
                        Discount = x.Discount,
                        IsCall = x.IsCall,
                        IsArrival = x.IsArrival,
                        CreatedOn = x.ReceiveDate != null ? x.ReceiveDate : DateTime.Now,
                        SizeInch = x.SizeInch != null ? x.SizeInch : "",
                        SizeFeet = x.SizeFt != null ? x.SizeFt : "",
                        ItemList = this.BindItemList(x.AttributeList),


                    }).Take(Count);

                    obj.Data = objItem;
                    obj.Result = obj.Data.Count() > 0 ? true : false;
                    obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
                }
                return obj;


            }
            catch (Exception ex)
            {

                throw ex;

            }

        }


        public List<string> BindImageList(string xmlStr)
        {
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(xmlStr))
            {
                result = XElement.Parse(xmlStr).Descendants("Photo").Where(x => Convert.ToInt16(x.Attribute("IsPrime").Value) == 0).
                    Select(x => (Path.Combine(this.BaseUrl, x.Element("PhotoName").Value))).ToList();
            }
            return result;

        }


        public string GetMainImage(string xmlStr)
        {
            string result = this.NoImage;
            if (!string.IsNullOrEmpty(xmlStr))
            {
                result = XElement.Parse(xmlStr).Descendants("Photo").Where(x => Convert.ToInt16(x.Attribute("IsPrime").Value) == 1).
                         Select(x => (Path.Combine(this.BaseUrl, x.Element("PhotoName").Value))).FirstOrDefault();
            }
            return result;

        }


        public List<ItemModel> BindItemList(string xmlStr)
        {
            List<ItemModel> result = new List<ItemModel>();
            if (!string.IsNullOrEmpty(xmlStr))
            {
                result = XElement.Parse(xmlStr).Descendants("Item").Select(x => new ItemModel { CategoryId = Convert.ToInt32(x.Attribute("AttributeId").Value), Category = x.Attribute("Name").Value, ItemName = x.Element("ItemName").Value }).ToList();
            }
            return result;

        }

        public List<StockModel> BindStockList(string xmlStr)
        {
            List<StockModel> result = new List<StockModel>();
            if (!string.IsNullOrEmpty(xmlStr))
            {
                result = XElement.Parse(xmlStr).Descendants("Stock").Select(x => new StockModel
                {
                    Price = Convert.ToDecimal(x.Attribute("Price").Value),
                    StockNo = x.Attribute("StockNo").Value,
                    TStockNo = x.Attribute("TStockNo").Value,
                }).ToList();
            }
            return result;

        }


        public async Task<ServiceResponse<ProductModel>> GetProductDetail(int ItemFinishId)
        {
            try
            {
                ServiceResponse<ProductModel> obj = new ServiceResponse<ProductModel>();
                using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {


                    string sql = @"SELECT IPM.ITEM_FINISHED_ID as ItemFinishId,IM.MasterCompanyId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,
IPM.Shadecolor_Id ShadeColorId,ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode,sz.SizeInch, sz.SizeFt,IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, IsNull(z.PRICE, 0.00) Price, 
z.DESCRIPTION,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,
IsNUll(cost.Discount,0.00) Discount,IsNUll(cost.IsCall,0) IsCall, IsNUll(cost.IsArrival,0) IsArrival,
(Select y.AttributeId as  '@AttributeId',y.AttributeName as  '@Name', x.AttributeValue as ItemName    from  tblItemAttributes x 
Inner Join tblItemAttributeMaster y on x.AttributeId=y.AttributeId
Where x.ItemFinishId=IPM.ITEM_FINISHED_ID
FOR XML PATH('Item'), ROOT('ItemList'), type) as AttributeList,
(Select IsNull(img.Remarks,'MIRZAPUR KALEEN AND RUGS') as '@PhotoRemarks',img.IsPrime as '@IsPrime',img.PhotoName  From 
tblItemPhoto img(Nolock)
Where img.ItemFinishId=IPM.ITEM_FINISHED_ID 
FOR XML PATH('Photo'), ROOT('PhotoList'), type) as PhotoList,
(Select IsNUll(x.Price,0) as '@Price',x.StockNo as '@StockNo',
x.TStockNo as '@TStockNo'  From CarpetNumber x(Nolock)
Where x.CurrentProStatus=1 and x.Pack=0 and x.Item_Finished_Id=IPM.ITEM_FINISHED_ID
FOR XML PATH('Stock'), ROOT('StockList'), type) as StockList
FROM  ITEM_MASTER IM(Nolock) 
Inner JOIN ITEM_PARAMETER_MASTER IPM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID 
Left Join ITEM_PARAMETER_OTHER z on IPM.ITEM_FINISHED_ID = z.ITEM_FINISHED_ID
LEFT JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
LEFT JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
LEFT JOIN tblItemCosting(Nolock) cost on IPM.ITEM_FINISHED_ID = cost.ItemFinishId
Where IPM.ITEM_FINISHED_ID=@ItemFinishId";

                    var result = (await connection.QueryAsync(sql, new { @ItemFinishId = ItemFinishId }));
                    var objItem = result.Select(x => new ProductModel
                    {

                        ItemFinishId = x.ItemFinishId,
                        QualityId = x.QualityId != null ? x.QualityId : 0,
                        ColorId = x.ColorId != null ? x.ColorId : 0,
                        DesignId = x.DesignId != null ? x.DesignId : 0,
                        ShapeId = x.ShapeId != null ? x.ShapeId : 0,
                        ShadecolorId = x.ShadecolorId != null ? x.ShadecolorId : 0,
                        CategoryId = x.CategoryId != null ? x.CategoryId : 0,
                        ItemId = x.ItemId != null ? x.ItemId : 0,
                        ProductCode = x.ProductCode,
                        CategoryName = x.CategoryName,
                        ItemName = x.ItemName,
                        QualityName = x.QualityName,
                        DesignName = x.DesignName,
                        ColorName = x.ColorName,
                        ShadeColorName = x.ShadeColorName,
                        ShapeName = x.ShapeName,
                        HSNCode = x.HSNCode,
                        QualityCode = x.QualityCode,
                        Width = x.WidthINCH != null ? x.WidthINCH : 0,
                        Length = x.LengthINCH != null ? x.LengthINCH : 0,
                        Height = x.HeightINCH != null ? x.HeightINCH : 0,
                        Status = x.Status != null ? x.Status : 0,
                        StoreId = x.MasterCompanyId != null ? x.MasterCompanyId : 0,
                        Description = x.DESCRIPTION != null ? x.DESCRIPTION : "Product Description not available",
                        UnitTypeId = x.UnitTypeId != null ? x.UnitTypeId : 0,
                        UnitType = x.UnitType,
                        PrimePhoto = this.GetMainImage(x.PhotoList),
                        ProductImages = this.BindImageList(x.PhotoList),
                        CreatedOn = x.ReceiveDate != null ? x.ReceiveDate : DateTime.Now,
                        SizeInch = x.SizeInch != null ? x.SizeInch : "",
                        SizeFeet = x.SizeFt != null ? x.SizeFt : "",
                        Price = (decimal)x.Price,
                        Discount = x.Discount,
                        IsCall = x.IsCall,
                        IsArrival = x.IsArrival,
                        ItemList = this.BindItemList(x.AttributeList),
                        StockList = this.BindStockList(x.StockList)


                    }).FirstOrDefault();

                    obj.Data = objItem;
                    obj.Result = obj.Data != null ? true : false;
                    obj.Message = obj.Data != null ? "Data Found." : "No Data found.";
                }
                return obj;

            }
            catch (Exception ex)
            {

                throw ex;

            }
        }



        public async Task<ServiceResponse<ProductModel>> AddToCardt(int ItemFinishId, int Quantity, string Source, short PackId)
        {

            ServiceResponse<ProductModel> obj = new ServiceResponse<ProductModel>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"Declare @Count int;
Update [dbo].[CarpetNumber] Set PackingID=0 Where PackingID>=101 and Pack_Date<=GETDATE().add and Item_Finished_Id=@ItemFinishId;
SELECT @Count=count(*) FROM CarpetNumber WHERE Item_Finished_Id=@ItemFinishId AND Pack=0;
IF (@Count>=@Quantity) 
BEGIN
WITH UpdateStock AS (
select TOP (select @Quantity)  * from [dbo].[CarpetNumber] Where Item_Finished_Id=@ItemFinishId AND Pack=0 
)--101
Update UpdateStock Set Pack = @PackId,PackSource = @Source;

SELECT distinct IM.MasterCompanyId,IM.ITEM_ID as ItemId,IPM.ITEM_FINISHED_ID as ItemFinishId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,IPM.Shadecolor_Id ShadeColorId,
ICM.CATEGORY_ID as CategoryId,IPM.ProductCode,IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, 
ISNULL(Q.QualityName, '') QualityName, ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,
ISNULL(SC.ShadeColorName, '') ShadeColorName, ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, 
Isnull(IM.ITEM_CODE, '') ItemCode, IsNull(Q.QualityCode, '')  QualityCode, IsNull(SZ.WidthInch, 0) Width,
IsNull(SZ.LengthINCH, 0) Length, IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
IPM.Description,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,tblImg.PHOTO,tblImg.Remarks,
stock.StockNo,stock.TStockNo,ISNULL(stock.Price, 0 ) AS Price
FROM  ITEM_MASTER IM(Nolock) Inner Join ITEM_PARAMETER_MASTER IPM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID 
inner join CarpetNumber stock(Nolock) ON IPM.ITEM_FINISHED_ID  = stock.Item_Finished_Id  
JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
LEFT JOIN MAIN_ITEM_IMAGE tblImg(Nolock) ON IPM.ITEM_FINISHED_ID = tblImg.FINISHEDID
Where stock.ITEM_FINISHED_ID=@ItemFinishId and stock.PackingID=@PackId AND stock.packsource=@Source;
END";

                var result = (await connection.QueryAsync(sql, new { @ItemFinishId = ItemFinishId, @Quantity = Quantity, @Source = Source, @PackId = PackId }));

                var objItem = (from itm in result
                               group itm by new { itm.ItemFinishId } into itmGroup
                               orderby itmGroup.Key.ItemFinishId descending
                               select new ProductModel
                               {
                                   ItemFinishId = itmGroup.Key.ItemFinishId != null ? itmGroup.Key.ItemFinishId : 0,
                                   QualityId = itmGroup.FirstOrDefault().QualityId != null ? itmGroup.FirstOrDefault().QualityId : 0,
                                   ColorId = itmGroup.FirstOrDefault().ColorId != null ? itmGroup.FirstOrDefault().ColorId : 0,
                                   DesignId = itmGroup.FirstOrDefault().DesignId != null ? itmGroup.FirstOrDefault().DesignId : 0,
                                   ShapeId = itmGroup.FirstOrDefault().ShapeId != null ? itmGroup.FirstOrDefault().ShapeId : 0,
                                   ShadecolorId = itmGroup.FirstOrDefault().ShadecolorId != null ? itmGroup.FirstOrDefault().ShadecolorId : 0,
                                   CategoryId = itmGroup.FirstOrDefault().CategoryId != null ? itmGroup.FirstOrDefault().CategoryId : 0,
                                   ItemId = itmGroup.FirstOrDefault().ItemId != null ? itmGroup.FirstOrDefault().ItemId : 0,
                                   ProductCode = itmGroup.FirstOrDefault().ProductCode,
                                   CategoryName = itmGroup.FirstOrDefault().CategoryName,
                                   ItemName = itmGroup.FirstOrDefault().ItemName,
                                   QualityName = itmGroup.FirstOrDefault().QualityName,
                                   DesignName = itmGroup.FirstOrDefault().DesignName,
                                   ColorName = itmGroup.FirstOrDefault().ColorName,
                                   ShadeColorName = itmGroup.FirstOrDefault().ShadeColorName,
                                   ShapeName = itmGroup.FirstOrDefault().ShapeName,
                                   HSNCode = itmGroup.FirstOrDefault().HSNCode,
                                   QualityCode = itmGroup.FirstOrDefault().QualityCode,

                                   Width = itmGroup.FirstOrDefault().WidthINCH != null ? itmGroup.FirstOrDefault().WidthINCH : 0,
                                   Length = itmGroup.FirstOrDefault().LengthINCH != null ? itmGroup.FirstOrDefault().LengthINCH : 0,
                                   Height = itmGroup.FirstOrDefault().HeightINCH != null ? itmGroup.FirstOrDefault().HeightINCH : 0,

                                   Status = itmGroup.FirstOrDefault().Status != null ? itmGroup.FirstOrDefault().Status : 0,
                                   StoreId = itmGroup.FirstOrDefault().MasterCompanyId != null ? itmGroup.FirstOrDefault().MasterCompanyId : 0,
                                   Description = itmGroup.FirstOrDefault().Description,
                                   UnitTypeId = itmGroup.FirstOrDefault().UnitTypeId != null ? itmGroup.FirstOrDefault().UnitTypeId : 0,
                                   UnitType = itmGroup.FirstOrDefault().UnitType,
                                   ProductImages = itmGroup.Where(x => x.ImagePath != null).Select(x => (string)x.ImagePath).ToList(),
                                   Price = itmGroup.FirstOrDefault().Price != null ? itmGroup.FirstOrDefault().Price : 0,

                                   Stocks = itmGroup.Select(x => (long)x.StockNo).ToList(),
                                   StockNos = itmGroup.Select(x => (string)x.TStockNo).ToList(),

                               }).FirstOrDefault();
                obj.Data = objItem;
                obj.Result = obj.Data != null ? true : false;
                obj.Message = obj.Data != null ? "Data Found." : "No Data found.";
            }
            return obj;

        }



        public async Task<ServiceResponse<int>> CreateOrder(OrderModel _model)
        {

            ServiceResponse<int> obj = new ServiceResponse<int>();
            string Message = string.Empty;
            IDbTransaction transaction = null;
            try
            {

                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();

                    string Query;
                    Query = @"INSERT INTO [sales].[Order_Master]
([sale_date],[transaction_id],[delievery_type],[port_type],[description],[unit],[created_datetime],
[created_by],[is_active],[sale_status],[sale_value],[bill_id],[session_year],[DISCOUNTPER])
VALUES
(@SaleDate,@TransactionId,@DelieveryType,@PortType,@Description,@Unit,@CreatedOn,
@CreatedBy,@IsActive,@SaleStatus,@SaleValue,@BillId,@SessionYear,@DisCountPer);
select SCOPE_IDENTITY();
";
                    _model.OrderId = (await cnn.ExecuteScalarAsync<int>(Query, new
                    {
                        @MirrorId = _model.MirrorId,
                        @TransactionId = _model.TransactionId,
                        @SaleDate = _model.SaleDate,
                        @SaleStatus = _model.SaleStatus,
                        @SaleValue = 0.00,
                        @BillId = 0,
                        @SessionYear = _model.SessionYear,
                        @DisCountPer = _model.DisCountPer,
                        @Unit = _model.Unit,
                        @PortType = _model.PortType,
                        @DelieveryType = _model.DelieveryType,
                        @Description = _model.Description,
                        @IsActive = _model.IsActive,
                        @CreatedBy = _model.CreatedBy,
                        @CreatedOn = _model.CreatedOn
                    }, transaction));

                    string CustQuery;
                    CustQuery = @"INSERT INTO [sales].[Customer_Details]([order_id],[title],[name],[address],[state],[city],[country],[zipcode],[shippingaddress],		 
[mobile],[email],[created_datetime],[created_by],[mob_country_code])
VALUES(@OrderId,@Title,@Name,@Address,@State,@City,@Country,@ZipCode,@ShippingAddress,
@ContactNo,@Email,@CreatedOn,@CreatedBy,@CountryCode)";

                    _model.Customer.OrderId = _model.OrderId;

                    var addCust = (await cnn.ExecuteAsync(CustQuery, _model.Customer, transaction));

                    _model.ItemList.ForEach(x => x.OrderId = _model.OrderId);

                    //string sqlQuery = @"INSERT INTO [sales].[Order_Item_Details]
                    //([trans_id],[stock_id],[order_id],[order_type],[order_type_prefix],[sale_type],[qty],[currency_type],
                    //[price],[price_inr],[conversion_rate],[unit],[item_type],[item_desc],[created_datetime],
                    //[created_by],[is_active],[session_year],[hsncode],[finishedid])
                    //VALUES
                    //(@TransId,@StockId,@OrderId,@OrderType,@OrderTypePrefix,@SalesType,@Qty,@CurrencyType,
                    //@Price,@PriceINR,@ConversionRate,@Unit,@ItemType,@ItemDescription,@CreatedOn,@CreatedBy,@IsActive,@SessionYear,@HsnCode,@FinishedId);
                    //Update [dbo].[CarpetNumber] Set PackDate=@CreatedOn,Pack=2,PackSource=@PackSource,PackingDetailId=SCOPE_IDENTITY(),PackingId=@OrderId
                    //Where TStockNo=@StockId;";

                    string sqlQuery = @"Declare @Count int
Update x SET x.is_active = 'false',x.updated_by = 1,x.updated_datetime = getDate() ,x.bill_id = 0
from sales.Order_Master x Inner Join sales.Order_Item_Details y on x.id = y.order_id 
inner join  [dbo].[CarpetNumber] z on  z.TStockNo = y.stock_id
Where z.Pack>=101 and z.Item_Finished_Id=@FinishedId and z.PackSource = @Source and z.PackingID=@CustomerId

Update y SET y.is_active = 'false',y.updated_by = 1,y.updated_datetime = getDate() ,y.bill_id = 0
from sales.Order_Master x Inner Join sales.Order_Item_Details y on x.id = y.order_id 
inner join  [dbo].[CarpetNumber] z on  z.TStockNo = y.stock_id
Where z.Pack>=101 and z.Item_Finished_Id=@FinishedId and z.PackSource = @Source and z.PackingID=@CustomerId

Update y SET y.is_active = 'false',y.updated_by = 1
from sales.Order_Item_Details x Inner Join sales.Order_Payment y on x.order_id = y.order_id 
inner join  [dbo].[CarpetNumber] z on  z.TStockNo = x.stock_id
Where z.Pack>=101 and z.Item_Finished_Id=@FinishedId and z.PackSource = @Source and z.PackingID=@CustomerId
Update x SET x.PackingDetailId = 0,x.PackingID=null,x.packsource = '',x.Pack = 0,x.Pack_Date = null
from[dbo].[CarpetNumber] x inner join[sales].Order_Item_Details y on x.TStockNo = y.stock_id
where x.Pack>=101 and x.Item_Finished_Id=@FinishedId and x.PackSource = @Source and x.PackingID=@CustomerId
SELECT @Count=count(*) FROM CarpetNumber WHERE Item_Finished_Id=@FinishedId AND Pack=0
IF (@Count>=@Quantity) 
BEGIN
WITH UpdateStock AS(
select TOP (select @Quantity)  * from [dbo].[CarpetNumber] Where Item_Finished_Id=@FinishedId AND Pack=0 
)
select * into #temp from UpdateStock
Update p Set p.Pack = @PackId,p.PackingID=@CustomerId,p.PackingDetailID=@OrderId,p.Pack_Date=@CreatedOn,p.PackSource = @Source  from  
CarpetNumber p inner join  #temp q on p.TStockNo=q.TStockNo

INSERT INTO [sales].[Order_Item_Details]
([bill_id],[trans_id],[stock_id],[order_id],[order_type],[order_type_prefix],[sale_type],[qty],[currency_type],
[price],[price_inr],[conversion_rate],[unit],[item_type],[item_desc],[created_datetime],
[created_by],[is_active],[session_year],[finishedid])
select 0,@TransId,x.TStockNo as StockId,@OrderId,@OrderType,@OrderTypePrefix,@SalesType,1,@CurrencyType,
@Price,@Price,@ConversionRate,@Unit,@ItemType,@ItemDescription,@CreatedOn,@CreatedBy,@IsActive,@SessionYear,@FinishedId
from #temp as  x 
drop table #temp
END";

                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model.ItemList, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        obj.Data = _model.OrderId;

                    }
                    else
                    {

                        obj.Data = -1;

                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                    }
                    obj.Message = obj.Data > 0 ? "Data Found." : "No Data found.";

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                obj.Data = -1;
                obj.Message = ex.Message;
                return obj;

            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
                obj.Result = obj.Data > 0 ? true : false;
            }
            return obj;
        }


        public async Task<ServiceResponse<bool>> AddPayment(OrderPaymentModel _model)
        {
            bool IsSuccess = false;
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            string Message = string.Empty;
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();

                    string sqlQuery = @"IF NOT EXISTS (SELECT 0 FROM sales.Order_Payment WHERE order_id=@OrderId)
BEGIN
INSERT INTO [sales].[Order_Payment]
([order_id],[pay_mode],[card_type],[amount],[amout_hd],[IGST],[GST],[pay_date],[currency_type],[created_datetime]
,[created_by],[updated_by],[update_datetime],[is_active],[paylaterstatus],[paylaterdate])
VALUES
(@OrderId,@PaymentMode,@CardType,@Amount,@Amount,@IGST,@GST,@PaymentDate,@Currency,@CreatedOn
,@CreatedBy,@CreatedBy,@CreatedOn,@IsActive,@PaylaterStatus,@PaymentDate)

Update [sales].[Order_Master] Set sale_status=1 Where id=@OrderId
Update x SET x.Pack=1,x.PackingDetailId=y.id,x.PackingID=y.order_id from [dbo].[CarpetNumber] x inner join [sales].Order_Item_Details y on x.TStockNo=y.stock_id
where y.order_id=@OrderId
END";

                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {

                        IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Message = ex.Message;
                IsSuccess = false;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }


            obj.Data = IsSuccess;
            obj.Result = IsSuccess;
            obj.Message = IsSuccess ? "Data Found." : "No Data found.";
            return obj;
        }

        public async Task<ServiceResponse<bool>> CancelOrder(CancelOrderModel _model)
        {
            bool IsSuccess = false;
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            string Message = string.Empty;
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();

                    string sqlQuery = @"Update y SET y.is_active = @IsActive,y.updated_by = @CreatedBy,y.updated_datetime = @CreatedOn ,y.bill_id = @BillId
from sales.Order_Master x Inner Join sales.Order_Item_Details y on x.id = y.order_id Where y.order_id = @OrderId
Update y SET y.is_active = @IsActive,y.updated_by = @CreatedBy
from sales.Order_Master x Inner Join sales.Order_Payment y on x.id = y.order_id Where y.order_id = @OrderId
Update x SET x.PackingDetailId = @PackingDetailId,x.packsource = '',x.Pack = 0,x.Pack_Date = null
from[dbo].[CarpetNumber] x inner join[sales].Order_Item_Details y on x.TStockNo = y.stock_id
where y.order_id = @OrderId";

                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {

                        IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                Message = ex.Message;
                IsSuccess = false;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }


            obj.Data = IsSuccess;
            obj.Result = IsSuccess;
            obj.Message = IsSuccess ? "Data Found." : "No Data found.";
            return obj;
        }

        public async Task<ServiceResponse<bool>> AddWishItem(WishItemModel _model)
        {
            bool IsSuccess = false;
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    string sqlQuery = @"IF NOT EXISTS (SELECT 0 FROM tblWishList WHERE CustomerId=@CustomerId and  ProductId=@ProductId and StoreId=@StoreId)
BEGIN
INSERT INTO tblWishList(StoreId,ProductId,Quantity,IsActive,CustomerId,IsPublished,CreatedOn)
VALUES(@StoreId,@ProductId,@Quantity,@IsActive,@CustomerId,@IsPublished,@CreatedOn)
END
ELSE
BEGIN
UPDATE tblWishList SET Quantity=@Quantity WHERE CustomerId=@CustomerId and  ProductId=@ProductId and StoreId=@StoreId
END";

                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model);
                    if (rowsAffected > 0)
                    {

                        IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                IsSuccess = false;
            }

            obj.Data = IsSuccess;
            obj.Result = IsSuccess;
            obj.Message = IsSuccess ? "Data Found." : "No Data found.";
            return obj;
        }

        public async Task<ServiceResponse<bool>> DelWishItem(int WishId)
        {
            bool IsSuccess = false;
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            try
            {
                using (IDbConnection conn = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    string sqlQuery = @"Delete From tblWishList Where WishId=@WishId";

                    int rowsAffected = await conn.ExecuteAsync(sqlQuery, new { @WishId = WishId });
                    if (rowsAffected > 0)
                    {
                        IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                IsSuccess = false;
            }

            obj.Data = IsSuccess;
            obj.Result = IsSuccess;
            obj.Message = IsSuccess ? "Data Found." : "No Data found.";
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<WishModel>>> GetWishList(int StoreId, int CustomerId)
        {
            try
            {
                ServiceResponse<IEnumerable<WishModel>> obj = new ServiceResponse<IEnumerable<WishModel>>();
                using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {

                    string sql = @"SELECT wish.WishId,wish.CustomerId ,wish.Quantity,wish.ProductId,wish.StoreId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,
IPM.Shadecolor_Id ShadeColorId,ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode,sz.SizeInch, sz.SizeFt,IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, IsNull(z.PRICE, 0.00) Price, 
z.DESCRIPTION,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,
(Select y.AttributeId as  '@AttributeId',y.AttributeName as  '@Name', x.AttributeValue as ItemName    from  tblItemAttributes x 
Inner Join tblItemAttributeMaster y on x.AttributeId=y.AttributeId
Where x.ItemFinishId=IPM.ITEM_FINISHED_ID
FOR XML PATH('Item'), ROOT('ItemList'), type) as AttributeList,
(Select IsNull(img.Remarks,'MIRZAPUR KALEEN AND RUGS') as '@PhotoRemarks',img.IsPrime as '@IsPrime',img.PhotoName  From 
tblItemPhoto img(Nolock) Where img.ItemFinishId=IPM.ITEM_FINISHED_ID 
FOR XML PATH('Photo'), ROOT('PhotoList'), type) as PhotoList
FROM tblWishList(Nolock) wish Inner Join ITEM_PARAMETER_MASTER IPM(Nolock) 
ON wish.ProductId = IPM.ITEM_FINISHED_ID Inner Join ITEM_MASTER IM(Nolock) 
on IPM.ITEM_ID=IM.ITEM_ID 
Left Join ITEM_PARAMETER_OTHER z on IPM.ITEM_FINISHED_ID = z.ITEM_FINISHED_ID
LEFT JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
LEFT JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
Where wish.CustomerId=@CustomerId and wish.StoreId=@StoreId";
                    var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId, @CustomerId = CustomerId }));

                    var objItem = result.Select(x => new WishModel
                    {

                        WishId = x.WishId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        StoreId = x.StoreId,
                        CustomerId = x.CustomerId,
                        ProductCode = x.ProductCode,
                        CategoryName = x.CategoryName,
                        ItemName = x.ItemName,
                        QualityName = x.QualityName,
                        DesignName = x.DesignName,
                        ColorName = x.ColorName,
                        ShadeColorName = x.ShadeColorName,
                        ShapeName = x.ShapeName,
                        PrimePhoto = this.GetMainImage(x.PhotoList),
                        SizeInch = x.SizeInch != null ? x.SizeInch : "",

                    });

                    obj.Data = objItem;
                    obj.Result = obj.Data.Count() > 0 ? true : false;
                    obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
                }
                return obj;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        public async Task<ServiceResponse<IEnumerable<BillModel>>> GetAllWebOrder()
        {
            ServiceResponse<IEnumerable<BillModel>> obj = new ServiceResponse<IEnumerable<BillModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"Select y.order_id,IsNull(Max(y.bill_id),00) as BillId 
  FROM [MirzapurSale].[sales].[Order_Master] x inner join 
  [MirzapurSale].[sales].[Order_Item_Details] y on x.id=y.order_id and x.description='WebSales'
  group By y.order_id;";

                var result = (await connection.QueryAsync(sql)).Where(x => x.order_id != null).Select(x => new BillModel { OrderId = (long)x.order_id, BillId = (long)x.BillId });

                obj.Data = result;
                obj.Result = obj.Data.Count() > 0 ? true : false;
                obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<dynamic>> GetOrderDeatil(long orderId)
        {
            ServiceResponse<dynamic> obj = new ServiceResponse<dynamic>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"Select * FROM [MirzapurSale].[sales].[Order_Master] x Left join 
[MirzapurSale].[sales].[Order_Item_Details] y on x.id=y.order_id 
Left join 
[MirzapurSale].[sales].[Order_Payment] z on x.id=z.order_id 
Left join 
[MirzapurSale].[sales].[Customer_Details] p on x.id=p.order_id 
Left join 
[MirzapurSale].[sales].[Customer_Details] q on x.id=q.order_id
Where x.id=@OrderId;";

                var result = (await connection.QueryAsync(sql, new { @OrderId = orderId }));

                obj.Data = result;
                obj.Result = obj.Data != null ? true : false;
                obj.Message = obj.Data != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }



        public async Task<ServiceResponse<IEnumerable<dynamic>>> GetFilter(int StoreId)
        {
            ServiceResponse<IEnumerable<dynamic>> obj = new ServiceResponse<IEnumerable<dynamic>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"Select x.AttributeId,y.AttributeName,x.AttributeValue 
from tblItemAttributes x Inner Join tblItemAttributeMaster y 
on x.AttributeId=y.AttributeId
Group By x.AttributeId,y.AttributeName,x.AttributeValue,y.CompanyId
Having y.CompanyId=@StoreId";

                var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId }));

                obj.Data = result;
                obj.Result = obj.Data != null ? true : false;
                obj.Message = obj.Data != null ? result.Count() + " Record Found." : "No Data found.";
            }
            return obj;
        }









    }
}
