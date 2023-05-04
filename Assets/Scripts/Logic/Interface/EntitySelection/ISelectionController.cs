using Model;

namespace Logic.Interface.EntitySelection {
	public interface ISelectionController {
		
		public SelectableEntityList<ISelectableEntityMB> CurrentlySelectedEntities { get; }
		
	}
}