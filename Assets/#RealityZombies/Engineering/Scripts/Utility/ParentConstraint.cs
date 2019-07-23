using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentConstraint : MonoBehaviour {

	protected Transform _transform;
	public new Transform transform {
		get {
			if (_transform == null) {
				_transform = GetComponent<Transform>();
			}
			return _transform;
		}
	}

	public Transform parent;
	
	// LateUpdate is called once per frame, after update. required for accurate positions of animated objects
	void LateUpdate () {
		if (parent != null) {
			transform.position = parent.position;
			transform.rotation = parent.rotation;
			transform.localScale = parent.localScale;
		}
	}
}
