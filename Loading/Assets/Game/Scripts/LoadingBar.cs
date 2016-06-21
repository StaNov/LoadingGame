using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour {

    public float maximumWidth = 400;
    public float secondsToLoad = 20;
    public RectTransform innerBarRectTransform;
    public Text percentsText;

    private int loadedPercents;

    private static LoadingBar instance;

    void Awake() {
        instance = this;
    }

    IEnumerator Start() {
        loadedPercents = 0;

        while (loadedPercents < 100) {
            AddOnePercent();
            yield return new WaitForSeconds(secondsToLoad / 100);
        }

        LevelEnder.EndGame(LevelEnd.PATIENT);
    }

    public static void FillRestOfLoadingBar(float seconds) {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.FillRestOfLoadingBarCoroutine(seconds));
    }

    private IEnumerator FillRestOfLoadingBarCoroutine(float seconds) {
        int percentsToFill = 100 - loadedPercents;
        float percentsInterval = seconds / (float) percentsToFill;

        while (loadedPercents < 100) {
            AddOnePercent();
            yield return new WaitForSeconds(percentsInterval);
        }
    }

    private void AddOnePercent() {
        loadedPercents++;

        innerBarRectTransform.anchoredPosition = new Vector2(
            maximumWidth * (loadedPercents / 100f),
            innerBarRectTransform.anchoredPosition.y
        );
        percentsText.text = loadedPercents + " %";
    }
}
