using System.ComponentModel;

namespace ClaimsService.Enums
{
    public enum DataType
    {
        Boolean = 1,
        Byte = 2,
        SByte = 3,
        Char = 4,
        Decimal = 5,
        Double = 6,
        Float = 7,
        Int = 8,
        UInt = 9,
        Long = 10,
        ULong = 11,
        Short = 12,
        UShort = 13,
        String = 14,
        Object = 15,
        DateTime = 16,
        Guid = 17,
        TimeSpan = 18,
        Uri = 19
    }

    public enum BodyTypes
    {
        [Description("form-data")]
        FormData = 1,
        [Description("x-www-form-urlencoded")]
        FormUrlEncoded = 2,
        Raw = 3,
        Binary = 4,
        GraphQL = 5
    }

    public enum ApplicationTypes
    {
        Text = 1,
        Javascript = 2,
        JSON = 3,
        HTML = 4,
        XML = 5
    }

    public enum RouteTypes
    {
        REST = 1,
        SOAP = 2
    }
}
