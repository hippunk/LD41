using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimController : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private float delayStartAnim;
    private float timeBetweenAnim = 0f;

    private void Start() {
        this.timeBetweenAnim = Time.time + this.delayStartAnim;
    }

    private void FixedUpdate() {
        if (this.timeBetweenAnim - Time.time <= 0) {
            this.animator.SetTrigger("start");
        }
    }

}
