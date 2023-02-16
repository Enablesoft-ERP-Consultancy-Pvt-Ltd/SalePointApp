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
                string sql = @"Select p.UserId,p.UserType,p.FirstName,p.MiddleName,p.LastName,p.Email,p.CellPhone,p.HomePhone,p.EmgPhone,p.EmgContact,
q.Owner,q.FlatNo,q.Address,q.City,q.Country,q.State,q.ZipCode,
p.FirstName as SuprFName,p.MiddleName as SuprMName,p.LastName as SuprLName
from tblUser p  Left Join tblAddress q on p.UserId=q.UserId
Left Join tblUser s on p.SupervisorId=s.UserId
Where p.UserId=@UserId;";
                var result = (await connection.QueryAsync(sql, new { @StoreId = StoreId }));
                var objItem = (from usr in result
                               select new ProductModel
                               {
                                   //UserId = usr.UserId,
                                   //UserType = usr.UserType,
                                   //Name = new NameClass
                                   //{
                                   //    FirstName = usr.FirstName,
                                   //    MiddleName = usr.MiddleName,
                                   //    LastName = usr.LastName,
                                   //},
                                   //Supervisor = new NameClass
                                   //{
                                   //    FirstName = usr.SuprFName,
                                   //    MiddleName = usr.SuprMName,
                                   //    LastName = usr.SuprLName,
                                   //},
                                   //CellPhone = usr.CellPhone,
                                   //Email = usr.Email,
                                   //HomePhone = usr.HomePhone,
                                   //EmergPhone = usr.EmgPhone,
                                   //EmergContact = usr.EmgPhone,

                                   //Address = new AddressView()
                                   //{
                                   //    LocationDetail = usr.Address,
                                   //    Owner = usr.Owner,
                                   //    FlatNo = usr.FlatNo,
                                   //    Country = usr.Country,
                                   //    State = usr.State,
                                   //    City = usr.City,
                                   //    ZipCode = usr.ZipCode,
                                   //},
                               });
                obj.Data = objItem;
                obj.Result = obj.Data.Count() > 0 ? true : false;
                obj.Message = obj.Data.Count() > 0 ? "Data Found." : "No Data found.";
            }
            return obj;
        }




    }
}
