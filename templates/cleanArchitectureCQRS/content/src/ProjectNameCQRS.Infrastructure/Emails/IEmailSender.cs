namespace ProjectNameCQRS.Emails;

public interface IEmailSender
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    Task SendEmailAsync(string content);
}