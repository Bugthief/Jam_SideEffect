using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public bool isCoutingDown = false;
    public float currentTime;
    public float maxTime;
    public float timeLeft;// 剩余时间

    public GameObject timeProgressBar;// 剩余时间的进度条

    public GeneralInfo generalInfo;

    public void StartCountDown(float startTime, float endTime)
    {
        isCoutingDown = true;
        currentTime = startTime;
        maxTime = endTime;

        timeProgressBar.GetComponent<Slider>().maxValue = maxTime;// 最大时间值同步给时间进度条
    }

    public void Update()
    {
        if (isCoutingDown && GameManager.Instance != null)
        {
            currentTime += Time.deltaTime;
            GameManager.Instance.currentTime = currentTime;
            timeLeft = maxTime - currentTime;
            generalInfo.UpdateTimeText(timeLeft);

            TimeBar_GetTime();

            if (currentTime >= maxTime)
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
        GameManager.Instance.TimeOutFailure();
    }

    void TimeBar_GetTime()
    {
        timeProgressBar.GetComponent<Slider>().value = timeLeft;
    }
}
