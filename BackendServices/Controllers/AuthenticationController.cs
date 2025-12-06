using BackendServices.BindingModels.Auth;
using BackendServices.BusinessObjects.UserManagement;
using BackendServices.Helper.Result;
using BackendServices.Helper.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackendServices.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IBOUserManagement _boUserManagement;

        public  AuthenticationController(IBOUserManagement bOUserManagement)
        { 
            _boUserManagement = bOUserManagement;
        }

        [HttpPost("RegistrationUser")]
        public async Task<Result<string>> ListUser([FromBody]BReqRegistration req)
        {
            var result  = new Result<string>();
            try
            {
                result = await _boUserManagement.Registration(req);

            }
            catch(Exception ex)
            {
                result.SetException(ex);
            }
            return result;
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] BReqLogin req)
        {
            var result = new ResultBase();
            try
            {
                result = await _boUserManagement.LoginUser(req);
                return ActionResultExtensions.SetHttpStatus(result);

            }
            catch (Exception ex)
            { 
                return ActionResultExtensions.SetHttpException(result, ex);
            }
        }



    }
}
