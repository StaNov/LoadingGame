using UnityEngine;
using System.Collections;

public class EndingManager : MonoBehaviour {

    void Awake () {
        if (PlayerPrefs.HasKey(LevelEnder.levelEnd.ToString())) {
            return;
        }

        PlayerPrefs.SetString(LevelEnder.levelEnd.ToString(), "unlocked");
        PlayerPrefs.Save();
        // TODO register achievement
    }
}
