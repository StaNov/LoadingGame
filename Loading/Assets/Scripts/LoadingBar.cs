﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour {

    public float maximumWidth = 400;
    public float secondsToLoad = 20;
    public RectTransform innerBarRectTransform;
    public Text percentsText;

    private int loadedPercents;

    IEnumerator Start() {
        loadedPercents = 0;

        while (loadedPercents < 100) {
            loadedPercents++;

            innerBarRectTransform.sizeDelta = new Vector2(
                maximumWidth * (loadedPercents/100f), 
                innerBarRectTransform.sizeDelta.y
            );
            percentsText.text = loadedPercents + " %";

            yield return new WaitForSeconds(secondsToLoad / 100);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}