using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staranimation : MonoBehaviour
{
    public GameObject starAnimation;
    private AudioSource starSFX;
    private Vector3 scaleChange;
    private bool SFXplayed;
    public float delay;

    void Start()
    {
        starSFX = GetComponent<AudioSource>();
    }
    void Update()
    {
        //delay to stop all the stars from appearing at once
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
        else if(delay <= 0)
        {
            if (starAnimation.activeInHierarchy == true) //if star object is active
            {
                if (starAnimation.transform.localScale.x < 1.0 && starAnimation.transform.localScale.y < 1.0) //if star has not reached the max size
                {
                    scaleChange = new Vector3(Time.deltaTime*3, Time.deltaTime*3, Time.deltaTime*3);
                    starAnimation.transform.localScale += scaleChange;
                }
            }
            if (SFXplayed == false)
            {
                starSFX.Play();
                SFXplayed = true;
            }
        }
    }
}
