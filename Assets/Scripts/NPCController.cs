using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public GameController controller;

    void OnCollisionEnter2D(Collision2D collision) //function that detects if player collide with NPC
    {
        if (collision.gameObject.tag == "Player")
        {
            controller.DisplayTurnInDialogue();
        }
    }
}
