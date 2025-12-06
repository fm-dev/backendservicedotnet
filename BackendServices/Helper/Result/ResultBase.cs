namespace BackendServices.Helper.Result
{
    public class ResultBase
    {
        public string ResultCode { get; set; }

        public string ResultMessage { get; set; }

        public int? ResultCountData { get; set; }

        public ResultBase()
        {
            ResultCode = "1000";
            ResultMessage = "Success";
            ResultCountData = 0;
        }
    }
   
}
