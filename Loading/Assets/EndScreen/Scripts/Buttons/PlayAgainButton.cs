using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayAgainButton : MonoBehaviour {

    public GameObject adLoadingPanel;

    private float timeSinceAdLoadStarted;

    void Start() {
        adLoadingPanel.SetActive(false);
    }

    public void PlayAnAdAndRestart() {
        StartCoroutine(PlayAnAdAndRestartCoroutine());
    }

    private IEnumerator PlayAnAdAndRestartCoroutine() {
        adLoadingPanel.SetActive(true);
        timeSinceAdLoadStarted = Time.time;

        while (!Advertisement.IsReady()) {
            if (Time.time - timeSinceAdLoadStarted > 5) {
                Restart();
                yield break;
            }

            yield return null;
        }

        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show(null, options);
    }

    private void HandleShowResult(ShowResult showResult) {
        Restart();
    }

    public void Restart() {
        SceneManager.LoadScene(0);
    }
}
