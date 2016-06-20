using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class FirstPlaythroughPanel : MonoBehaviour, IPointerClickHandler {

    public GameObject panel;

    void Start () {
        panel.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData) {

        if (Time.timeSinceLevelLoad < 1) {
            return;
        }

        panel.SetActive(false);
    }
}
