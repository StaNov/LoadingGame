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
    public Material dwarfToiletPapered;

    private static LevelEnder instance;
    public static LevelEnd levelEnd {get; private set;}

    private AudioSource audioSource;
    private bool alreadyEnding;

    void Awake () {
        if (SceneManager.GetActiveScene().buildIndex == 0 || instance == null) {
            if (instance != null) {
                Destroy(instance.gameObject);
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            levelEnd = LevelEnd.TEST;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        alreadyEnding = false;

        if (dwarf != null) {
            dwarf.SetActive(false);
        }
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

        if (levelEnd != LevelEnd.HEART) {
            audioSource.PlayOneShot(toiletFlushing);
            yield return new WaitForSeconds(toiletFlushing.length);
        }

        MainCamera.ZoomOut();
        if (levelEnd == LevelEnd.SHAKE) {
            Door.OpenDoorCensored();
            dwarf.GetComponentInChildren<Renderer>().material = dwarfToiletPapered;
        } else {
            Door.OpenDoor();
        }
        
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
    TEST, PATIENT, IMPATIENT, INTERRUPTED, VIOLENT, FIRE, HEART, SHAKE
}