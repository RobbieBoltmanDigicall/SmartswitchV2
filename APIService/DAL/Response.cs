namespace ClaimsService.DAL
{
    public class Response
    {
        public int ResponseId { get; set; }
        public Route Route { get; set; }
        public ResponseBody? ResponseBody { get; set; }
        public ICollection<ResponseHeader>? ResponseHeaders { get; set; }
    }
}
