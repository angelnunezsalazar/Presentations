using CodeCampServer.Infrastructure.NHibernate;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class SessionBuilderTester : IntegrationTestBase
	{
		[Test]
		public void Should_create_a_new_session()
		{
			var builder = new SessionBuilder();
			var session = builder.GetSession();
			session.ShouldNotBeNull();
			session.IsOpen.ShouldBeTrue();
		}

		[Test]
		public void Should_return_the_same_instance_second_time()
		{
			var builder = new SessionBuilder();
			var session = builder.GetSession();
			session.ShouldNotBeNull();

			var session2 = builder.GetSession();
			ReferenceEquals(session, session2).ShouldBeTrue();
		}

		[Test]
		public void Should_build_a_new_session_when_session_is_closed()
		{
			var builder = new SessionBuilder();
			var session = builder.GetSession();
			session.Close();

			var session2 = builder.GetSession();
			session2.IsOpen.ShouldBeTrue();
		}

		[Test]
		public void Should_build_a_new_session_when_session_is_disposed()
		{
			var builder = new SessionBuilder();
			var session = builder.GetSession();
			session.Dispose();

			var session2 = builder.GetSession();
			session2.IsOpen.ShouldBeTrue();
		}
	}
}