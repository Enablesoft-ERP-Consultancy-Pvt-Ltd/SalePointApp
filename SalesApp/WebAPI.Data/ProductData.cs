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


namespace SalesApp.WebAPI.Data
{
    public class ProductData : IProductData
    {
        private IConfiguration configuration;
        public ProductData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        //public async Task<ServiceResponse<UserModel>> LogInUser(LoginModel model)
        //{
        //    ServiceResponse<UserModel> obj = new ServiceResponse<UserModel>();
        //    try
        //    {
        //        using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
        //        {
        //            string sqlQuery = "select * from tblUser where UserName=@UserName and UserPassword=@UserPassword and IsActive=@IsActive";

        //            var result = (await connection.QueryAsync(sqlQuery,
        //                new
        //                {
        //                    @UserName = model.UserName,
        //                    @UserPassword = model.Password,
        //                    @IsActive = model.IsActive
        //                })).FirstOrDefault();

        //            if (result.UserId > 0)
        //            {

        //                var resObj = new UserModel
        //                {
        //                    UserId = result.UserId,

        //                    UserName = result.UserName,
        //                };

        //                string sqlstr = "insert Into tblLogin(UserId,LogIn) Values(@UserId,@LogIn) select SCOPE_IDENTITY();";
        //                resObj.LoginInId = (long)(connection.Query<long>(sqlstr, new { @UserId = result.UserId, @LogIn = DateTime.Now }).First());

        //                obj.Data = resObj;
        //                obj.Result = obj.Data.LoginInId > 0 ? true : false;
        //                obj.Message = obj.Data.LoginInId > 0 ? "Data Found." : "No Data found.";

        //            }
        //        };

        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.Message = ex.Message;
        //        return obj;
        //    }
        //}

        //public async Task<ServiceResponse<string>> LogOutUser(int userId)
        //{
        //    ServiceResponse<string> obj = new ServiceResponse<string>();
        //    try
        //    {
        //        using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
        //        {
        //            string sqlQuery = "Update tblLogin Set LogOut=@LogOut Where UserId=@UserId; ";

        //            int rowsAffected = cnn.Execute(sqlQuery, new { @LogOut = DateTime.Now, @UserId = userId });
        //            if (rowsAffected > 0)
        //            {
        //                obj.Result = true;
        //                obj.Data = "Sucessfully  Created.";
        //            }
        //            else
        //            {
        //                obj.Data = null;
        //                obj.Message = "Failed new creation.";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        obj.Message = ex.Message;
        //        return obj;
        //    }
        //    finally
        //    {

        //    }
        //    return obj;
        //}

        //public async Task<ServiceResponse<string>> AddUser(AccountUserModel _model)
        //{
        //    ServiceResponse<string> sres = new ServiceResponse<string>();
        //    IDbTransaction transaction = null;

        //    try
        //    {
        //        using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
        //        {
        //            if (cnn.State != ConnectionState.Open)
        //                cnn.Open();
        //            transaction = cnn.BeginTransaction();

        //            string _query = "INSERT INTO tblUser (UserKey,UserType,UserName,UserPassword,Organization,Title,SSN,FirstName,MiddleName,LastName,DOB,Email,CellPhone,HomePhone,EmgPhone,EmgContact,Gender,MaritalStatus,Ethnicity,SupervisorId,IsActive,CreatedOn,CreatedBy) VALUES (@UserKey,@UserType,@UserName,@UserPassword,@Organization,@Title,@SSN,@FirstName,@MiddleName,@LastName,@DOB,@Email,@CellPhone,@HomePhone,@EmgPhone,@EmgContact,@Gender,@MaritalStatus,@Ethnicity,@SupervisorId,@IsActive,@CreatedOn,@CreatedBy); select SCOPE_IDENTITY();";

        //            _model.UserId = (int)(cnn.ExecuteScalar<int>(_query, _model, transaction));
        //            string sqlQuery = "Insert Into tblAddress (UserId,AddressType,FlatNo,Address,City,Country,State,ZipCode,Longitude,Latitude,CreatedOn,CreatedBy) Values (@UserId,@AddressType,@FlatNo,@Address,@City,@Country,@State,@ZipCode,@Longitude,@Latitude,@CreatedOn,@CreatedBy);";
        //            int rowsAffected = cnn.Execute(sqlQuery, new
        //            {
        //                @UserId = _model.UserId,
        //                @AddressType = 1,
        //                @FlatNo = _model.HomeAddress.FlatNo,
        //                @Address = _model.HomeAddress.Address,
        //                @City = _model.HomeAddress.City,
        //                @Country = "USA",
        //                @State = _model.HomeAddress.State,
        //                @ZipCode = _model.HomeAddress.ZipCode,
        //                @Longitude = _model.HomeAddress.Longitude,
        //                @Latitude = _model.HomeAddress.Latitude,
        //                @CreatedOn = _model.CreatedOn,
        //                @CreatedBy = _model.CreatedBy
        //            }, transaction);
        //            transaction.Commit();
        //            if (rowsAffected > 0)
        //            {
        //                sres.Result = true;
        //                sres.Data = "Sucessfully  Created.";
        //            }
        //            else
        //            {
        //                sres.Data = null;
        //                sres.Message = "Failed new creation.";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        if (transaction != null)
        //        {
        //            transaction.Rollback();
        //        }
        //        sres.Message = ex.Message;
        //        return sres;
        //    }
        //    finally
        //    {
        //        if (transaction != null)
        //            transaction.Dispose();
        //    }
        //    return sres;
        //}

