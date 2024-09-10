namespace ClaimsService.Repository
{
    public class ResponseHeader
    {
        public int Id { get; set; }
        public int ResponseId { get; set; }
        public string HeaderKey { get; set; }
        public string HeaderValue { get; set; }
        public int DataTypeId { get; set; }
    }
}
