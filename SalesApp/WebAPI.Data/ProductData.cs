using Dapper;
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
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Aspose.BarCode.Generation;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using SalesApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            ServiceResponse<IEnumerable<ProductModel>> obj = new ServiceResponse<IEnumerable<ProductModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"SELECT DISTINCT TOP 1000 IM.MasterCompanyId,IM.ITEM_ID as ItemId,IPM.ITEM_FINISHED_ID as ItemFinishId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,IPM.Shadecolor_Id ShadeColorId,
ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode, IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
IPM.Description,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,tblImg.PHOTO as ImagePath,tblImg.Remarks,
stock.StockNo,stock.TStockNo,ISNULL(stock.Price, 0 ) AS Price,stock.Rec_Date as ReceiveDate
FROM  ITEM_MASTER IM(Nolock) Inner JOIN ITEM_PARAMETER_MASTER IPM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID 
Inner JOIN CarpetNumber stock(Nolock) ON IPM.ITEM_FINISHED_ID  = stock.Item_Finished_Id  
LEFT JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
LEFT JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
LEFT JOIN MAIN_ITEM_IMAGE tblImg(Nolock) ON IPM.ITEM_FINISHED_ID = tblImg.FINISHEDID
Where IM.MasterCompanyId=@StoreId and stock.CurrentProStatus=1 and stock.Pack=0;";







                //isnull(D.DesignCode,'') DesignCode,
                //isnull(C.ColorCode,'') ColorCode,
                //isnull(SZ.SizeCode,'') SizeCode, 
                //
                var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId }));

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
                                   //DesignCode = itmGroup.FirstOrDefault().DesignCode,
                                   //ColorCode = itmGroup.FirstOrDefault().ColorCode,
                                   //SizeCode = itmGroup.FirstOrDefault().SizeCode,

                                   Width = itmGroup.FirstOrDefault().WidthINCH != null ? itmGroup.FirstOrDefault().WidthINCH : 0,
                                   Length = itmGroup.FirstOrDefault().LengthINCH != null ? itmGroup.FirstOrDefault().LengthINCH : 0,
                                   Height = itmGroup.FirstOrDefault().HeightINCH != null ? itmGroup.FirstOrDefault().HeightINCH : 0,

                                   Status = itmGroup.FirstOrDefault().Status != null ? itmGroup.FirstOrDefault().Status : 0,
                                   StoreId = itmGroup.FirstOrDefault().MasterCompanyId != null ? itmGroup.FirstOrDefault().MasterCompanyId : 0,
                                   Description = itmGroup.FirstOrDefault().Description,
                                   UnitTypeId = itmGroup.FirstOrDefault().UnitTypeId != null ? itmGroup.FirstOrDefault().UnitTypeId : 0,
                                   UnitType = itmGroup.FirstOrDefault().UnitType,

                                   PrimePhoto = !string.IsNullOrEmpty(itmGroup.FirstOrDefault().ImagePath) ? Path.Combine(this.BaseUrl, (string)itmGroup.FirstOrDefault().ImagePath) : this.NoImage,
                                   ProductImages = itmGroup.Where(x => !string.IsNullOrEmpty(x.ImagePath)).Select(x => (Path.Combine(this.BaseUrl, (string)x.ImagePath))).ToList(),

                                   Price = itmGroup.FirstOrDefault().Price != null ? itmGroup.FirstOrDefault().Price : 0,
                                   //Stocks = itmGroup.Select(x => (long)x.StockNo).ToList(),
                                   //StockNos = itmGroup.Select(x => (string)x.TStockNo).ToList(),
                                   Quantity = itmGroup.Count(),



                                   CreatedOn = itmGroup.FirstOrDefault().ReceiveDate != null ? itmGroup.FirstOrDefault().ReceiveDate : DateTime.Now,

                               }); ;
                obj.Data = objItem;
                obj.Result = obj.Data.Count() > 0 ? true : false;
                obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<ProductModel>> GetProductDetail(int ItemFinishId)
        {
            ServiceResponse<ProductModel> obj = new ServiceResponse<ProductModel>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"SELECT DISTINCT TOP 1000 IM.MasterCompanyId,IM.ITEM_ID as ItemId,IPM.ITEM_FINISHED_ID as ItemFinishId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,IPM.Shadecolor_Id ShadeColorId,
ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode, IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
IPM.Description,IsNull(ProdAreaFt, 0) ProdAreaFt,IsNull(ProdAreaMtr, 0) ProdAreaMtr, 
UTM.UnitTypeID as UnitTypeId, UTM.UnitType,tblImg.PHOTO as ImagePath,tblImg.Remarks,
stock.StockNo,stock.TStockNo,ISNULL(stock.Price, 0 ) AS Price,stock.Rec_Date as ReceiveDate
FROM  ITEM_MASTER IM(Nolock) Inner JOIN ITEM_PARAMETER_MASTER IPM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID 
Inner JOIN CarpetNumber stock(Nolock) ON IPM.ITEM_FINISHED_ID  = stock.Item_Finished_Id  
LEFT JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON IM.CATEGORY_ID  = ICM.CATEGORY_ID  
LEFT JOIN UNIT_TYPE_MASTER UTM(Nolock) ON IM.UnitTypeID  = UTM.UnitTypeID
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
LEFT JOIN MAIN_ITEM_IMAGE tblImg(Nolock) ON IPM.ITEM_FINISHED_ID = tblImg.FINISHEDID
Where IPM.ITEM_FINISHED_ID=@ItemFinishId;";

                var result = (await connection.QueryAsync(sql, new { @ItemFinishId = ItemFinishId }));

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
                                   //DesignCode = itmGroup.FirstOrDefault().DesignCode,
                                   //ColorCode = itmGroup.FirstOrDefault().ColorCode,
                                   //SizeCode = itmGroup.FirstOrDefault().SizeCode,

                                   Width = itmGroup.FirstOrDefault().WidthINCH != null ? itmGroup.FirstOrDefault().WidthINCH : 0,
                                   Length = itmGroup.FirstOrDefault().LengthINCH != null ? itmGroup.FirstOrDefault().LengthINCH : 0,
                                   Height = itmGroup.FirstOrDefault().HeightINCH != null ? itmGroup.FirstOrDefault().HeightINCH : 0,

                                   Status = itmGroup.FirstOrDefault().Status != null ? itmGroup.FirstOrDefault().Status : 0,
                                   StoreId = itmGroup.FirstOrDefault().MasterCompanyId != null ? itmGroup.FirstOrDefault().MasterCompanyId : 0,
                                   Description = itmGroup.FirstOrDefault().Description,
                                   UnitTypeId = itmGroup.FirstOrDefault().UnitTypeId != null ? itmGroup.FirstOrDefault().UnitTypeId : 0,
                                   UnitType = itmGroup.FirstOrDefault().UnitType,

                                   PrimePhoto = !string.IsNullOrEmpty(itmGroup.FirstOrDefault().ImagePath) ? Path.Combine(this.BaseUrl, (string)itmGroup.FirstOrDefault().ImagePath) : this.NoImage,
                                   ProductImages = itmGroup.Where(x => !string.IsNullOrEmpty(x.ImagePath)).Select(x => (Path.Combine(this.BaseUrl, (string)x.ImagePath))).ToList(),

                                   Price = itmGroup.FirstOrDefault().Price != null ? itmGroup.FirstOrDefault().Price : 0,
                                   Stocks = itmGroup.Select(x => (long)x.StockNo).ToList(),
                                   StockNos = itmGroup.Select(x => (string)x.TStockNo).ToList(),
                                   Quantity = itmGroup.Count(),
                                   CreatedOn = itmGroup.FirstOrDefault().ReceiveDate != null ? itmGroup.FirstOrDefault().ReceiveDate : DateTime.Now,

                               }).FirstOrDefault();
                obj.Data = objItem;
                obj.Result = obj.Data != null ? true : false;
                obj.Message = obj.Data != null ? "Data Found." : "No Data found.";
            }
            return obj;
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
[created_by],[is_active],[sale_status],[session_year],[DISCOUNTPER])
VALUES
(@SaleDate,@TransactionId,@DelieveryType,@PortType,@Description,@Unit,@CreatedOn,
@CreatedBy,@IsActive,@SaleStatus,@SessionYear,@DisCountPer);
select SCOPE_IDENTITY();
";
                    _model.OrderId = (await cnn.ExecuteScalarAsync<int>(Query, new
                    {
                        @TransactionId = _model.TransactionId,
                        @SaleDate = _model.SaleDate,
                        @SaleStatus = _model.SaleStatus,
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
                    CustQuery = @"INSERT INTO [sales].[Customer_Details]([order_id],[name],[address],[state],[city],[country],[zipcode],[shippingaddress],		 
[mobile],[email],[created_datetime],[created_by])
VALUES(@OrderId,@Name,@Address,@State,@City,@Country,@ZipCode,@ShippingAddress,
@ContactNo,@Email,@CreatedOn,@CreatedBy)";

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

                    string sqlQuery = @"INSERT INTO [sales].[Order_Payment]
([order_id],[pay_mode],[card_type],[amount],[amout_hd],[IGST],[GST],[pay_date],[currency_type],[created_datetime]
,[created_by],[updated_by],[update_datetime],[is_active],[paylaterstatus],[paylaterdate])
VALUES
(@OrderId,@PaymentMode,@CardType,@Amount,@Amount,@IGST,@GST,@PaymentDate,@Currency,@CreatedOn
,@CreatedBy,@CreatedBy,@CreatedOn,@IsActive,@PaylaterStatus,@PaymentDate)

Update [sales].[Order_Master] Set sale_status=1 Where id=@OrderId
Update x SET x.Pack=1,x.PackingDetailId=y.id,x.PackingID=y.order_id from [dbo].[CarpetNumber] x inner join [sales].Order_Item_Details y on x.TStockNo=y.stock_id
where y.order_id=@OrderId";

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

                    string sqlQuery = @"Update x SET x.is_active = @IsActive,x.updated_by = @CreatedBy,x.updated_datetime = @CreatedOn ,x.bill_id = @BillId
from sales.Order_Master x Inner Join sales.Order_Item_Details y on x.id = y.order_id Where y.order_id = @OrderId
Update y SET y.is_active = @IsActive,y.updated_by = @CreatedBy,y.updated_datetime = @CreatedOn ,y.bill_id = @BillId
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






        public async Task<ServiceResponse<bool>> CancelAllOrder()
        {
            bool IsSuccess = false;
            ServiceResponse<bool> obj = new ServiceResponse<bool>();
            string Message = string.Empty;
     
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    

                    string sqlQuery = @"Update y SET y.is_active = 'false',y.updated_by = 1,y.updated_datetime = getDate() ,y.bill_id = 0
from sales.Order_Master x Inner Join sales.Order_Item_Details y on x.id = y.order_id 
inner join  [dbo].[CarpetNumber] z on  z.TStockNo = y.stock_id
Where z.Pack >= 101
Update y SET y.is_active = 'false',y.updated_by = 1
from sales.Order_Item_Details x Inner Join sales.Order_Payment y on x.order_id = y.order_id 
inner join  [dbo].[CarpetNumber] z on  z.TStockNo = x.stock_id
Where z.Pack >= 101
Update x SET x.PackingDetailId = 0,x.packsource = '',x.Pack = 0,x.Pack_Date = null
from[dbo].[CarpetNumber] x inner join[sales].Order_Item_Details y on x.TStockNo = y.stock_id
where x.Pack >= 101";

                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery);
        

                    if (rowsAffected > 0)
                    {

                        IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
               
                Message = ex.Message;
                IsSuccess = false;
            }
         


            obj.Data = IsSuccess;
            obj.Result = IsSuccess;
            obj.Message = IsSuccess ? "Data Found." : "No Data found.";
            return obj;
        }






    }
}
