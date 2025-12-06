using BackendServices.BindingModels.Auth;
using BackendServices.Helper.Result;

namespace BackendServices.BusinessObjects.UserManagement
{
    public interface IBOUserManagement
    {
        Task<Result<string>> Registration(BReqRegistration req);

        Task<Result<BResDataUserLogin>> LoginUser(BReqLogin req);
    }
}
