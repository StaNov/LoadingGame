using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEnder : MonoBehaviour {

    private static LevelEnder instance;
    public static LevelEnd levelEnd {get; private set;}

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            levelEnd = LevelEnd.TEST;
        } else {
            Destroy(gameObject);
        }
    }

    public static void EndGame(LevelEnd levelEnd) {
        LevelEnder.levelEnd = levelEnd;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

public enum LevelEnd {
    TEST, PATIENT, IMPATIENT
}