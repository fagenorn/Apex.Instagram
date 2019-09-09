namespace Apex.Instagram.API.Response.JsonMap
{
    public class GenericResponse : Response
    {
        public static Response GenericOkResponse { get; } = new GenericResponse {Status = Constants.Response.Instance.StatusOk};

        public static Response GenericFailResponse { get; } = new GenericResponse {Status = Constants.Response.Instance.StatusFail};
    }
}