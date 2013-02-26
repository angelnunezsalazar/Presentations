using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess.Impl;
using NBehave.Spec.NUnit;
using NHibernate;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	[TestFixture]
	public class EventRepositoryTester : DataTestBase
	{
		[Test]
		public void should_retrieve_events_for_a_usergroup()
		{
			var usergroup = new UserGroup();
			var conference1 = new Conference {UserGroup = usergroup};
			var meeting1 = new Meeting {UserGroup = usergroup};
			var meeting2 = new Meeting();

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(conference1);
				session.SaveOrUpdate(meeting1);
				session.SaveOrUpdate(meeting2);
				session.Flush();
			}

			IEventRepository repository = GetInstance<EventRepository>();
			Event[] events = repository.GetAllForUserGroup(usergroup);

			events.Length.ShouldEqual(2);
		}

		[Test]
		public void Should_retrieve_upcoming_events_for_a_usergroup()
		{
			SystemClockFactory.Default = () => new Clock(new DateTime(2009, 5, 5));
			var usergroup = new UserGroup();
			var event1 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 2),
			                  		EndDate = new DateTime(2009, 4, 6)
			                  	};
			var event4 = new Meeting
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 3),
			                  		EndDate = new DateTime(2009, 5, 4, 20, 0, 0)
			                  	};
			var event2 = new Conference
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 4),
			                  		EndDate = new DateTime(2009, 5, 5, 20, 0, 0)
			                  	};
			var event3 = new Meeting
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 5),
			                  		EndDate = new DateTime(2009, 5, 7)
			                  	};

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
				session.SaveOrUpdate(event1);
				session.SaveOrUpdate(event2);
				session.SaveOrUpdate(event3);
				session.SaveOrUpdate(event4);
				session.Flush();
			}

			IEventRepository repository = GetInstance<EventRepository>();
			Event[] events = repository.GetFutureForUserGroup(usergroup);

			events.Length.ShouldEqual(2);
			events[0].ShouldEqual(event2);
		}
	

		[Test]
		public void Should_retrieve_all_upcoming_events()
		{
			SystemClockFactory.Default = () => new Clock(new DateTime(2009, 5, 5));
			var usergroup = new UserGroup();
            var usergroup1 = new UserGroup();
			var event1 = new Conference
			                  	{
			                  		UserGroup = usergroup1,
			                  		StartDate = new DateTime(2000, 1, 2),
			                  		EndDate = new DateTime(2009, 4, 6)
			                  	};
			var event4 = new Meeting
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 3),
			                  		EndDate = new DateTime(2009, 5, 4, 20, 0, 0)
			                  	};
			var event2 = new Conference
			                  	{
			                  		UserGroup = usergroup1,
			                  		StartDate = new DateTime(2000, 1, 4),
			                  		EndDate = new DateTime(2009, 5, 5, 20, 0, 0)
			                  	};
			var event3 = new Meeting
			                  	{
			                  		UserGroup = usergroup,
			                  		StartDate = new DateTime(2000, 1, 5),
			                  		EndDate = new DateTime(2009, 5, 7)
			                  	};

			using (ISession session = GetSession())
			{
				session.SaveOrUpdate(usergroup);
                session.SaveOrUpdate(usergroup1);
				session.SaveOrUpdate(event1);
				session.SaveOrUpdate(event2);
				session.SaveOrUpdate(event3);
				session.SaveOrUpdate(event4);
				session.Flush();
			}

			IEventRepository repository = GetInstance<EventRepository>();
			Event[] events = repository.GetAllFutureEvents();

			events.Length.ShouldEqual(2);
			events[0].ShouldEqual(event2);
		}
	}
}