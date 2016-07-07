using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CompoundCollider : MonoBehaviour {

    public Action onClickAction { private get; set; }
	
	void OnMouseDown () {
	    onClickAction();
	}
}
