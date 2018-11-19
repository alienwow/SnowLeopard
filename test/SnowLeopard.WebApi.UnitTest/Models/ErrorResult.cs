namespace SnowLeopard.WebApi.UnitTest.Models
{
    public class ErrorResult
    {
        public int Index { get; set; }
        public SessionTestResult Last { get; set; }
        public SessionTestResult Curr { get; set; }
    }

}
