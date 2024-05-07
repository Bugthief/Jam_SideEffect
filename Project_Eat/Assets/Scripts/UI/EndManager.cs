using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public GameObject endPanel;
    public GameObject fail1;
    public GameObject fail2;
    public GameObject fail3;
    public GameObject win1;

    public void FailByTime(float currentTime, float fullTime)
    {
        endPanel.SetActive(true);
        float result = currentTime / fullTime;

        if(0 < result  && result <= 1/3) 
        {
            fail1.SetActive(true);
        }else if(1/3 < result && result <= 2/3)
        {
            fail2.SetActive(true);
        }else if(2/3 < result &&result <= 3 / 3)
        {
            fail3.SetActive(true);
        }
    }

    public void WinTheGame()
    {
        endPanel.SetActive(true);
        win1.SetActive(true);
    }

    private void Start()
    {
        endPanel.SetActive(false);
        fail1.SetActive(false);
        fail2.SetActive(false);
        fail3.SetActive(false);
        win1.SetActive(false);
    }
}
