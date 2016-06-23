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
        #if !UNITY_EDITOR
        if (levelEnd == LevelEnd.TEST) {
            Destroy(gameObject);
        }
        #endif
        
        Debug.Log(levelEnd.ToString() + ": " + PlayerPrefs.HasKey(levelEnd.ToString()));
        if (PlayerPrefs.HasKey(levelEnd.ToString())) {
            image.sprite = achievedSprite;
        }

        if (LevelEnder.levelEnd == levelEnd) {
            transform.SetParent(transform.parent.parent);
            GetComponent<Animator>().SetBool("Selected", true);
        }
    }
}
