using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private IController playerController;
    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<IController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.Death();
        }
    }
}
