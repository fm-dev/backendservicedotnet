using BackendServices.BindingModels.Auth;
using BackendServices.DataAccess.Context;
using BackendServices.DataAccess.Models.USERMANAGEMENT;
using BackendServices.Helper.Result;
using BackendServices.Helper.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace BackendServices.BusinessObjects.UserManagement
{
    public class BOUserManagement : IBOUserManagement
    {

        private readonly USERMANAGEMENTContext _dbuser;
        private readonly JWTService _jwtService;
        public BOUserManagement(USERMANAGEMENTContext userdb, JWTService jWTService)
        {
            _dbuser = userdb;
            _jwtService = jWTService;
        }
        public async Task<Result<string>> Registration(BReqRegistration req)
        {
            var result = new Result<string>();

            var begintran = await _dbuser.Database.BeginTransactionAsync();
            try
            {
                var checkdata = await _dbuser.users.Where(x => x.name.Equals(req.name) || x.email.Equals(req.email)).FirstOrDefaultAsync();

                if(checkdata != null)
                {
                    return result.SetBadRequest("Username or email is exist");
                }

                var data = new users
                {
                    name = req.name,
                    email = req.email,
                    password = PasswordUtils.HashPassword(req.password),
                    created_at = DateTime.Now
                };

                await _dbuser.users.AddAsync(data);

                await _dbuser.SaveChangesAsync();

                await begintran.CommitAsync();

                result.Data = "succes";

            }
            catch (Exception ex)
            {

                await begintran.RollbackAsync();

                result.SetException(ex);
            }
            return result;
        }

        public async Task<Result<BResDataUserLogin>> LoginUser(BReqLogin req)
        {
            var result = new Result<BResDataUserLogin>();

            try
            {
                var checkDataUser = await _dbuser.users.Where(x => x.name.Equals(req.username)).FirstOrDefaultAsync();

                if(checkDataUser == null)
                {
                    result.SetNotFound("Data User not found");

                    return result;
                }
                else
                {
                    if (PasswordUtils.VerifyPassword(checkDataUser.password, req.password))
                    {
                        var data = new BResDataUserLogin
                        {
                            email = checkDataUser.email,
                            username = checkDataUser.name,
                            token = _jwtService.GenerateToken(checkDataUser.email)
                        };
                        result.Data = data;
                    }
                }
            }
            catch(Exception ex)
            {
                result.SetException(ex);
            }
            return result;
        }

    }
}
