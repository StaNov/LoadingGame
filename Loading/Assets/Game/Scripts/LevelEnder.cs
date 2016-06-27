using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LevelEnder : MonoBehaviour {

    public AudioClip toiletFlushing;
    public AudioClip footSteps;
    public AudioClip doorSqueak;
    public AudioClip fart;
    public GameObject dwarf;

    private static LevelEnder instance;
    public static LevelEnd levelEnd {get; private set;}

    private AudioSource audioSource;
    private bool alreadyEnding;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        } else {
            instance.alreadyEnding = false;
            Destroy(gameObject);
        }
    }

    void Start() {
        levelEnd = LevelEnd.TEST;
        alreadyEnding = false;
        dwarf.SetActive(false);
    }

    public static void EndGame(LevelEnd levelEnd) {
        if (instance.alreadyEnding) {
            return;
        }

        instance.alreadyEnding = true;
        LevelEnder.levelEnd = levelEnd;

        instance.StartCoroutine(instance.EndGameCoroutine(levelEnd));
    }

    IEnumerator EndGameCoroutine(LevelEnd levelEnd) {
        DialogueSystem.CloseDialogue();
        Door.DisableKnocking();
        BackgroundSoundPlayer.Stop();

        if (levelEnd == LevelEnd.IMPATIENT) {
            audioSource.PlayOneShot(fart);
            LoadingBar.FillRestOfLoadingBar(fart.length);
            yield return new WaitForSeconds(fart.length);
        }

        audioSource.PlayOneShot(toiletFlushing);
        yield return new WaitForSeconds(toiletFlushing.length);

        MainCamera.ZoomOut();
        Door.OpenDoor();
        audioSource.PlayOneShot(doorSqueak);
        yield return new WaitForSeconds(doorSqueak.length);

        dwarf.SetActive(true);
        dwarf.GetComponentInChildren<Animator>().speed = 1 / footSteps.length;
        audioSource.PlayOneShot(footSteps);
        yield return new WaitForSeconds(footSteps.length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

public enum LevelEnd {
    TEST, PATIENT, IMPATIENT, INTERRUPTED, VIOLENT, FIRE
}