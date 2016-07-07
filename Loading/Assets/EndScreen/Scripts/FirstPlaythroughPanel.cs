using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class FirstPlaythroughPanel : MonoBehaviour, IPointerClickHandler {

    public LevelEndLabel[] texts;
    public GameObject endingLabelsParent;
    public GameObject panel;
    public Text textNotUnlockedAll;
    public Text textUnlockedAll;

    private float timeSinceLevelLoadOrLastTap;

    void Start () {
        if (EndingManager.levelEndAlreadyUnlocked) {
            panel.SetActive(false);
            return;
        }

        panel.SetActive(true);
        textNotUnlockedAll.gameObject.SetActive(true);
        textUnlockedAll.gameObject.SetActive(false);

        ReplaceLevelEndingText();
        timeSinceLevelLoadOrLastTap = Time.time;
    }

    private void ReplaceLevelEndingText() {
        foreach (LevelEndLabel text in texts) {
            if (text.levelEnd == LevelEnder.levelEnd) {
                textNotUnlockedAll.text = textNotUnlockedAll.text.Replace("%ENDING%", text.label);
                break;
            }
        }

        Debug.LogError("Text for ending '" + LevelEnder.levelEnd.ToString() + "' not found!");
    }

    public void OnPointerClick(PointerEventData eventData) {

        if (Time.time - timeSinceLevelLoadOrLastTap < 1) {
            return;
        }

        if (textNotUnlockedAll.gameObject.activeSelf && !AtLeastOneMoreEndingNotUnlocked()) {
            textNotUnlockedAll.gameObject.SetActive(false);
            textUnlockedAll.gameObject.SetActive(true);
            timeSinceLevelLoadOrLastTap = Time.time;
        } else {
            panel.SetActive(false);
        }
    }

    private bool AtLeastOneMoreEndingNotUnlocked() {
        foreach (LevelEnd levelEnd in GetImplementedEndings()) {
            if (! PlayerPrefs.HasKey(levelEnd.ToString())) {
                return true;
            }
        }

        return false;
    }

    private LevelEnd[] GetImplementedEndings() {
        LevelEnd[] result =  new LevelEnd[endingLabelsParent.transform.childCount];
        int i = 0;

        foreach (Transform child in endingLabelsParent.transform) {
            result[i] = child.GetComponent<EndingLabel>().levelEnd;
            i++;
        }

        return result;
    }

    [System.Serializable]
    public class LevelEndLabel {
        public LevelEnd levelEnd;
        public string label;
    }
}
