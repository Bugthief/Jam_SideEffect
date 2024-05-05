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
        pointText.text = "������ " + point.ToString();
    }

    public void UpdateTimeText(float time)
    {
        timeText.text = "ʱ�䣺 " + time.ToString();
    }

    private void Start()
    {
        UpdatePointText(GameManager.Instance.currentPoint);
    }
}
