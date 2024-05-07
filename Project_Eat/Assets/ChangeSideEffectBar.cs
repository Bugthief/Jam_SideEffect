using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSideEffectBar : MonoBehaviour
{
    public Sprite smallBackground;
    public Sprite midBackground;
    public Sprite largeBackground;

    public void ChangeBackground()
    {
        int i = CountImmediateChildren();

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        Sprite newSprite;
        float newWidth;
        float newHeight;

        if (i < 9)
        {
            newSprite = smallBackground;
        }
        else if (i < 17) 
        { 
            newSprite = midBackground; 
        }
        else
        {
            newSprite = largeBackground;
        }

        newWidth = newSprite.rect.width;
        newHeight = newSprite.rect.height;

        gameObject.GetComponent<Image>().sprite = newSprite;
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }

    public int CountImmediateChildren()
    {
        int childCount = 0;

        foreach (Transform child in gameObject.transform)
        {
            if (child.parent == gameObject.transform)
            {
                childCount++;
            }
        }

        return childCount;
    }
}
