using CookBook.Application.Services.Token;

namespace Utils.Test.Services;

public class TokenBuilder
{
    public static TokenController Instance()
    {
        return new TokenController(1000, "YWNpb05vVXNoQW1FTnRFVmVSQmFTS1lXSEVJc2tPd30na2g/VVpTam16bCs2NUp0cTBJMUolbF1cYw==");
    }
}
