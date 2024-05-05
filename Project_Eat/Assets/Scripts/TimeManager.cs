using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool isCoutingDown = false;
    public float currentTime;
    public float maxTime;

    public void StartCountDown(float startTime, float endTime)
    {
        isCoutingDown = true;
        currentTime = startTime;
        maxTime = endTime;
    }

    public void Update()
    {
        if (isCoutingDown)
        {
            currentTime += Time.deltaTime;
            GameManager.Instance.currentTime = currentTime;
            if(currentTime >= maxTime)
            {
                isCoutingDown = false;
                TimeOutFailure();
            }
        }
    }

    public void PauseCountDown(float duration)
    {
        isCoutingDown = false;
        StartCoroutine(ResumeCountDownAfter(duration));
    }

    IEnumerator ResumeCountDownAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        isCoutingDown = true;
    }


    private void TimeOutFailure()
    {

    }
}
