﻿using UnityEngine;
using System.Collections;

public class ShakeSimulator : MonoBehaviour {
    
	void Start () {
        #if !UNITY_EDITOR
        Destroy(gameObject);
        #endif
    }

    public void SimulateShake() {
        if (AcceptingInputStatus.isAcceptingInput) {
            DialogueSystem.Trigger(LevelEnd.SHAKE);
        }
	}
}
