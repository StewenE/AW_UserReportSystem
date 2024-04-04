using Helpers.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Helpers {
	public abstract class SealedTests<TClass, TBaseClass> : ClassTests<TClass, TBaseClass> where TClass: new () {
		[TestMethod] public void IsSealedTest() => Assert.IsTrue(typeof(TClass)?.IsSealed);
		protected void PropertyTest() {
			var name = CallingMethod(nameof(PropertyTest));
			var propertyInfo = type.GetProperty(name);
			var t = propertyInfo?.PropertyType;
			var v = GetRandom.Any(t);
			Assert.IsNotNull(v, $"GetRandom.Any returns null for type <{t?.Name}>");
			propertyInfo?.SetValue(obj, v);
			Assert.AreEqual(v, propertyInfo?.GetValue(obj));
		}
		protected override TClass? createObject() {
			throw new NotImplementedException();
		}
	}
}
