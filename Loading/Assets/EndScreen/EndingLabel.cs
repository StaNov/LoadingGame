using UnityEngine;
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
        if (LevelEnder.levelEnd == levelEnd) {
            image.sprite = achievedSprite;
        }
    }

    void Update () {
        
    }
}
