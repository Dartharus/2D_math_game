using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlourCollectible : MonoBehaviour
{
    void Start() //spawn flour object at start of level
    {
        spawn();
    }

    void OnTriggerEnter2D(Collider2D other) //if main character collects the apple object, destroy it
    {
        MainCharacterController controller = other.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            controller.CollectFlour();
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D other) //function to respawn the object of it is colliding with another flour
    {
        if (other.gameObject.tag == "Flour")
        {
            spawn();
        }
    }
    void spawn() //function to calculate and place the object at a random location
    {
        float x = Random.Range(5.5f, 9.0f);
        float y = Random.Range(2.5f, 4.0f);
        Vector3 pos = new Vector3(x, y, 10);
        transform.position = pos;
    }
}
