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
    public string[] dialogueLines;

    private int currentLinesIndex;
    private float timeLastLineShowed;
    

    private static DialogueSystem instance;

    void Awake() {
        instance = this;
        currentLinesIndex = 0;
    }

    void Start () {
        wrapper.SetActive(false);
    }

    public static void OnKnock() {
        instance.StartCoroutine(instance.OnKnockCoroutine());
    }

    IEnumerator OnKnockCoroutine() {
        // wait for one frame because we dont want the button to be pressed right after enabling
        yield return null;

        if (currentLinesIndex >= dialogueLines.Length) {
            Debug.Log("No more dialogue lines.");
            yield break;
        }
        
        instance.text.text = dialogueLines[currentLinesIndex];
        currentLinesIndex++;
        timeLastLineShowed = Time.time;

        instance.wrapper.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Time.time - timeLastLineShowed < secondsBeforeDialogMayBeClosed) {
            return;
        }

        wrapper.SetActive(false);

        if (currentLinesIndex >= dialogueLines.Length) {
            Door.DisableKnocking();
            LevelEnder.EndGame(LevelEnd.IMPATIENT);
        }
    }
}
