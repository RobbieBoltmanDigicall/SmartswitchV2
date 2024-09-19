namespace ClaimsService.DAL
{
    public class ResponseBodyParameter
    {
        public int ResponseBodyParameterId { get; set; }
        public ResponseBody ResponseBody { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DataType DataType { get; set; }
    }
}
