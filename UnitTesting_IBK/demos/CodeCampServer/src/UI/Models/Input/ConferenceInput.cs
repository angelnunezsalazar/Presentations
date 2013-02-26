using System;
using System.ComponentModel.DataAnnotations;
using CodeCampServer.UI.Helpers.Attributes;
using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Input
{
	public class ConferenceInput : EventInput
	{
		public virtual Guid Id { get; set; }

		public virtual Guid UserGroupId { get; set; }

		[Required]
		public virtual string Name { get; set; }

		[Required]
		[RegularExpression(@"^[A-Za-z0-9\-]+$", ErrorMessage = "Key should only contain letters, numbers, and hypens.")]
		public virtual string Key { get; set; }

		[Required]
		[Label("Start Date")]
		public override DateTime StartDate { get; set; }

		[Required]
		public override DateTime EndDate { get; set; }

		[Required]
		[Label("Time Zone")]
		public override string TimeZone { get; set; }

		[Multiline]
		public virtual string Description { get; set; }

		public virtual bool HasRegistration { get; set; }

		[Required]
		[Label("Location")]
		public virtual string LocationName { get; set; }

		public virtual string LocationUrl { get; set; }

		public virtual string Address { get; set; }

		public virtual string City { get; set; }

		[Label("State")]
		public virtual string Region { get; set; }

		[Label("Zip Code")]
		public virtual string PostalCode { get; set; }

		[Label("Phone Number")]
		public virtual string PhoneNumber { get; set; }

		[Multiline]
		public string HtmlContent { get; set; }
	}
}