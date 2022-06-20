namespace CST.ViewModels.HelperVM
{
    public class StatusHelperVM
    {
        public StatusCategory StatusCategory { get; set; }
        public string Message { get; set; }
    }

    public enum StatusCategory
    {
        NotFound, //==> 0
        Failed, //==> 1
        Success, //==> 2
        Error, //==> 3
        DataExists // => 4
    }
}
