using Microsoft.AspNetCore.Http;
using Moq;

namespace Utils.Test.Services;

public class ResponseCookieBuilder
{

    public static IResponseCookies Instance()
    {
        const string CookieKey = "mycookie";
        var requestCookiesMock = new Mock<IRequestCookieCollection>();
        requestCookiesMock.SetupGet(c => c[CookieKey]).Returns(CookieKey);

        var responseCookiesMock = new Mock<IResponseCookies>();
        responseCookiesMock.Setup(c => c.Delete(CookieKey)).Verifiable();

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(ctx => ctx.Request.Cookies)
                        .Returns(requestCookiesMock.Object);
        httpContextMock.Setup(ctx => ctx.Response.Cookies)
                        .Returns(responseCookiesMock.Object);
        return responseCookiesMock.Object;
    }
}
