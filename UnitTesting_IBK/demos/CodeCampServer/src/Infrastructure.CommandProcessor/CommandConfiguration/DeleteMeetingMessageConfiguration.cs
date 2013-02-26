using CodeCampServer.Core.Services.BusinessRule.DeleteMeeting;
using CodeCampServer.UI.Models.Messages;
using MvcContrib.CommandProcessor.Configuration;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class DeleteMeetingMessageConfiguration : MessageDefinition<DeleteMeetingInput>
	{
		public DeleteMeetingMessageConfiguration()
		{
			Execute<DeleteMeetingCommandMessage>();
		}
	}
}