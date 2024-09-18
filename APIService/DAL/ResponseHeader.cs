namespace ClaimsService.DAL
{
    public class ResponseHeader
    {
        public int Id { get; set; }
        public Response Response { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public DataType DataType { get; set; }
    }
}
