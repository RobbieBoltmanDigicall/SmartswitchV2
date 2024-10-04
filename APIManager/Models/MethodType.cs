namespace APIManager.Models
{
    public class MethodType
    {
        public int MethodTypeId { get; set; }
        public string MethodTypeName { get; set; }
        public RequestViewModel Route { get; set; }
    }
}
