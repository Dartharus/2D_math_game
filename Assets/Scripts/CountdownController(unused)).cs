using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "Go collect apples";

        yield return new WaitForSeconds(1f);

        countdownDisplay.gameObject.SetActive(false);
    }
}
