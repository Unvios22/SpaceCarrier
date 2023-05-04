using Enums;

namespace Model {
	public class Weapon {
		private string _name;
		private TargetType _targetType;
		private float _rateOfFire;
		private bool _isExplosive;
		private int _ammoCount;
		private int _weight;

		public string Name {
			get => _name;
			set => _name = value;
		}

		public TargetType TargetType {
			get => _targetType;
			set => _targetType = value;
		}

		public float RateOfFire {
			get => _rateOfFire;
			set => _rateOfFire = value;
		}

		public bool IsExplosive {
			get => _isExplosive;
			set => _isExplosive = value;
		}

		public int AmmoCount {
			get => _ammoCount;
			set => _ammoCount = value;
		}

		public int Weight {
			get => _weight;
			set => _weight = value;
		}
	}
}