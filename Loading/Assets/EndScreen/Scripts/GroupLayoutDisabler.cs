using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GroupLayoutDisabler : MonoBehaviour {
    
	void Awake () {
	    GetComponent<LayoutGroup>().enabled = false;
	}
}
