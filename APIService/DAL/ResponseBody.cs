namespace ClaimsService.DAL
{
    public class ResponseBody
    {
        public int ResponseBodyId { get; set; }
        public int ResponseId { get; set; }
        public Response Response { get; set; }
        public BodyType BodyType { get; set; }
        public ApplicationType? ApplicationType { get; set; }
        public List<ResponseBodyParameter>? Parameters { get; set; }
    }
}
