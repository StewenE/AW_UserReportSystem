using Helpers.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Helpers {
	[TestClass]
	public abstract class AbstractTests<TClass, TBaseClass> : BaseTests {
		protected TClass? obj;
		[TestInitialize]
		public override void TestInitialize() {
			base.TestInitialize();
			obj = createObject();
		}

        protected abstract TClass? createObject();
		protected override Type? type => typeof(TClass);
		[TestMethod] public void IsBaseClassTest() 
			=> Assert.AreEqual(typeof(TClass)?.BaseType?.Name, typeof(TBaseClass).Name);
		[TestMethod] public virtual void IsAbstractTest() => Assert.IsTrue(typeof(TClass)?.IsAbstract);
		protected void PropertyTest() {
			var name = CallingMethod(nameof(PropertyTest));
			var propertyInfo = type.GetProperty(name);
			var t = propertyInfo?.PropertyType;
			var v = GetRandom.Any(t);
			Assert.IsNotNull(v, $"GetRandom.Any returns null for type <{t?.Name}>");
			propertyInfo?.SetValue(obj, v);
			Assert.AreEqual(v, propertyInfo?.GetValue(obj));
		}
	}
}
