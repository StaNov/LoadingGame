using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Door : MonoBehaviour {

    public AudioClip[] knocks;
    public AudioClip coneFly;
    public GameObject closedDoor;
    public GameObject openedDoor;
    public GameObject openedDoorCensored;
    public CompoundCollider doorCollider;
    public CompoundCollider heartCollider;

    private static Door instance;

    private AudioSource audioSource;
    private Animator animator;
    private bool knockingEnabled = false;

    void Awake() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        doorCollider.onClickAction = () => {
            OnDoorClicked();
        };

        heartCollider.onClickAction = () => {
            OnHeartClicked();
        };

        closedDoor.SetActive(true);
        openedDoor.SetActive(false);
        openedDoorCensored.SetActive(false);
    }

    IEnumerator Start() {
        // wait before knocking is enabled
        yield return new WaitForSeconds(0.5f);

        knockingEnabled = true;
    }

    void OnDoorClicked() {
        if (!knockingEnabled) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        audioSource.PlayOneShot(knocks[Random.Range(0, knocks.Length)]);
        animator.SetTrigger("knock");
        DialogueSystem.OnKnock();
    }

    void OnHeartClicked() {
        if (!knockingEnabled) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        audioSource.PlayOneShot(coneFly);
        // animator.SetTrigger("knock"); // TODO
        DialogueSystem.OnHeartClicked();
    }

    public static void DisableKnocking() {
        instance.knockingEnabled = false;
    }

    public static void OpenDoor() {
        LoadingBar.DestroySelf();
        instance.closedDoor.SetActive(false);
        instance.openedDoor.SetActive(true);
    }

    public static void OpenDoorCensored() {
        LoadingBar.DestroySelf();
        instance.closedDoor.SetActive(false);
        instance.openedDoorCensored.SetActive(true);
    }
}
