using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Collections;

public class EndingManager : MonoBehaviour {

    public static bool levelEndAlreadyUnlocked { get; private set; }

    void Awake () {
        LevelEnd levelEnd = LevelEnder.levelEnd;
        levelEndAlreadyUnlocked = PlayerPrefs.HasKey(levelEnd.ToString());

        StartCoroutine(SendAnalyticsEvent());

        SocialManager.UnlockAchievement(levelEnd);

        PlayerPrefs.SetString(levelEnd.ToString(), "unlocked");
        PlayerPrefs.Save();
    }

    private IEnumerator SendAnalyticsEvent() {
        yield return null;

        Debug.Log(Analytics.CustomEvent("EndSceneLoaded", new Dictionary<string, object> {
            { "LevelEnd", LevelEnder.levelEnd.ToString() },
            { "FirstTimeAchieved", ! levelEndAlreadyUnlocked }
        }));
    }
}
