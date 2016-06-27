using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class EndingManager : MonoBehaviour {

    void Awake () {
        LevelEnd levelEnd = LevelEnder.levelEnd;

        SocialManager.UnlockAchievement(levelEnd);
        Analytics.CustomEvent("EndSceneLoaded", new Dictionary<string, object> {
            { "LevelEnd", levelEnd },
            { "FirstTimeAchieved", ! PlayerPrefs.HasKey(levelEnd.ToString()) }
        });

        PlayerPrefs.SetString(levelEnd.ToString(), "unlocked");
        PlayerPrefs.Save();
    }
}
