using ErrorOr;

namespace Pineapple.GrpcMock.Application.Proxies;

public interface IProxyService
{
    /// <summary>
    /// Check if request should be proxy
    /// </summary>
    /// <param name="request">Request information</param>
    /// <returns>Return true if request should be proxy</returns>
    bool Can(CanProxyQueryDto query);

    /// <summary>
    /// Return host url to proxy
    /// </summary>
    /// <returns>Url or error</returns>
    string GetUrl(GetProxyUrlQueryDto query);
}
