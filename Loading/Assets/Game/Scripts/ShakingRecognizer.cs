using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakingRecognizer : MonoBehaviour {
	
    public Text text;

    void Start() {
        #if !DEBUG
        Destroy(text.gameObject);
        #endif
    }

	void Update () {
        float acceleration = Input.acceleration.sqrMagnitude;

        #if DEBUG
        text.text = Mathf.FloorToInt(Input.acceleration.sqrMagnitude).ToString();
        #endif

        if (acceleration > 4.5) {
            DialogueSystem.Trigger(LevelEnd.SHAKE);
        }
	}
}
