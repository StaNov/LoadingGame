using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class DialogueSystem : MonoBehaviour {

    public GameObject wrapper;
    public Text text;
    public Button closeButton;
    public float secondsBeforeCloseButtonIsShown = 1;
    
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

        if (instance.currentLinesIndex >= instance.currentDialogueLines.Length) {
            Debug.Log("No more dialogue lines.");
            return;
        }

        DialogueLine currentLine = instance.currentDialogueLines[instance.currentLinesIndex];

        if (currentLine.showNextLineAfterThis) {
            LoadingBar.DestroySelf();
            BackgroundSoundPlayer.Stop();
        }

        instance.text.text = currentLine.text;
        instance.timeLastLineShowed = Time.time;

        instance.wrapper.SetActive(true);
        instance.closeButton.gameObject.SetActive(false);
        instance.StartCoroutine(instance.ShowButton());
    }
    
    public IEnumerator ShowButton() {
        yield return new WaitForSeconds(secondsBeforeCloseButtonIsShown);

        closeButton.gameObject.SetActive(true);
    }

    public void CloseDialogueNonStaticWrapper() {
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