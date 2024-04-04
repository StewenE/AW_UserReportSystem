using System.Reflection;

namespace Helpers.Methods {
	public static class GetSolution {
		public static Assembly Assembly(string name) => System.Reflection.Assembly.Load(name);
		public static List<Type> Types(string assemblyName) => Types(Assembly(assemblyName));
		public static List<Type> Types(Assembly a)
			=> a.GetTypes().Where(x => !x.Name.StartsWith("<")).ToList();
	}
}
