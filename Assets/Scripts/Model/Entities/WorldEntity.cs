using System;
using System.Collections.Generic;
using Model.Entities.EntityData;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model.Entities {
	[Serializable]
	public abstract class WorldEntity {

		[ShowInInspector] private string _name;
		[ShowInInspector] private EntityPosition _entityPosition;
		[ShowInInspector] private EntityRotation _entityRotation;
		[ShowInInspector] private float _speed;
		[ShowInInspector] private List<Modifier<IModifierAffectable>> _modifiers;

		public WorldEntity() {
			_entityPosition = new EntityPosition();
			_entityRotation = new EntityRotation();
			_modifiers = new List<Modifier<IModifierAffectable>>();
		}
		
		public void AddModifier(Modifier<IModifierAffectable> modifier) {
			_modifiers.Add(modifier);
		}

		public void RemoveModifier(Modifier<IModifierAffectable> modifier) {
			_modifiers.Remove(modifier);
		}

		public void SetRandomRotation() {
			var randomAngle = Random.Range(0, 360);
			EntityRotation.WorldRotation = new Vector3(0, 0, randomAngle);
		}

		public void SetRandomSpeed(float min, float max) {
			Speed = Random.Range(min, max);
		}
		
		public string Name {
			get => _name;
			set => _name = value;
		}
		
		public EntityPosition EntityPosition {
			get => _entityPosition;
			set => _entityPosition = value;
		}

		public EntityRotation EntityRotation {
			get => _entityRotation;
			set => _entityRotation = value;
		}

		public float Speed {
			get => _speed;
			protected set => _speed = value;
		}
	}
}