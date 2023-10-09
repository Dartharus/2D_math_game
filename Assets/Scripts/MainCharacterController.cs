using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    private AudioSource pickup;
    public AudioSource walk;
    public GameController controller;
    public float speed = 3.0f;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    public float displayTime = 3.0f;
    public GameObject dialogBox;
    float timerDisplay;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
        pickup = GetComponent<AudioSource>();
    }
    //Update is called once per frame
    void Update()
    {
        if (horizontal != 0 || vertical != 0) //if there is no movement pause walking SFX
        {
            walk.UnPause();
        }
        else
        {
            walk.Pause();
        }
        if (controller.gamePlaying)
        {
            GetPlayerInput();
            if (timerDisplay >= 0) //timer logic for dialogue box
            {
                timerDisplay -= Time.deltaTime;
                if (timerDisplay < 0)
                {
                    dialogBox.SetActive(false);
                }
            }
        }
        else
        {
            horizontal = 0.0f;
            vertical = 0.0f;
        }
        
    }
    //FixedUpdate can be called multiple times per frame. Used for physics calculation
    void FixedUpdate()
    {
        //movement for main character
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    private void GetPlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        //use approximation instead of absolute in case there is a very small number
        //if x or y not 0 then main character is moving
        //normalize the vector2 to use with animator to determine correct direction animation
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }

    public void CollectApple()  //function to control collection of apples
    {
        controller.IncreaseApple(1);    //increase the amount of items held
        pickup.Play();  //play audio
        if (controller.apples > controller.minApples)   //display a dialogue if held amount exceeds objective requirement
        {
            DisplayDialog();
        }
    }

    public void CollectFlour() //function to control collection of flour
    {
        controller.IncreaseFlour(1);
        pickup.Play();
        if (controller.flour > controller.minFlour)
        {
            DisplayDialog();
        }
    }

    public void DisplayDialog() //function to display the dialogue
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
    }
}
