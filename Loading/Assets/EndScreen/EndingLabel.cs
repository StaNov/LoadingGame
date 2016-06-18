﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingLabel : MonoBehaviour {

    public LevelEnd levelEnd;
    public Sprite achievedSprite;

    private Image image;

    void Awake () {
        image = GetComponent<Image>();
    }

    void Start () {
        #if !UNITY_EDITOR
        if (levelEnd == LevelEnd.TEST) {
            Destroy(gameObject);
        }
        #endif

        if (LevelEnder.levelEnd == levelEnd) {
            image.sprite = achievedSprite;
        }
    }
}
