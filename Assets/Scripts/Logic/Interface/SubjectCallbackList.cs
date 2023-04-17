using System;
using System.Collections.Generic;

namespace Logic.Interface {
	public class SubjectCallbackList<T> : List<T> {
		//TODO: refactor class name to reflect functionality better
		private readonly Action<T> _onItemAdded;
		private readonly Action<T> _onItemRemoved;
		
		//TODO: refactor implementation of callbacks to be more robust
		
		public SubjectCallbackList(Action<T> onItemAdded, Action<T> onItemRemoved) {
			_onItemAdded = onItemAdded;
			_onItemRemoved = onItemRemoved;
		}

		public new void Add(T obj) {
			_onItemAdded(obj);
			base.Add(obj);
		}

		public new void Remove(T obj) {
			_onItemRemoved(obj);
			base.Remove(obj);
		}

		public new void Clear() {
			foreach (var element in this) {
				_onItemRemoved(element);
			}
			base.Clear();
		}
	}
}