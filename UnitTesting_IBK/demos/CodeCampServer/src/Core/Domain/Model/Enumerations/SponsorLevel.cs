using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Model.Enumerations
{
	public class SponsorLevel : Enumeration
	{
		public static SponsorLevel Silver = new SponsorLevel(1, "Silver");
		public static SponsorLevel Gold = new SponsorLevel(2, "Gold");
		public static SponsorLevel Platinum = new SponsorLevel(3, "Platinum");

		public SponsorLevel() {}

		public SponsorLevel(int value, string displayName)
			: base(value, displayName) {}
	}
}