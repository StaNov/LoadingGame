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
    [Multiline]
    public string[] dialogueLinesShake;

    private int currentLinesIndexKnocking;
    private int currentLinesIndexHeart;
    private int currentLinesIndexShake;

    private float timeLastLineShowed;
    private bool isDialogueShown;
    

    private static DialogueSystem instance;

    void Awake() {
        instance = this;
        currentLinesIndexKnocking = 0;
        currentLinesIndexHeart = 0;
        currentLinesIndexShake = 0;
    }

    void Start () {
        wrapper.SetActive(false);
        isDialogueShown = false;
    }

    public static void OnKnock() {
        instance.isDialogueShown = true;
        instance.StartCoroutine(instance.OnKnockCoroutine());
    }

    public static void OnHeartClicked() {
        instance.isDialogueShown = true;
        instance.StartCoroutine(instance.OnHeartCoroutine());
    }

    public static void OnShake() {
        if (! instance.isDialogueShown) {
            instance.isDialogueShown = true;
            instance.StartCoroutine(instance.OnShakeCoroutine());
        }
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
        currentLinesIndexShake = 0;
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
        currentLinesIndexShake = 0;
        timeLastLineShowed = Time.time;

        instance.wrapper.SetActive(true);
    }

    IEnumerator OnShakeCoroutine() {
        // wait for one frame because we dont want the button to be pressed right after enabling
        yield return null;

        if (currentLinesIndexShake >= dialogueLinesShake.Length) {
            Debug.Log("No more dialogue lines.");
            yield break;
        }

        instance.text.text = dialogueLinesShake[currentLinesIndexShake];
        currentLinesIndexShake++;
        currentLinesIndexHeart = 0;
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
        instance.isDialogueShown = false;

        if (instance.currentLinesIndexKnocking >= instance.dialogueLinesKnocking.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(LevelEnd.IMPATIENT);
        } else if (instance.currentLinesIndexHeart >= instance.dialogueLinesHeart.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(LevelEnd.HEART);
        } else if (instance.currentLinesIndexShake >= instance.dialogueLinesShake.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(LevelEnd.SHAKE);
        }

    }

    public static bool DialogueKnockingStarted() {
        return instance.currentLinesIndexKnocking > 0;
    }
}
