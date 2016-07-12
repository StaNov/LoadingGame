using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class LoadingBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public float maximumWidth = 400;
    public float secondsToLoad = 20;
    public RectTransform innerBarRectTransform;
    public Text percentsText;

    private int startPercents;
    private float startTime;
    private float endTime;
    private float timeToAddOnePercent;
    private bool dragging;

    private static LoadingBar instance;

    void Awake() {
        instance = this;
    }

    void Start() {
        startPercents = 0;
        startTime = Time.time;
        endTime = startTime + secondsToLoad;
        timeToAddOnePercent = (endTime - startTime) / 100;
        dragging = false;
    }

    void Update() {

        if (CurrentPercentsByTime() == 100) {
            LevelEnd levelEnd = DialogueSystem.DialogueStarted() ? LevelEnd.INTERRUPTED : LevelEnd.PATIENT;
            LevelEnder.EndGame(levelEnd);
        }

        if (!dragging) {
            UpdatePercents();
        }

        UpdatePercentsText();
    }

    public static void FillRestOfLoadingBar(float seconds) {
        instance.startPercents = instance.CurrentPercentsByTime();
        instance.startTime = Time.time;
        instance.endTime = instance.startTime + seconds;
    }

    private void UpdatePercents() {
        int currentPercents = CurrentPercentsByTime();

        innerBarRectTransform.anchoredPosition = new Vector2(
            maximumWidth * (currentPercents / 100f),
            innerBarRectTransform.anchoredPosition.y
        );
    }

    private void UpdatePercentsText() {
        percentsText.text = CurrentPercentsByPosition() + " %";
    }

    private int CurrentPercentsByTime() {
        if (Time.time >= endTime) {
            return 100;
        }

        float timeProgress = (Time.time - startTime) / (endTime - startTime);

        float result = startPercents + (timeProgress * (100 - startPercents));

        return Mathf.FloorToInt(result);
    }

    private int CurrentPercentsByPosition() {
        float position = innerBarRectTransform.anchoredPosition.x;

        float percents = (position / maximumWidth) * 100;

        int percentsInt = Mathf.FloorToInt(percents);

        return Mathf.Clamp(percentsInt, 0, 100);
    }

    public static void DestroySelf() {
        if (instance != null) {
            Destroy(instance.transform.parent.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        dragging = false;

        instance.startPercents = instance.CurrentPercentsByPosition();
        instance.startTime = Time.time;
        instance.endTime = instance.startTime + (100-instance.startPercents) * timeToAddOnePercent;
    }

    public void OnChange(Vector2 v) {
        if (!dragging) {
            return;
        }

        innerBarRectTransform.anchoredPosition = new Vector2(
            Mathf.Clamp(innerBarRectTransform.anchoredPosition.x, 0, maximumWidth),
            innerBarRectTransform.anchoredPosition.y
        );

        if (instance.CurrentPercentsByPosition() == 100) {
            DialogueSystem.Trigger(LevelEnd.DRAG);
            OnDisableInput();
            enabled = false;
        }
    }

    public static void OnDisableInput() {
        if (instance == null) {
            return;
        }

        instance.GetComponent<ScrollRect>().enabled = false;
    }
}
