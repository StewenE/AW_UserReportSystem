using Domain;
using Test.Helpers;

namespace Test.Domain {
	[TestClass]
	public class ReportTests : SealedTests<Report, Entity> {
		[TestMethod] public void NameTest() => PropertyTest();
		[TestMethod] public void DescriptionTest() => PropertyTest();
		[TestMethod] public void SubmitDateTest() => PropertyTest();
		[TestMethod] public void SolveByDateTest() => PropertyTest();

	}
}
