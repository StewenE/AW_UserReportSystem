using Helpers.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Helpers {
	[TestClass]
	public class AssemblyTests {
		[TestMethod]
		public void IsTested() {
			var assembly = GetClass.Assembly(GetType());
			var assemblyName = assembly.FullName?? string.Empty;
			var solutionName = assemblyName.Replace("Test.", "");
			var types = GetSolution.Types(solutionName).Select(x => x.Name);
			var tests = GetSolution.Types(assemblyName).Select(x => x.Name);
			foreach(var t in types) {
				if(tests.Contains(t + "Tests")) continue;
				Assert.Inconclusive($"Member <{t}> is not tested.");
			}
		}
	}
}
