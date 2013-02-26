using System.Web.Mvc;
using CodeCampServer.UI.Helpers.Filters;

namespace CodeCampServer.UI
{
	public class ConventionActionInvoker : ControllerActionInvoker
	{
		private readonly IConventionActionFilter[] _filters;

		public ConventionActionInvoker(IConventionActionFilter[] filters)
		{
			_filters = filters;
		}

		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
		{
			FilterInfo filters = base.GetFilters(controllerContext, actionDescriptor);

			foreach (IActionFilter filter in _filters)
			{
				filters.ActionFilters.Add(filter);
			}

			return filters;
		}
	}
}