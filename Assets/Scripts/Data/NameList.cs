using UnityEngine;

namespace Data {
	public static class NameList {

		public static string GetRandomTestShipName() {
			return TestShipNames[Random.Range(0, TestShipNames.Length)];
		}
		
		public static string[] TestShipNames = {
			"Boaty McBoat Face",
			"USN Nacho Boat",
			"Test Ship",
			"Vessel",
			"It floats!",
			"Alpha",
			"Beta",
			"Lorem",
			"Ipsum",
			"Dolor",
			"Sit",
			"Amet"
,		};
	}
}