namespace Model {
	public abstract class Modifier<T> : ICrewActionable where T: IModifierAffectable {
		private T _modifierTarget;
		public abstract void ApplyModifierEffect(T target);
	}
}