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
    }

    void OnDoorClicked() {
        if (!AcceptingInputStatus.isAcceptingInput) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        audioSource.PlayOneShot(knocks[Random.Range(0, knocks.Length)]);
        animator.SetTrigger("knock");
        DialogueSystem.Trigger(LevelEnd.IMPATIENT);
    }

    void OnHeartClicked() {
        if (!AcceptingInputStatus.isAcceptingInput) {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        audioSource.PlayOneShot(coneFly);
        // animator.SetTrigger("knock"); // TODO
        DialogueSystem.Trigger(LevelEnd.HEART);
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
