using UnityEngine;
using System.Collections;

public class EndingManager : MonoBehaviour {

    void Awake () {
        SocialManager.UnlockAchievement(LevelEnder.levelEnd);

        PlayerPrefs.SetString(LevelEnder.levelEnd.ToString(), "unlocked");
        PlayerPrefs.Save();
    }
}
