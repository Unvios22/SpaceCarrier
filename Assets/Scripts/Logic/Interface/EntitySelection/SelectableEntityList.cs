using System.Collections.Generic;
using Model;

namespace Logic.Interface.EntitySelection {
	public class SelectableEntityList<T> : List<T> where T : ISelectableEntityMB{

		public SelectableEntityList() { }
		
		public SelectableEntityList(IEnumerable<T> collection) {
			foreach (var entity in collection) {
				Add(entity);
			}	
		}

		public new void Add(T obj) {
			obj.DisplaySelectionMarker();
			base.Add(obj);
		}

		public new void Remove(T obj) {
			obj.HideSelectionMarker();
			base.Remove(obj);
		}

		public new void Clear() {
			foreach (var element in this) {
				element.HideSelectionMarker();
			}
			base.Clear();
		}
	}
}