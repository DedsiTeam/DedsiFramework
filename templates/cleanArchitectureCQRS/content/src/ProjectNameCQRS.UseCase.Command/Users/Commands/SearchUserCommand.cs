using Dedsi.Ddd.CQRS.Commands;
using ProjectNameCQRS.Users.Dtos;

namespace ProjectNameCQRS.Users.Commands;

public record SearchUserCommand(string UserName,string Account,string Email) : DedsiCommand<SearchUserPagedResultDto>;