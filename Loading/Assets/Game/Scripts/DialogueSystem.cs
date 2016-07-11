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
    public DialogueLine[] dialogueLinesDrag;

    private int currentLinesIndex;
    private DialogueLine[] currentDialogueLines { get {
            switch (currentLevelEnd) {
                case LevelEnd.IMPATIENT:
                    return dialogueLinesKnocking;
                case LevelEnd.HEART:
                    return dialogueLinesHeart;
                case LevelEnd.SHAKE:
                    return dialogueLinesShake;
                case LevelEnd.DRAG:
                    return dialogueLinesDrag;
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

        if (currentLinesIndex >= currentDialogueLines.Length) {
            Debug.Log("No more dialogue lines.");
            yield break;
        }

        DialogueLine currentLine = currentDialogueLines[currentLinesIndex];

        if (currentLine.showNextLineAfterThis) {
            LoadingBar.DestroySelf();
            BackgroundSoundPlayer.Stop();
        }

        instance.text.text = currentLine.text;
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
        if (!instance.isDialogueShown) {
            return;
        }

        instance.wrapper.SetActive(false);
        instance.isDialogueShown = false;

        instance.currentLinesIndex++;

        if (instance.currentLinesIndex >= instance.currentDialogueLines.Length) {
            AcceptingInputStatus.DisableAcceptingInput();
            LevelEnder.EndGame(instance.currentLevelEnd);
            return;
        }

        if (instance.currentDialogueLines[instance.currentLinesIndex - 1].showNextLineAfterThis) {
            instance.wrapper.SetActive(true); // to not blink away for the one frame
            Trigger(instance.currentLevelEnd);
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