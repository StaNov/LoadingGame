using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class EndingManager : MonoBehaviour {

    public static bool levelEndAlreadyUnlocked { get; private set; }

    void Awake () {
        LevelEnd levelEnd = LevelEnder.levelEnd;

        levelEndAlreadyUnlocked = PlayerPrefs.HasKey(levelEnd.ToString());

        SocialManager.UnlockAchievement(levelEnd);
        Analytics.CustomEvent("EndSceneLoaded", new Dictionary<string, object> {
            { "LevelEnd", levelEnd },
            { "FirstTimeAchieved", ! PlayerPrefs.HasKey(levelEnd.ToString()) }
        });

        PlayerPrefs.SetString(levelEnd.ToString(), "unlocked");
        PlayerPrefs.Save();
    }
}
