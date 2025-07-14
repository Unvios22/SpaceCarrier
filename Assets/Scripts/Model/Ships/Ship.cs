using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Model {
	
	[Serializable]
	public class Ship : CommandableEntity {
		
		[ShowInInspector] private List<Weapon> _armaments;
		[ShowInInspector] private List<Personnel> _personnel;
		[ShowInInspector] private float _topSpeed;
		[ShowInInspector] private int _health;

		//TODO: Add fuel
		//TODO: Add ship frame definition
		//TODO: Add modules (radar, sonar, etc.)

	}
}