using UnityEngine;
using GooglePlayGames;
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
        while (!Social.localUser.authenticated) {
            yield return null;
        }

        #if !UNITY_EDITOR
        Social.ReportProgress(GetAchievementId(levelEnd), 100.0, (success) => {
            if (success) {
                Debug.Log("Achievement reported: " + levelEnd);
            } else {
                Debug.LogError("Achievement NOT reported: " + levelEnd);
            }
        });
        #else
        Debug.Log("Achievement unlocked: " + levelEnd);
        #endif
    }

    private void Authenticate() {

        #if !UNITY_EDITOR
        PlayGamesPlatform.Activate();
        #endif

        Social.localUser.Authenticate((success) => {
            if (success) {
                Debug.Log("Social autenticated");
            } else {
                Debug.Log("SOCIAL NOT AUTENTICATED!!!");
            }
            
        });
    }

    static string GetAchievementId(LevelEnd levelEnd) {
        switch (levelEnd) {
            case LevelEnd.PATIENT:
                return AchievementsConstants.achievement_patient_ending;
            case LevelEnd.IMPATIENT:
                return AchievementsConstants.achievement_impatient_ending;
            case LevelEnd.INTERRUPTED:
                return AchievementsConstants.achievement_interrupted_ending;
            case LevelEnd.VIOLENT:
                return AchievementsConstants.achievement_violent_ending;
            case LevelEnd.FIRE:
                return AchievementsConstants.achievement_fire_ending;
            case LevelEnd.HEART:
                return AchievementsConstants.achievement_throw_out_ending;
            case LevelEnd.SHAKE:
                return AchievementsConstants.achievement_dirty_ending;
        }

        return null;
    }
}