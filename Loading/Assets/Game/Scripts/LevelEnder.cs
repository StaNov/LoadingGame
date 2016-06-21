using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class LevelEnder : MonoBehaviour {

    public AudioClip toiletFlushing;
    public AudioClip footSteps;
    public AudioClip doorSqueak;

    private static LevelEnder instance;
    public static LevelEnd levelEnd {get; private set;}

    private AudioSource audioSource;

    void Awake () {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            levelEnd = LevelEnd.TEST;
            audioSource = GetComponent<AudioSource>();
        } else {
            Destroy(gameObject);
        }
    }

    public static void EndGame(LevelEnd levelEnd) {
        LevelEnder.levelEnd = levelEnd;

        instance.StartCoroutine(instance.EndGame());
    }

    IEnumerator EndGame() {
        audioSource.PlayOneShot(toiletFlushing);
        yield return new WaitForSeconds(toiletFlushing.length);

        audioSource.PlayOneShot(doorSqueak);
        yield return new WaitForSeconds(doorSqueak.length);

        audioSource.PlayOneShot(footSteps);
        yield return new WaitForSeconds(footSteps.length);

        yield return new WaitForSeconds(1); // just one more second before loading end-scene

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

public enum LevelEnd {
    TEST, PATIENT, IMPATIENT
}