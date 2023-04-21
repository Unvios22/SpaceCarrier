
using System;
using System.Collections.Generic;
using Model.Entities.EntityData;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace Model {
	[Serializable]
	public abstract class WorldEntity {

		[ShowInInspector] private string _name;
		[ShowInInspector] private int _bearing;
		[ShowInInspector] private Position _position;
		[ShowInInspector] private float _speed;
		[ShowInInspector] private List<Modifier<IModifierAffectable>> _modifiers;

		public WorldEntity() {
			_position = new Position();
			_modifiers = new List<Modifier<IModifierAffectable>>();
		}
		
		public void AddModifier(Modifier<IModifierAffectable> modifier) {
			_modifiers.Add(modifier);
		}

		public void RemoveModifier(Modifier<IModifierAffectable> modifier) {
			_modifiers.Remove(modifier);
		}

		public void SetRandomBearing() {
			Bearing = Random.Range(0, 360);
		}

		public void SetRandomSpeed(float min, float max) {
			Speed = Random.Range(min, max);
		}
		
		public string Name {
			get => _name;
			set => _name = value;
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
			protected set => _speed = value;
		}
	}
}