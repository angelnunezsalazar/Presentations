using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Model;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public abstract class KeyedRepositoryTester<T, TRepository> : RepositoryTester<T, TRepository>
		where TRepository : IKeyedRepository<T> where T : KeyedObject, new()
	{
		[Test]
		public virtual void Should_get_by_key()
		{
			var one = new T();
			var two = new T();
			var three = new T();
			one.Key = "key";
			two.Key = "key2";
			three.Key = "key3";
			PersistEntities(one, two, three);

			TRepository repository = CreateRepository();

			T returnedFromDatabase = repository.GetByKey("key");
			returnedFromDatabase.ShouldEqual(one);
		}

		[Test]
		public void Should_be_able_to_instantiate()
		{
			TRepository repository = CreateRepository();
			repository.ShouldNotBeNull();
		}
	}
}