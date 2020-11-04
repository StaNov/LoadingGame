using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour {

    public GameObject adLoadingPanel;

    void Start() {
        adLoadingPanel.SetActive(false);
    }

    public void PlayAnAdAndRestart() {
        SceneManager.LoadScene(0);
    }
}
