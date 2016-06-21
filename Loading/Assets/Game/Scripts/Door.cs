using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Door : MonoBehaviour {

    public AudioClip[] knocks;

    private AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        audioSource.PlayOneShot(knocks[Random.Range(0, knocks.Length)]);
        DialogueSystem.OnKnock();
    }
}
