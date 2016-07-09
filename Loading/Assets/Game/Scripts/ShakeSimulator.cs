using UnityEngine;
using System.Collections;

public class ShakeSimulator : MonoBehaviour {
    
	void Start () {
        #if !UNITY_EDITOR
        Destroy(gameObject);
        #endif
    }
	
	public void SimulateShake () {
	    DialogueSystem.Trigger(LevelEnd.SHAKE);
	}
}
