using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingLabel : MonoBehaviour {

    public LevelEnd levelEnd;

    private Image image;

    void Awake () {
        image = GetComponent<Image>();
    }

    void Start () {
        if (LevelEnder.levelEnd == levelEnd) {
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
    }

    void Update () {
        
    }
}
