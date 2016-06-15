﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueSystem : MonoBehaviour {

    public GameObject wrapper;
    public Text text;

    private static DialogueSystem instance;

    void Awake() {
        instance = this;
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
        instance.wrapper.SetActive(true);
    }

    public void OnGuiClick() {
        wrapper.SetActive(false);
    }
}
