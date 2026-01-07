using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace Dedsi.Core.Extensions;

public static class CurrentUserExtensions
{
    /// <summary>
    ///  获得 UserId
    /// </summary>
    /// <param name="currentUser"></param>
    /// <returns></returns>
    public static string GetClaimUserId(this ICurrentUser currentUser)
    {
        return GetClaimValue(currentUser, AbpClaimTypes.UserId);
    }
    
    /// <summary>
    ///  获得 UserName
    /// </summary>
    /// <param name="currentUser"></param>
    /// <returns></returns>
    public static string GetClaimUserName(this ICurrentUser currentUser)
    {
        return GetClaimValue(currentUser, AbpClaimTypes.UserName);
    }
    
    /// <summary>
    /// 获得 Claim Value
    /// </summary>
    /// <param name="currentUser"></param>
    /// <param name="claimType"></param>
    /// <returns></returns>
    public static string GetClaimValue(this ICurrentUser currentUser, string claimType)
    {
        var claim = currentUser.FindClaim(claimType);
        if (claim == null)
        {
            throw new UserFriendlyException($"claimType-{claimType} ：不存在。");
        }
        return claim.Value;
    }

    /// <summary>
    /// 用户信息输出日志
    /// </summary>
    /// <param name="currentUser"></param>
    /// <param name="logger"></param>
    /// <param name="message">附加信息</param>
    public static void LogUserInfo(this ICurrentUser currentUser, ILogger logger, string message = "")
    {
        logger.LogInformation($"当前用户信息：PhoneNumber = {currentUser.PhoneNumber} Name = {currentUser.Name} " + message);
    }
}