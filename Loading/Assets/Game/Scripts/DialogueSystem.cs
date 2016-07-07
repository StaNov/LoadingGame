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
    
    [Multiline]
    public string[] dialogueLinesKnocking;
    [Multiline]
    public string[] dialogueLinesHeart;

    private int currentLinesIndexKnocking;
    private int currentLinesIndexHeart;

    private float timeLastLineShowed;
    

    private static DialogueSystem instance;

    void Awake() {
        instance = this;
        currentLinesIndexKnocking = 0;
        currentLinesIndexHeart = 0;
    }

    void Start () {
        wrapper.SetActive(false);
    }

    public static void OnKnock() {
        instance.StartCoroutine(instance.OnKnockCoroutine());
    }

    public static void OnHeartClicked() {
        instance.StartCoroutine(instance.OnHeartCoroutine());
    }

    IEnumerator OnKnockCoroutine() {
        // wait for one frame because we dont want the button to be pressed right after enabling
        yield return null;

        if (currentLinesIndexKnocking >= dialogueLinesKnocking.Length) {
            Debug.Log("No more dialogue lines.");
            yield break;
        }
        
        instance.text.text = dialogueLinesKnocking[currentLinesIndexKnocking];
        currentLinesIndexKnocking++;
        currentLinesIndexHeart = 0;
        timeLastLineShowed = Time.time;

        instance.wrapper.SetActive(true);
    }

    IEnumerator OnHeartCoroutine() {
        // wait for one frame because we dont want the button to be pressed right after enabling
        yield return null;

        if (currentLinesIndexHeart >= dialogueLinesHeart.Length) {
            Debug.Log("No more dialogue lines.");
            yield break;
        }

        instance.text.text = dialogueLinesHeart[currentLinesIndexHeart];
        currentLinesIndexHeart++;
        currentLinesIndexKnocking = 0;
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

        if (instance.currentLinesIndexKnocking >= instance.dialogueLinesKnocking.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(LevelEnd.IMPATIENT);
        } else if (instance.currentLinesIndexHeart >= instance.dialogueLinesHeart.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(LevelEnd.HEART);
        }

    }

    public static bool DialogueStarted() {
        return instance.currentLinesIndexKnocking > 0;
    }
}
