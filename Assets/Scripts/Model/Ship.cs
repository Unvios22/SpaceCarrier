using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Model {
	
	[Serializable]
	public abstract class Ship : WorldEntity {
		
		[ShowInInspector] private string _name;
		[ShowInInspector] private List<Weapon> _armaments;
		[ShowInInspector] private List<Personnel> _personnel;
		[ShowInInspector] private float _topSpeed;
		[ShowInInspector] private int _health;
		
		protected Ship(string name, List<Weapon> armaments, List<Personnel> personnel, float topSpeed, int health) {
			_name = name;
			_armaments = armaments;
			_personnel = personnel;
			_topSpeed = topSpeed;
			_health = health;
		}
		
		//TODO: Add fuel
		//TODO: Add ship frame definition
		//TODO: Add modules (radar, sonar, etc.)
		
	}
}