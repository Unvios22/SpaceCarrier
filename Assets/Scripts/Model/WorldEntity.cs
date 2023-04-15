using System;
using System.Collections.Generic;
using EntityData;
using Sirenix.OdinInspector;

namespace Model {
	[Serializable]
	public abstract class WorldEntity {
		[ShowInInspector] private int _bearing;
		[ShowInInspector] private Position _position;
		[ShowInInspector] private float _speed;
		[ShowInInspector] private List<Modifier<IModifierAffectable>> _modifiers;

		public void AddModifier(Modifier<IModifierAffectable> modifier) {
			_modifiers.Add(modifier);
		}

		public void RemoveModifier(Modifier<IModifierAffectable> modifier) {
			_modifiers.Remove(modifier);
		}

		public Position Position {
			get => _position;
			set => _position = value;
		}

		public int Bearing {
			get => _bearing;
			set {
				if (value is < 0 or > 360) {
					throw new ArgumentException("Input bearing exceeds limit!");
				}
				_bearing = value;
			}
		}

		public float Speed {
			get => _speed;
			set => _speed = value;
		}
	}
}