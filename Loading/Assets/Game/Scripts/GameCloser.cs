using UnityEngine;
using System.Collections;

public class GameCloser : MonoBehaviour {

    private static GameCloser instance;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Quitting");
            Application.Quit();
        }
    }
}
