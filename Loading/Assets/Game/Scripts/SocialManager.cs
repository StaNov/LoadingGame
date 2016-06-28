using UnityEngine;
using System.Collections;

public class SocialManager : MonoBehaviour {

    private static SocialManager instance;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Authenticate();
        } else {
            Destroy(gameObject);
        }
    }

    public static void UnlockAchievement (LevelEnd levelEnd) {
        instance.StartCoroutine(instance.UnlockAchievementCoroutine(levelEnd));
    }

    private IEnumerator UnlockAchievementCoroutine(LevelEnd levelEnd) {
        if (!Social.localUser.authenticated) {
            Authenticate();
        }

        while (!Social.localUser.authenticated) {
            yield return null;
        }

        #if UNITY_EDITOR
        Debug.Log("Achievement unlocked: " + levelEnd);
        #else
        Social.ReportProgress(GetAchievementId(levelEnd), 100.0, null);
        #endif
    }

    private void Authenticate() {
        Social.localUser.Authenticate((success) => {
            Debug.Log(success ? "Social autenticated" : "SOCIAL NOT AUTENTICATED!!!");
        });
    }

    static string GetAchievementId(LevelEnd levelEnd) {
        switch (levelEnd) {
            case LevelEnd.PATIENT:
                return "CgkIvon-9KIPEAIQAQ";
            case LevelEnd.IMPATIENT:
                return "CgkIvon-9KIPEAIQAg";
            case LevelEnd.INTERRUPTED:
                return "CgkIvon-9KIPEAIQBQ";
            case LevelEnd.VIOLENT:
                return "CgkIvon-9KIPEAIQAw";
            case LevelEnd.FIRE:
                return "CgkIvon-9KIPEAIQBA";
        }

        return null;
    }
}