using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DialogueSystem : MonoBehaviour, IPointerDownHandler {

    public GameObject wrapper;
    public Text text;
    public float secondsBeforeDialogMayBeClosed = 0.7f;
    
    public DialogueLine[] dialogueLinesKnocking;
    public DialogueLine[] dialogueLinesHeart;
    public DialogueLine[] dialogueLinesShake;

    private int currentLinesIndex;
    private DialogueLine[] currentDialogueLines { get {
            switch (currentLevelEnd) {
                case LevelEnd.IMPATIENT:
                    return dialogueLinesKnocking;
                case LevelEnd.HEART:
                    return dialogueLinesHeart;
                case LevelEnd.SHAKE:
                    return dialogueLinesShake;
                default:
                    Debug.LogError("No lines found for topic " + currentLevelEnd + "!!!");
                    return null;
            }
    } }

    private float timeLastLineShowed;
    private bool isDialogueShown;
    private LevelEnd currentLevelEnd;
    

    private static DialogueSystem instance;

    void Awake() {
        instance = this;
    }

    void Start () {
        wrapper.SetActive(false);
        isDialogueShown = false;
        currentLevelEnd = LevelEnd.NONE;
    }

    public static void Trigger(LevelEnd topic) {
        if (instance.isDialogueShown) {
            return;
        }

        if (topic != instance.currentLevelEnd) {
            instance.currentLinesIndex = 0;
        }

        instance.currentLevelEnd = topic;
        instance.isDialogueShown = true;
        instance.StartCoroutine(instance.TriggerCoroutine(topic));
    }

    IEnumerator TriggerCoroutine(LevelEnd topic) {
        // wait for one frame because we dont want the button to be pressed right after enabling
        yield return null;

        if (currentLinesIndex >= dialogueLinesKnocking.Length) {
            Debug.Log("No more dialogue lines.");
            yield break;
        }

        instance.text.text = currentDialogueLines[currentLinesIndex].text;
        currentLinesIndex++;
        timeLastLineShowed = Time.time;

        instance.wrapper.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Time.time - timeLastLineShowed < secondsBeforeDialogMayBeClosed) {
            return;
        }

        CloseDialogue();
    }

    public static void CloseDialogue() {

        instance.wrapper.SetActive(false);
        instance.isDialogueShown = false;

        if (instance.currentLinesIndex >= instance.currentDialogueLines.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(instance.currentLevelEnd);
        }
    }

    public static bool DialogueStarted() {
        return instance.currentLevelEnd != LevelEnd.NONE;
    }
}

[System.Serializable]
public class DialogueLine {

    [Multiline]
    public string text;

    public bool showNextLineAfterThis = false;
}