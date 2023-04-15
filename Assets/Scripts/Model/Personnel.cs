namespace Model {
	public abstract class Personnel {
		
		private int _experience;
		private int _tiredness;

		public int Experience {
			get => _experience;
			set => _experience = value;
		}

		public int Tiredness {
			get => _tiredness;
			set => _tiredness = value;
		}
	}
}