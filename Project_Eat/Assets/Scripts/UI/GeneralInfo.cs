using UnityEngine;
using UnityEngine.UI;

public class GeneralInfo : MonoBehaviour
{
    public Image pointImage;
    public Image timeImage;

    float maxTime;
    float maxPoint;

    public void UpdatePointImage(float point)
    {
        float fillAmount = Mathf.Clamp01(point / maxPoint);
        pointImage.fillAmount = fillAmount;
    }

    public void UpdateTimeImage(float time)
    {
        float fillAmount = Mathf.Clamp01(time / maxTime);
        timeImage.fillAmount = fillAmount;
    }

    private void Start()
    {
        maxTime = GameManager.Instance.maxTime;
        maxPoint = GameManager.Instance.maxPoint;

        UpdatePointImage(GameManager.Instance.currentPoint);
        UpdateTimeImage(GameManager.Instance.currentTime);
    }
}
