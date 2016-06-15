using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Door : MonoBehaviour {

    public Text testText;

    void Start() {
        testText.gameObject.SetActive(false);
    }

    IEnumerator OnMouseDown() {
        // TODO
        testText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        testText.gameObject.SetActive(false);
    }
}
