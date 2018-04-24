using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAnimController : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private float delayStartAnim;
    private float lastTimeAnim = 0f;
    private float timeBetweenAnim = 0f;

    private void Start() {
        this.timeBetweenAnim = Time.time + this.delayStartAnim;
    }

    private void FixedUpdate() {
        if (Time.time - this.lastTimeAnim >= this.timeBetweenAnim) {
            this.animator.SetTrigger("wave");
            this.lastTimeAnim = Time.time;
            this.timeBetweenAnim = Random.Range(3f, 6f);
        }
    }

}
