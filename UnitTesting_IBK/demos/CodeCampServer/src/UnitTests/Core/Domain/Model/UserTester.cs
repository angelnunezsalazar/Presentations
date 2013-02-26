using CodeCampServer.Core.Domain.Bases;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain.Model
{
	[TestFixture]
	public class UserTester : TestBase
	{
		[Test]
		public void Should_be_admin_if_username_matches()
		{
			var user=new User {Username = User.ADMIN_USERNAME};

            var userIsAdmin = user.IsAdmin();

		    userIsAdmin.ShouldBeTrue();
		}
	}
}