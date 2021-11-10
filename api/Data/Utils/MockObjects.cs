using Microsoft.AspNetCore.Http;

namespace api.Data.Utils
{
    public class MockObjects
    {
        public static HttpContext GetMockHttpContext(string scheme, string host, string path)
        {
            var mockHttpContext = new DefaultHttpContext();
            mockHttpContext.Request.Scheme = scheme;
            mockHttpContext.Request.Host = new HostString(host);
            mockHttpContext.Request.Path = path;
            mockHttpContext.Request.QueryString = new QueryString();
            return mockHttpContext;
        }
    }
}
