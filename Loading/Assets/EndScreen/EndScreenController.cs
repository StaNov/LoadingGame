using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenController : MonoBehaviour {

    public Text endScreenText;

    void Awake () {
        endScreenText.text = "Game Over: " + LevelEnder.levelEnd;
    }

    void Start () {
        
    }

    void Update () {
        
    }
}
