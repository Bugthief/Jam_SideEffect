using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Food;

public class SideEffectWarning : MonoBehaviour
{
    public float fadeDuration = 10f;
    public float hoverFadeSpeed = 2f;

    private float currentFadeTime = 0f;

    public Image image;
    public TMP_Text tmpText;

    private void Update()
    {
        if (HoverEffect.IsPointerOverUI(gameObject))
        {
            currentFadeTime = 0f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            tmpText.alpha = 1f;
        }
        else
        {
            
            if (currentFadeTime < fadeDuration)
            {
                currentFadeTime += Time.deltaTime;
                float t = currentFadeTime / fadeDuration;
                float alpha = Mathf.Lerp(1f, 0f, t);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
                tmpText.alpha = alpha;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void UpdateSideEffectWarningText(SideEffectTypeEnum thisSideEffectKey)
    {
        tmpText.text = "新副作用：" + GameManager.Instance.SideEffectDictionary[thisSideEffectKey].SideEffectName;
    }

}
