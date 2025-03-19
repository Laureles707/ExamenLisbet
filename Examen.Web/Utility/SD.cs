using Microsoft.AspNetCore.Mvc;

namespace Examen.Web.Utility
{
    public class SD
    {
        public static string CompraAPIBase {  get; set; }
        public static string GatewayAPI { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public const string TokenCookie = "JWTToken";
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
