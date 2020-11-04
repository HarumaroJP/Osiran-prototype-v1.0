using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {
    [SerializeField] private bool enable;
    [SerializeField] private Sprite enableSprite;
    private SpriteRenderer renderer;


    private void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            //TODO: フラグに当たったらlerpさせて停止させる
            // other.GetComponent<IController>().Idle();
            Take();
        }
    }


    public void Take() {
        renderer.sprite = enableSprite;
        enable = true;
    }
}
