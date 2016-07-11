using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class PlayAgainButton : MonoBehaviour {

    public GameObject adLoadingPanel;

    void Start() {
        adLoadingPanel.SetActive(false);
    }

    public void PlayAnAdAndRestart() {
        if (! AtLeastXEndingsWereUnlocked(3) || !Advertisement.IsReady("video")) {
            Restart();
            return;
        }

        adLoadingPanel.SetActive(true);
        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show("video", options);
    }

    private bool AtLeastXEndingsWereUnlocked(int x) {
        int count = 0;

        foreach (LevelEnd levelEnd in Enum.GetValues(typeof(LevelEnd))) {
            if (PlayerPrefs.HasKey(levelEnd.ToString())) {
                count++;
            }
        }
        
        return count >= x;
    }

    private void HandleShowResult(ShowResult showResult) {
        adLoadingPanel.SetActive(false);
        Restart();
    }

    private void Restart() {
        SceneManager.LoadScene(0);
    }
}
