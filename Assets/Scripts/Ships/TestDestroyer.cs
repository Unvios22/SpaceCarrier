using System.Collections.Generic;
using Model;

namespace Ships {
	public class TestDestroyer : Ship {
		public TestDestroyer(string name, List<Weapon> armaments, List<Model.Personnel> personnel, float topSpeed, int health) : base(name, armaments, personnel, topSpeed, health) { }
	}
}