using ProjectNameCQRS.Emails;
using ProjectNameCQRS.Users.DomainEvents;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace ProjectNameCQRS.Users.DomainHandlers
{
    public class CreateUserSendEmailEventHandler(IEmailSender emailSender) : ILocalEventHandler<CreateUserSendEmailEvent>, ITransientDependency
    {
        public Task HandleEventAsync(CreateUserSendEmailEvent sendEmailEventData)
        {
            return emailSender.SendEmailAsync($"注册成功，给 {sendEmailEventData.User.UserName} 发送邮件[{sendEmailEventData.User.Email}]");
        }
    }
}
