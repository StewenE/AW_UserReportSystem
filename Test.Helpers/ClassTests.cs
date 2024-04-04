using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Helpers {
	public abstract class ClassTests<TClass, TBaseClass> : AbstractTests<TClass, TBaseClass> where TClass: new() {
		protected TClass? obj;
		[TestInitialize] public void TestInitialize() => obj = new TClass();
		[TestMethod] public override void IsAbstractTest() => Assert.IsFalse(typeof(TClass)?.IsAbstract);
		[TestMethod] public void CanCreateTest() => Assert.IsInstanceOfType(obj, typeof(TClass));
    }
}
