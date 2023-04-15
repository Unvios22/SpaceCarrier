using System.Collections.Generic;
using Personnel;

namespace Model {
	public abstract class Aircraft : WorldEntity {
		
		private List<Weapon> _armaments;
		private string _name;
		private Pilot _pilot;
		private float _topSpeed;
		private float _agility;
		private float _altitude;
		private int _health;
		
		//TODO: add fuselage definition with different damage types
		//TODO: add fuel
		
		public List<Weapon> Armaments {
			get => _armaments;
			set => _armaments = value;
		}

		public string Name {
			get => _name;
			set => _name = value;
		}

		public Pilot Pilot {
			get => _pilot;
			set => _pilot = value;
		}
		
		public float TopSpeed {
			get => _topSpeed;
			set => _topSpeed = value;
		}
		
		public float Agility {
			get => _agility;
			set => _agility = value;
		}
		
		public float Altitude {
			get => _altitude;
			set => _altitude = value;
		}
		
		public int Health {
			get => _health;
			set => _health = value;
		}
	}
}