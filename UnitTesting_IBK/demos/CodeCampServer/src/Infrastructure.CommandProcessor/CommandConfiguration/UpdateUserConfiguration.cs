using CodeCampServer.Core.Services.BusinessRule;
using CodeCampServer.Infrastructure.CommandProcessor.Rules;
using CodeCampServer.UI.Models.Input;
using MvcContrib.CommandProcessor.Configuration;
using MvcContrib.CommandProcessor.Validation.Rules;

namespace CodeCampServer.Infrastructure.CommandProcessor.CommandConfiguration
{
	public class UpdateUserConfiguration : MessageDefinition<UserInput>
	{
		public UpdateUserConfiguration()
		{
			Execute<UpdateUserCommandMessage>().Enforce(
				v =>
					{
						v.Rule<ValidateNotNull>(c => c.Username).RefersTo(m => m.Username);
						v.Rule<ValidateNotNull>(c => c.Password).RefersTo(m => m.Password);
						v.Rule<ValidateNotNull>(c => c.Name).RefersTo(m => m.Name);
						v.Rule<ValidateNotNull>(c => c.EmailAddress).RefersTo(m => m.EmailAddress);
						v.Rule<ValidateNotNull>(c => c.ConfirmPassword).RefersTo(m => m.ConfirmPassword);
						v.Rule<ValidateEqualTo>(c => c.Password, c => c.ConfirmPassword).RefersTo(m => m.ConfirmPassword);
						v.Rule<UsernameMustBeUnique>().RefersTo(m => m.Username);
					});
		}
	}
}