using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private int level;
    private String scene;
    private AudioSource drop;

    //public variables for easy drag and drop from the inspector
    public GameObject appleContainer, hudContainer, objectiveDialogue, turnInDialogue, applePrefab, flourPrefab, mainCharacter, scoreScreenContainer;
    public GameObject Star1Empty, Star2Empty, Star3Empty, Star1Full, Star2Full, Star3Full, Star1anim, Star2anim, Star3anim;
    public GameObject AppleUI1, AppleUI2, AppleUI3, AppleUI4, AppleUI5, AppleUI6, AppleUI7, AppleUI8, AppleUI9;
    public GameObject FlourUI1, FlourUI2, FlourUI3, FlourUI4, FlourUI5;
    public GameObject DropAppleButton, DropFlourButton, PauseButton, ResumeButton, HintButton, HomeButton;
    public Text scorePoints, timer, numOfApples, numOfFlour, turnInText, objectiveText, Star1Text, Star2Text, Star3Text, MCDialogueText;
    public bool gamePlaying { get; private set; }

    public int apples = 0;
    public int minApples;

    public int flour = 0;
    public int minFlour;

    public int countdownTime;
    public float timeValue;
    private Vector3 MainCharacterPos;

    private bool scoreCondition1, scoreCondition2, scoreCondition3;
    public int star1Score, star2Score, star3Score;
    private int Score;

    private int tutorialStep;

    private bool isObjectiveDialogueOn;


    private void Awake()
    {
        instance = this;
    }

    private void Start()    //initialize different parameters when game starts
    {
        drop = GetComponent<AudioSource>();
        level = PlayerPrefs.GetInt("GameLevel");
        gamePlaying = false;
        objectiveDialogue.SetActive(true);
        isObjectiveDialogueOn = true;
        Star1Text.text = formatScore(star1Score);
        Star2Text.text = formatScore(star2Score);
        Star3Text.text = formatScore(star3Score); ;
        if (level == 1 || level == 0)
        {
            numOfFlour.text = "";
        }
        if (level == 0)
        {
            tutorialStep = 1;
            objectiveText.text = "Help me collect an apple";
        }
    }

    private void Update()
    {
        //logic for tutorial level. Toggling gameobjects and changing dialogue text depending on tutorial step
        if (level == 0)
        {
            timer.text = "";
            if (tutorialStep == 2)
            {
                objectiveText.text = "Use WASD keys to move around \r\nMove over an apple to pick it up";
            }
            if (apples == 1 && tutorialStep == 3)
            {
                tutorialStep++;
            }
            if (apples == 1 && tutorialStep == 4)
            {
                gamePlaying = false;
                objectiveText.text = "Now pick up another apple";
                hudContainer.SetActive(false);
                objectiveDialogue.SetActive(true);
                isObjectiveDialogueOn = true;
            }
            if (apples == 2 && tutorialStep == 5)
            {
                gamePlaying = false;
                objectiveText.text = "You have picked up too many apples.\r\nPress the drop apple button on the right side of the \r\nscreen to drop an apple.";
                hudContainer.SetActive(false);
                objectiveDialogue.SetActive(true);
                isObjectiveDialogueOn = true;
            }
            if (apples == 1 && tutorialStep == 6)
            {
                gamePlaying = false;
                objectiveText.text = "Now that you have the correct number of apples,\r\ngive them to me.";
                hudContainer.SetActive(false);
                objectiveDialogue.SetActive(true);
                isObjectiveDialogueOn = true;
            }
        }
        switch (level) //switch case to initialize the different texts for each level
        {
            case 1:
                objectiveText.text = "Help me collect " + minApples + " apples \r\nand give them to me";
                if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 2:
                objectiveText.text = "Help me collect " + minApples + " apples to make 1 \r\napple salad and give them to me";
                if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 3:
                objectiveText.text = "Help me collect apples to make \r\n2 apple salads and give them to me.\r\nCollect " + minApples + " apples please.";
                if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 4:
                objectiveText.text = "Help me collect apples to make 3 apple salads \r\nRemember, I need 3 apples to make \r\n1 apple salad";
                if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 5:
                objectiveText.text = "Help me collect apples to make 1 apple \r\nsalad. Also, collect 2 extra apples";
                if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 6:
                objectiveText.text = "Help me collect " + minApples + " apples and " + minFlour + " flour \r\nand give them to me";
                if (flour > minFlour)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n much flour";
                }
                else if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 7:
                objectiveText.text = "Help me collect " + minApples + " apples and " + minFlour + " flour \r\nto make an apple pie and give them to me";
                if (flour > minFlour)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n much flour";
                }
                else if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 8:
                objectiveText.text = "Help me collect ingredients to make 2 apple pies and give them to me \r\nCollect " + minApples + " apples and " + minFlour + " flour please.";
                if (flour > minFlour)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n much flour";
                }
                else if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 9:
                objectiveText.text = "Help me collect ingredients to make 3 apple pies and give them to me\r\nRemember, I need 2 Apples and 1 Flour to make an apple pie";
                if (flour > minFlour)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n much flour";
                }
                else if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;
            case 10:
                objectiveText.text = "Help me collect ingredients to make an apple pie.\r\nAlso, collect 2 extra apples";
                if (flour > minFlour)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n much flour";
                }
                else if (apples > minApples)
                {
                    MCDialogueText.text = "I might have \r\npicked up too \r\n many apples";
                }
                break;

        }

        if (gamePlaying)
        {
            timeValue -= Time.deltaTime;
            Score = (int)timeValue;
            MainCharacterPos = mainCharacter.transform.position;
            DisplaySidePanelImages();
            if (level != 0) //display timer when not in tutorial
            {
                DisplayTimer(timeValue);
            }
        }
        //if else statements to handle pausing the game and using the hint button
        if (gamePlaying == false && isObjectiveDialogueOn == false && level != 0)
        {
            PauseButton.SetActive(false);
            HintButton.SetActive(false);
            ResumeButton.SetActive(true);
            HomeButton.SetActive(true);
        }
        else if (gamePlaying == true && isObjectiveDialogueOn == false && level != 0)
        {
            ResumeButton.SetActive(false);
            HintButton.SetActive(true);
            PauseButton.SetActive(true);
            HomeButton.SetActive(false);
        }
        else
        {
            ResumeButton.SetActive(false);
            HintButton.SetActive(false);
            PauseButton.SetActive(false);
            HomeButton.SetActive(false);
        }
        if (timeValue <= 0)
        {
            timeValue = 0;
        }
    }

    private void BeginGame()
    {
        gamePlaying = true;
    }

    private void DisplayTimer(float timeToDisplay)  //function to format and display the timer
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreaseApple(int amount)
    {
        apples += amount;
    }

    public void IncreaseFlour(int amount)
    {
        flour += amount;
    }

    private void DisplaySidePanelImages() //function to display picked up items on the side panel as well as buttons
    {
        if (apples >= 1)
        {
            AppleUI1.SetActive(true);
            DropAppleButton.SetActive(true);
        }
        else
        {
            AppleUI1.SetActive(false);
            DropAppleButton.SetActive(false);
        }
        if (apples >= 2)
            AppleUI2.SetActive(true);
        else
            AppleUI2.SetActive(false);
        if (apples >= 3)
            AppleUI3.SetActive(true);
        else
            AppleUI3.SetActive(false);
        if (apples >= 4)
            AppleUI4.SetActive(true);
        else
            AppleUI4.SetActive(false);
        if (apples >= 5)
            AppleUI5.SetActive(true);
        else
            AppleUI5.SetActive(false);
        if (apples >= 6)
            AppleUI6.SetActive(true);
        else
            AppleUI6.SetActive(false);
        if (apples >= 7)
            AppleUI7.SetActive(true);
        else
            AppleUI7.SetActive(false);
        if (apples >= 8)
            AppleUI8.SetActive(true);
        else
            AppleUI8.SetActive(false);
        if (apples >= 9)
            AppleUI9.SetActive(true);
        else
            AppleUI9.SetActive(false);

        if (level > 1)
        {
            if (flour >= 1)
            {
                FlourUI1.SetActive(true);
                DropFlourButton.SetActive(true);
            }
            else
            {
                FlourUI1.SetActive(false);
                DropFlourButton.SetActive(false);
            }
            if (flour >= 2)
                FlourUI2.SetActive(true);
            else
                FlourUI2.SetActive(false);
            if (flour >= 3)
                FlourUI3.SetActive(true);
            else
                FlourUI3.SetActive(false);
            if (flour >= 4)
                FlourUI4.SetActive(true);
            else
                FlourUI4.SetActive(false);
            if (flour >= 5)
                FlourUI5.SetActive(true);
            else
                FlourUI5.SetActive(false);
        }
    }

    public void DisplayTurnInDialogue() //function that displays the dialogue when interacting with NPC
    {
        turnInDialogue.SetActive(true);
        hudContainer.SetActive(false);
        switch (level)  //initialze dialogue texts depending on level
        {
            case 1:
                if (apples < minApples)
                {
                    turnInText.text = "You have not collected enough apples\r\nI need " + minApples + " apples.";
                }
                else if (apples > minApples)
                {
                    turnInText.text = "You have collected too many apples\r\nI need " + minApples + " apples.";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required apples";
                    checkScoreCondition();
                }
                break;
            case 2:
                if (apples < minApples)
                {
                    turnInText.text = "You have not collected enough apples\r\nI need " + minApples + " apples.";
                }
                else if (apples > minApples)
                {
                    turnInText.text = "You have collected too many apples\r\nI need " + minApples + " apples.";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required apples";
                    checkScoreCondition();
                }
                break;
            case 3:
                if (apples < minApples)
                {
                    turnInText.text = "You have not collected enough apples\r\nI need " + minApples + " apples.";
                }
                else if (apples > minApples)
                {
                    turnInText.text = "You have collected too many apples\r\nI need " + minApples + " apples.";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required apples";
                    checkScoreCondition();
                }
                break;
            case 4:
                if (apples < minApples)
                {
                    turnInText.text = "You have not collected enough apples";
                }
                else if (apples > minApples)
                {
                    turnInText.text = "You have collected too many apples";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required apples";
                    checkScoreCondition();
                }
                break;
            case 5:
                if (apples < minApples)
                {
                    turnInText.text = "You have not collected enough apples";
                }
                else if (apples > minApples)
                {
                    turnInText.text = "You have collected too many apples";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required apples";
                    checkScoreCondition();
                }
                break;
            case 6:
                if (apples < minApples || flour < minFlour)
                {
                    turnInText.text = "You have not collected enough ingredients\r\nI need " + minApples + " apples and " + minFlour + " flour.";
                }
                else if (apples > minApples || flour > minFlour)
                {
                    turnInText.text = "You have collected too many ingredients\r\nI need " + minApples + " apples and " + minFlour + " flour.";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required ingredients";
                    checkScoreCondition();
                }
                break;
            case 7:
                if (apples < minApples || flour < minFlour)
                {
                    turnInText.text = "You have not collected enough ingredients\r\nI need " + minApples + " apples and " + minFlour + " flour.";
                }
                else if (apples > minApples || flour > minFlour)
                {
                    turnInText.text = "You have collected too many ingredients\r\nI need " + minApples + " apples and " + minFlour + " flour.";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required ingredients";
                    checkScoreCondition();
                }
                break;
            case 8:
                if (apples < minApples || flour < minFlour)
                {
                    turnInText.text = "You have not collected enough ingredients\r\nI need " + minApples + " apples and " + minFlour + " flour.";
                }
                else if (apples > minApples || flour > minFlour)
                {
                    turnInText.text = "You have collected too many ingredients\r\nI need " + minApples + " apples and " + minFlour + " flour.";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required ingredients";
                    checkScoreCondition();
                }
                break;
            case 9:
                if (apples < minApples || flour < minFlour)
                {
                    turnInText.text = "You have not collected enough ingredients";
                }
                else if (apples > minApples || flour > minFlour)
                {
                    turnInText.text = "You have collected too many ingredients";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required ingredients";
                    checkScoreCondition();
                }
                break;
            case 10:
                if (apples < minApples || flour < minFlour)
                {
                    turnInText.text = "You have not collected enough ingredients";
                }
                else if (apples > minApples || flour > minFlour)
                {
                    turnInText.text = "You have collected too many ingredients";
                }
                else
                {
                    turnInText.text = "Thank you for collecting the required ingredients";
                    checkScoreCondition();
                }
                break;
        }
        if (level == 0)
        {
            turnInText.text = "Thank you for collecting the apple";
        }

        gamePlaying = false;
    }

    private void checkScoreCondition()
    {
        if (Score >= star1Score)
        {
            scoreCondition1 = true;
        }
        if (Score >= star2Score)
        {
            scoreCondition2 = true;
        }
        if (Score >= star3Score)
        {
            scoreCondition3 = true;
        }
    }
    public void onButtonContinue()  //function for the continue button to start the level
    {
        BeginGame();
        hudContainer.SetActive(true);
        objectiveDialogue.SetActive(false);
        isObjectiveDialogueOn = false;
    }

    public void onButtonContinueToScoreScreen() //function for the continue button when interacting with the NPC
    {
        if (level > 0 && level <= 5)
        {
            if (apples != minApples)
            {
                turnInDialogue.SetActive(false);
                hudContainer.SetActive(true);
                gamePlaying = true;
            }
            else
            {
                turnInDialogue.SetActive(false);
                scoreScreenContainer.SetActive(true);
                scorePoints.text = formatScore(Score);
            }
        }
        if (level >= 6 && level <= 10)
        {
            if (apples != minApples || flour != minFlour)
            {
                turnInDialogue.SetActive(false);
                hudContainer.SetActive(true);
                gamePlaying = true;
            }
            else
            {
                turnInDialogue.SetActive(false);
                scoreScreenContainer.SetActive(true);
                scorePoints.text = formatScore(Score);
            }
        }

        //if else statements that handle which type of star to display
        if (scoreCondition1 == true)
        {
            Star1anim.SetActive(true);
            Star1Empty.SetActive(false);
        }
        else if (scoreCondition1 == false)
        {
            Star1Full.SetActive(false);
            Star1Empty.SetActive(true);
        }
        if (scoreCondition2 == true)
        {
            Star2anim.SetActive(true);
            Star2Empty.SetActive(false);
        }
        else if (scoreCondition2 == false)
        {
            Star2Full.SetActive(false);
            Star2Empty.SetActive(true);
        }
        if (scoreCondition3 == true)
        {
            Star3anim.SetActive(true);
            Star3Empty.SetActive(false);
        }
        else if (scoreCondition3 == false)
        {
            Star3Full.SetActive(false);
            Star3Empty.SetActive(true);
        }
    }

    public void onButtonRetry() //function to reload the level
    {
        if (level == 1)
        {
            SceneManager.LoadScene("TestScene");
        }
        else
        {
            scene = "Level" + level;
            SceneManager.LoadScene(scene);
        }
    }

    public void onButtonNextLevel() //function to go to the next level
    {
        level++;
        PlayerPrefs.SetInt("GameLevel", level);
        if (level >= 2 && level <= 5)
        {
            scene = "Level" + level;
            SceneManager.LoadScene(scene);
        }
        //after completing level 5 go back to difficulty select screen. Select medium difficulty starts level 6
        else if (level == 6) 
        {
            SceneManager.LoadScene("DifficultySelect");
        }
        else if (level >= 7 && level <= 10)
        {
            scene = "Level" + level;
            SceneManager.LoadScene(scene);
        }
        else
        {
            SceneManager.LoadScene("EndScreen");
        }

    }

    public void onButtonDropApple()
    {
        dropApple();
    }
    public void onButtonDropFlour()
    {
        dropFlour();
    }

    public void onButtonContinueTutorial() //function for continue button inside tutorial
    {
        if (tutorialStep == 2)
        {
            BeginGame();
            hudContainer.SetActive(true);
            objectiveDialogue.SetActive(false);
            isObjectiveDialogueOn = false;
        }
        if (tutorialStep == 4 || tutorialStep == 5 || tutorialStep == 6)
        {
            gamePlaying = true;
            hudContainer.SetActive(true);
            objectiveDialogue.SetActive(false);
            isObjectiveDialogueOn = false;
        }
        tutorialStep++;
    }

    public void onButtonPause()
    {
        gamePlaying = false;
    }

    public void onButtonResume()
    {
        gamePlaying = true;
    }

    public void onButtonHint()
    {
        gamePlaying = false;
        objectiveDialogue.SetActive(true);
        isObjectiveDialogueOn = true;
    }

    public void onButtonBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void dropApple()
    {
        //outside of tutorial dropping apple respawns a new apple
        if (level != 0)
        {
            if (apples > 0)
            {
                apples--;
                float x = Random.Range(-11.5f, -5.5f);
                float y = Random.Range(5.5f, 2.5f);
                Instantiate(applePrefab, new Vector3(x, y, MainCharacterPos.z), Quaternion.identity);
                drop.Play();
            }
        }
        //inside tutorial just drop apple below character
        else if (level == 0 && tutorialStep != 5)
        {
            if (apples > 0)
            {
                apples--;
                Instantiate(applePrefab, new Vector3(MainCharacterPos.x, MainCharacterPos.y - 2, MainCharacterPos.z), Quaternion.identity);
                drop.Play();
            }
        }
    }

    public void dropFlour()
    {
        if (flour > 0)
        {
            flour--;
            float x = Random.Range(5.5f, 9.0f);
            float y = Random.Range(2.5f, 4.0f);
            Instantiate(flourPrefab, new Vector3(x, y, MainCharacterPos.z), Quaternion.identity);
            drop.Play();
        }
    }

    private String formatScore(int score) //function to format the string below each star at the end screen with the score
    {
        String s;
        if (score < 10)
        {
            s = "00:0" + score;
        }
        else
        {
            s = "00:" + score;
        }
        return s;
    }
}