using System.ComponentModel.DataAnnotations;

namespace BackendServices.BindingModels.Auth
{
    public class BReqRegistration
    {
        public string name { get; set; }
        [EmailAddress(ErrorMessage ="this format not valid")]
        public string email { get; set; }
        public string password { get; set; }

    }
}
