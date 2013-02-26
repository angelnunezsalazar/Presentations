using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;

namespace CodeCampServer.Core.Services.BusinessRule.DeleteUserGroup
{
	public class DeleteUserGroupCommandHandler : ICommandHandler<DeleteUserGroupCommandMessage>
	{
		private readonly IUserGroupRepository _userGroupRepository;

		public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository)
		{
			_userGroupRepository = userGroupRepository;
		}

		public object Execute(DeleteUserGroupCommandMessage commandMessage)
		{
			_userGroupRepository.Delete(commandMessage.UserGroup);
			return commandMessage.UserGroup;
		}
	}
}