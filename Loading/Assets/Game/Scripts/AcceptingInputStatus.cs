using UnityEngine;
using System.Collections;

public class AcceptingInputStatus : MonoBehaviour {

    public static bool isAcceptingInput { get; private set; }

    private static AcceptingInputStatus instance;
    
	void Awake () {
	    instance = this;
        isAcceptingInput = true;
	}

    public static void DisableAcceptingInput() {
        isAcceptingInput = false;
    }
}
