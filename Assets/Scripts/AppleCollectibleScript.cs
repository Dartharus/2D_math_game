using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleCollectibleScript : MonoBehaviour
{
    private int level;

    void Start() //randomly spawn apple objects when not the tutorial level
    {
        level = PlayerPrefs.GetInt("GameLevel");
        if(level != 0)
        {
            spawn();
        }
    }

    void OnTriggerEnter2D(Collider2D other) //if main character collects the apple object, destroy it
    {
        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            controller.CollectApple();
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other) //function to respawn the object of it is colliding with another apple
    {
        if (other.gameObject.tag == "Apples")
        {
            spawn();
        }
    }
    void spawn() //function to calculate and place the object at a random location
    {
        float x = Random.Range(-11.5f, -5.5f);
        float y = Random.Range(5.5f, 2.5f);
        Vector3 pos = new Vector3(x, y, 10);
        transform.position = pos;
    }
}
