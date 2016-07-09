using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour {

    public float maximumWidth = 400;
    public float secondsToLoad = 20;
    public RectTransform innerBarRectTransform;
    public Text percentsText;

    private int startPercents;
    private float startTime;
    private float endTime;

    private static LoadingBar instance;

    void Awake() {
        instance = this;
    }

    void Start() {
        startPercents = 0;
        startTime = Time.time;
        endTime = startTime + secondsToLoad;
    }

    void Update() {

        if (CurrentPercents() == 100) {
            LevelEnd levelEnd = DialogueSystem.DialogueKnockingStarted() ? LevelEnd.INTERRUPTED : LevelEnd.PATIENT;
            LevelEnder.EndGame(levelEnd);
        }
        
        UpdatePercents();
    }

    public static void FillRestOfLoadingBar(float seconds) {
        instance.startPercents = instance.CurrentPercents();
        instance.startTime = Time.time;
        instance.endTime = instance.startTime + seconds;
    }

    private void UpdatePercents() {
        int currentPercents = CurrentPercents();

        innerBarRectTransform.anchoredPosition = new Vector2(
            maximumWidth * (currentPercents / 100f),
            innerBarRectTransform.anchoredPosition.y
        );
        percentsText.text = currentPercents + " %";
    }

    private int CurrentPercents() {
        if (Time.time >= endTime) {
            return 100;
        }

        float timeProgress = (Time.time - startTime) / (endTime - startTime);

        float result = startPercents + (timeProgress * (100 - startPercents));

        return Mathf.FloorToInt(result);
    }

    public static void DestroySelf() {
        Destroy(instance.transform.parent.gameObject);
    }
}
