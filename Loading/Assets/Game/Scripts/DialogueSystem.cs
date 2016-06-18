using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour {

    public GameObject wrapper;
    public Text text;

    [Multiline]
    public string[] dialogueLines;

    private int currentLinesIndex;
    

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

        instance.wrapper.SetActive(true);
    }

    public void OnGuiClick() {
        if (currentLinesIndex >= dialogueLines.Length) {
            LevelEnder.EndGame(LevelEnd.IMPATIENT);
            return;
        }

        wrapper.SetActive(false);
    }
}
