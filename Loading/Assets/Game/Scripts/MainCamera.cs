using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    private static MainCamera instance;

    private Animator animator;

    void Awake () {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public static void ZoomOut () {
        instance.animator.SetTrigger("ZoomOut");
    }
}
