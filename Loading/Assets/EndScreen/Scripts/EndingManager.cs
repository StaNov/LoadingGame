using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Collections;

public class EndingManager : MonoBehaviour {

    public static bool levelEndAlreadyUnlocked { get; private set; }

    void Awake () {
        string levelEnd = LevelEnder.levelEnd.ToString();
        levelEndAlreadyUnlocked = PlayerPrefs.HasKey(levelEnd);

        StartCoroutine(UnlockAchievementAndSendAnalyticsEvent());

        PlayerPrefs.SetString(levelEnd, "unlocked");
        PlayerPrefs.Save();
    }

    private IEnumerator UnlockAchievementAndSendAnalyticsEvent() {
        yield return null;

        SocialManager.UnlockAchievement(LevelEnder.levelEnd);

        #if !UNITY_EDITOR
        Analytics.CustomEvent("EndSceneLoaded", new Dictionary<string, object> {
            { "LevelEnd", LevelEnder.levelEnd.ToString() },
            { "FirstTimeAchieved", ! levelEndAlreadyUnlocked }
        });
        #endif
    }
}
