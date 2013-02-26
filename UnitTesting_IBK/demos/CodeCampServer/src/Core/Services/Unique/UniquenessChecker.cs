using System;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;

namespace CodeCampServer.Core.Services.Unique
{
	public class UniquenessChecker : IUniquenessChecker
	{
		private readonly IEntityCounter _counter; 

		public UniquenessChecker(IEntityCounter counter)
		{
			_counter = counter;
		}

		public bool IsUnique<TModel>(IEntitySpecification<TModel> specification)
			where TModel : PersistentObject
		{
			var inputValueIsUnique = _counter.CountByProperty(specification) == 0;
			return inputValueIsUnique;
		}

		public string BuildFailureMessage<TModel>(object value, Expression<Func<TModel, object>> propertyExpression) where TModel : PersistentObject
		{
			return string.Format("Property '{0}' should be unique, but the value '{1}' already exists.",
			                     UINameHelper.BuildNameFrom(propertyExpression), value);
		}
	}
}