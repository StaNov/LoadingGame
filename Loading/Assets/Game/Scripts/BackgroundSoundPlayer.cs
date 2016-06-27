using UnityEngine;
using System.Collections;

public class BackgroundSoundPlayer : MonoBehaviour {

    private static BackgroundSoundPlayer instance;

    private AudioSource audioSource;

    void Awake () {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public static void Stop () {
        instance.audioSource.Stop();
    }
}
