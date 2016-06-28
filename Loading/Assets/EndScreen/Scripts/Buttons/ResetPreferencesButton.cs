using UnityEngine;
using System.Collections;

public class ResetPreferencesButton : MonoBehaviour {

    #if !DEBUG
    void Start () {
        Destroy(gameObject);
        return;
    }
    #endif

    public void ResetPreferences () {
        PlayerPrefs.DeleteAll();
    }
}