        //public async Task<ServiceResponse<IEnumerable<AccountUserModel>>> GetUser(int userType)
        //{
        //    ServiceResponse<IEnumerable<AccountUserModel>> obj = new ServiceResponse<IEnumerable<AccountUserModel>>();
        //    using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
        //    {
        //        string sql = "Select * from tblUser Where UserType=@UserType; ";
        //        IEnumerable<AccountUserModel> results = (await connection.QueryAsync<AccountUserModel>(sql,
        //                 new { @UserType = userType }));
        //        obj.Data = results;
        //        obj.Result = results.Any() ? true : false;
        //        obj.Message = results.Any() ? "Data Found." : "No Data found.";
        //    }
        //    return obj;
        //}


        public async Task<ServiceResponse<IEnumerable<ProductModel>>> GetProductList(int StoreId)
        {
            ServiceResponse<IEnumerable<ProductModel>> obj = new ServiceResponse<IEnumerable<ProductModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("ERPConnection").ToString()))
            {
                string sql = @"SELECT distinct IM.MasterCompanyId,IM.ITEM_ID as ItemId,IPM.ITEM_FINISHED_ID as ItemFinishId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,IPM.Shadecolor_Id ShadeColorId,
ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode, IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
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
Where IM.MasterCompanyId=@StoreId and stock.CurrentProStatus=1;";




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
                                   ProductImages = itmGroup.Where(x => x.ImagePath != null).Select(x => (string)x.ImagePath).ToList(),
                                   Price = itmGroup.FirstOrDefault().Price != null ? itmGroup.FirstOrDefault().Price : 0,
                                   Stocks = itmGroup.Select(x => (long)x.StockNo).ToList(),
                                   StockNos = itmGroup.Select(x => (string)x.TStockNo).ToList(),

                               });
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
                string sql = @"SELECT distinct IM.MasterCompanyId,IM.ITEM_ID as ItemId,IPM.ITEM_FINISHED_ID as ItemFinishId,IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, IPM.design_Id DesignId, IPM.Size_Id SizeId,IPM.Shape_Id ShapeId,IPM.Shadecolor_Id ShadeColorId,
ICM.CATEGORY_ID as CategoryId,IPM.ProductCode, 
IM.ITEM_NAME as ItemName, ICM.CATEGORY_NAME as CategoryName, ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, ISNULL(C.ColorName, '') ColorName,ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, Q.Hscode HSNCode, Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode, IsNull(SZ.WidthInch, 0) Width,IsNull(SZ.LengthINCH, 0) Length,
IsNull(SZ.HeightINCH, 0) Height, ipm.status as Status, 
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
Where IM.MasterCompanyId=@StoreId and stock.CurrentProStatus=1;";

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














        public async Task<Tuple<int, bool>> CreateOrder(OrderModel _model)
        {
            string Message = string.Empty;
            IDbTransaction transaction = null;
            try
            {

                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();

                    string Query;
                    Query = @"INSERT INTO [sales].[Order_Master]
([mirror_id],[sale_date],[transaction_id],[delievery_type],[port_type],[description],[unit],[created_datetime],
[created_by],[is_active],[sale_status],[session_year],[DISCOUNTPER])
VALUES
(@MirrorId,@SaleDate,@TransactionId,@DelieveryType,@PortType,@Description,@Unit,@CreatedOn,
@CreatedBy,@IsActive,@SaleStatus,@SessionYear,@DisCountPer);
select SCOPE_IDENTITY();
";
                    _model.OrderId = (await cnn.ExecuteScalarAsync<int>(Query, new
                    {
                        @MirrorId = _model.MirrorId,
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

                    _model.ItemList.ForEach(x => x.OrderId = _model.OrderId);

                    string sqlQuery = @"INSERT INTO [sales].[Order_Item_Details]
([trans_id],[stock_id],[order_id],[order_type],[order_type_prefix],[sale_type],[qty],[currency_type],
[price],[price_inr],[conversion_rate],[unit],[item_type],[item_desc],[created_datetime],
[created_by],[is_active],[session_year],[hsncode],[finishedid])
VALUES
(@TransId,@StockId,@OrderId,@OrderType,@OrderTypePrefix,@SalesType,@Qty,@CurrencyType,
@Price,@PriceINR,@ConversionRate,@Unit,@ItemType,@ItemDescription,@CreatedOn,@CreatedBy,@IsActive,@SessionYear,@HsnCode,@FinishedId);

Update [dbo].[CarpetNumber] Set PackDate=@CreatedOn,Pack=2,PackSource=@PackSource,PackingDetailId=SCOPE_IDENTITY(),PackingId=@OrderId
Where TStockNo=@StockId;";

                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model.ItemList, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        return Tuple.Create(_model.OrderId, true);
                    }
                    else
                    {
                        return Tuple.Create(-1, false);

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
                return Tuple.Create(-1, false);
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }

        }

        public async Task<bool> AddPayment(OrderPaymentModel _model)
        {
            bool IsSuccess = false;

            string Message = string.Empty;
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();

                    string sqlQuery = @"INSERT INTO [sales].[Order_Payment]
([order_id],[pay_mode],[card_type],[amount],[amout_hd],[IGST],[GST],[pay_date],[currency_type],[created_datetime]
,[created_by],[updated_by],[update_datetime],[is_active],[paylaterstatus],[paylaterdate])
VALUES
(@OrderId,@PaymentMode,@CardType,@Amount,@Amount,@IGST,@GST,@PaymentDate,@Currency,CreatedOn
,@CreatedBy,@CreatedOn,@CreatedBy,@IsActive,@PaylaterStatus,@PaymentDate)";

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
            return IsSuccess;
        }



















    }
}
