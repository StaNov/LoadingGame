using UnityEngine;
using System.Collections;

public class SocialManager : MonoBehaviour {

    private static SocialManager instance;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Social.localUser.Authenticate(null);
        } else {
            Destroy(gameObject);
        }
    }

    public static void UnlockAchievement (LevelEnd levelEnd) {
        #if UNITY_EDITOR
        Debug.Log("Achievement unlocked: " + levelEnd);
        #else
        Social.ReportProgress(GetAchievementId(levelEnd), 100.0, null);
        #endif
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