using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralInfo : MonoBehaviour
{
    public TMP_Text pointText;
    public TMP_Text timeText;

    public void UpdatePointText(float point)
    {
        pointText.text = "分数： " + point.ToString();
    }

    public void UpdateTimeText(float time)
    {
        timeText.text = "时间： " + time.ToString();
    }

    private void Start()
    {
        UpdatePointText(GameManager.Instance.currentPoint);
    }
}
