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
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"SELECT IPM.ITEM_FINISHED_ID as ItemFinishId,

IPM.Quality_Id as QualityId,
IPM.Color_Id ColorId, 
IPM.design_Id DesignId, 
IPM.Size_Id SizeId,
IPM.Shape_Id ShapeId,
IPM.Shadecolor_Id ShadeColorId,
ICM.CATEGORY_ID as CategoryId,
IM.ITEM_ID as ItemId,
IPM.ProductCode, 
ICM.CATEGORY_NAME as CategoryName, 
IM.ITEM_NAME as ItemName, 
ISNULL(Q.QualityName, '') QualityName, 
ISNULL(D.DesignName, '') DesignName, 
ISNULL(C.ColorName, '') ColorName,
ISNULL(SC.ShadeColorName, '') ShadeColorName, 
ISNULL(S.ShapeName, '') ShapeName, 
Q.Hscode HSNCode, 
Isnull(IM.ITEM_CODE, '') ItemCode, 
IsNull(Q.QualityCode, '')  QualityCode ,  
isnull(D.DesignCode,'') DesignCode,
isnull(C.ColorCode,'') ColorCode,
isnull(SZ.SizeCode,'') SizeCode, 
ISNULL(SZ.SizeMtr, '') SizeMtr, 
ISNULL(SZ.SizeFt, '') SizeFt, 
IsNull(SZ.WidthInch, 0) WidthINCH,
IsNull(SZ.LengthINCH, 0) LengthINCH, 
IsNull(SZ.HeightINCH, 0) HeightINCH, 
ipm.status as Status, 
IPM.MasterCompanyId,
IPM.Description, 
IsNull(ProdSizeFt, 0) ProdSizeFt, 
IsNull(ProdSizeMtr, 0) ProdSizeMtr, 
IsNull(ProdAreaFt, 0) ProdAreaFt, 
IsNull(ProdAreaMtr, 0) ProdAreaMtr,   
IsNull(ProdLengthMtr, 0) ProdLengthMtr,
UTM.UnitTypeID as UnitTypeId, 
UTM.UnitType 
FROM ITEM_PARAMETER_MASTER IPM(Nolock) JOIN ITEM_MASTER IM(Nolock) ON IM.ITEM_ID = IPM.ITEM_ID   
JOIN UNIT_TYPE_MASTER UTM(Nolock) ON UTM.UnitTypeID = IM.UnitTypeID   
JOIN ITEM_CATEGORY_MASTER ICM(Nolock) ON ICM.CATEGORY_ID = IM.CATEGORY_ID   
LEFT JOIN Quality Q(Nolock) ON Q.QualityId = IPM.QUALITY_ID   
LEFT JOIN Design D(Nolock) ON D.DesignId = IPM.DESIGN_ID   
LEFT JOIN Color C(Nolock) ON C.ColorId = IPM.COLOR_ID   
LEFT JOIN ShadeColor SC(Nolock) ON SC.ShadecolorId = IPM.SHADECOLOR_ID   
LEFT JOIN Shape S(Nolock) ON S.ShapeId = IPM.SHAPE_ID   
LEFT JOIN Size SZ(Nolock) ON SZ.SizeId = IPM.SIZE_ID
Where IPM.MasterCompanyId=@StoreId;";
                var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId }));
                var objItem = (from usr in result
                               select new ProductModel
                               {
                                   ItemFinishId = usr.ItemFinishId ,
                                   QualityId = usr.QualityId != null ? usr.QualityId : 0,
                                   ColorId = usr.ColorId != null ? usr.ColorId : 0,
                                   DesignId = usr.DesignId != null ? usr.DesignId : 0,
                                   ShapeId = usr.ShapeId != null ? usr.ShapeId : 0,
                                   ShadecolorId = usr.ShadecolorId != null ? usr.ShadecolorId : 0,
                                   CategoryId = usr.CategoryId != null ? usr.CategoryId : 0,
                                   ItemId = usr.ItemId != null ? usr.ItemId : 0,
                                   ProductCode = usr.ProductCode,
                                   CategoryName = usr.CategoryName,
                                   ItemName = usr.ItemName,
                                   QualityName = usr.QualityName,
                                   DesignName = usr.DesignName,
                                   ColorName = usr.ColorName,
                                   ShadeColorName = usr.ShadeColorName,
                                   ShapeName = usr.ShapeName,
                                   HSNCode = usr.HSNCode,
                                   QualityCode = usr.QualityCode,
                                   DesignCode = usr.DesignCode,
                                   ColorCode = usr.ColorCode,
                                   SizeCode = usr.SizeCode,
                                   Width = usr.WidthINCH,
                                   Length = usr.LengthINCH,
                                   Height = usr.HeightINCH,
                                   Status = usr.Status,
                                   StoreId = usr.MasterCompanyId,
                                   Description = usr.Description,
                                   UnitTypeId = usr.UnitTypeId != null ? usr.UnitTypeId : 0,
                                   UnitType = usr.UnitType,

                               });
                obj.Data = objItem;
                obj.Result = obj.Data.Count() > 0 ? true : false;
                obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
            }
            return obj;
        }




    }
}
