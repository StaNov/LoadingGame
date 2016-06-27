﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Door : MonoBehaviour {

    public AudioClip[] knocks;
    public GameObject closedDoor;
    public GameObject openedDoor;

    private static Door instance;

    private AudioSource audioSource;
    private Animator animator;
    private bool knockingEnabled = false;

    void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        closedDoor.SetActive(true);
        openedDoor.SetActive(false);
    }

    IEnumerator Start() {
        // wait before knocking is enabled
        yield return new WaitForSeconds(0.5f);

        knockingEnabled = true;
    }

    void OnMouseDown() {
        if (!knockingEnabled) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        audioSource.PlayOneShot(knocks[Random.Range(0, knocks.Length)]);
        animator.SetTrigger("knock");
        DialogueSystem.OnKnock();
    }

    public static void DisableKnocking() {
        instance.knockingEnabled = false;
    }

    public static void OpenDoor() {
        LoadingBar.DestroySelf();
        instance.closedDoor.SetActive(false);
        instance.openedDoor.SetActive(true);
    }
}
