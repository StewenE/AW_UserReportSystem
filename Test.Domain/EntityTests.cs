using Domain;
using Test.Helpers;

namespace Test.Domain {
	[TestClass]
	public class EntityTests : AbstractTests<Entity, object> {
		private class entity : Entity { }
		protected override Entity? createObject() => new entity();
		[TestMethod] public void IdTest() => PropertyTest();
	}
}
