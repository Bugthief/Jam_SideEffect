using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Food;

public class SideEffectIcon : MonoBehaviour
{
    public TMP_Text sideEffectIntro;
    public SideEffectTypeEnum thisSideEffectKey;

    public GameObject sideEffectTextBoxObject;
    public Transform sideEffectTextBoxParent;

    bool IsMoved = false;
    bool IsFliped = false;

    public void UpdateSideEffectIntroText()
    {
        SideEffect thisSideEffect = GameManager.Instance.SideEffectDictionary[thisSideEffectKey];

        string sideEffectDetail = string.Join(", ", thisSideEffect.SideEffectDetailsList);

        sideEffectIntro.text =
            "名称： " + thisSideEffect.SideEffectName + "\n" +
            "介绍： " + sideEffectDetail + "\n" +
            "层数： " + thisSideEffect.SideEffectCount;
    }

    public void MoveTextBox()
    {
        sideEffectTextBoxObject.transform.SetParent(sideEffectTextBoxParent);
    }

    private void Update()
    {
        if (!IsMoved && HoverEffect.IsPointerOverUI(gameObject)) { MoveTextBox(); IsMoved = true; }

        if(sideEffectTextBoxObject.activeInHierarchy && !IsImageFullyVisible() && !IsFliped)
        {
            IsFliped = true;

            RectTransform rectTransform = sideEffectTextBoxObject.GetComponent<RectTransform>();

            float rectWidth = rectTransform.sizeDelta.x;

            rectTransform.anchoredPosition -= new Vector2(rectWidth, 0f);
        }
    }

    public bool IsImageFullyVisible()
    {
        Image sideEffectTextBoxBackground = sideEffectTextBoxObject.GetComponent<Image>();
        RectTransform rectTransform = sideEffectTextBoxBackground.rectTransform;

        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Vector3 minViewport = Camera.main.WorldToViewportPoint(corners[0]);
        Vector3 maxViewport = Camera.main.WorldToViewportPoint(corners[2]);

        bool isVisible = minViewport.x >= 0 && maxViewport.x <= 1 &&
                         minViewport.y >= 0 && maxViewport.y <= 1;

        return isVisible;
    }
}
