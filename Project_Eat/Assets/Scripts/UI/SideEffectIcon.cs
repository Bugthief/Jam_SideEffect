using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Food;

public class SideEffectIcon : MonoBehaviour
{
    public TMP_Text sideEffectIntro;
    public SideEffectTypeEnum thisSideEffectKey;

    public GameObject sideEffectTextBoxObject;
    public Transform sideEffectTextBoxParent;

    bool IsMoved = false;

    public void UpdateSideEffectIntroText()
    {
        SideEffect thisSideEffect = GameManager.Instance.SideEffectDictionary[thisSideEffectKey];

        string sideEffectDetail = string.Join(", ", thisSideEffect.SideEffectDetailsList);

        sideEffectIntro.text =
            "Ãû³Æ£º " + thisSideEffect.SideEffectName + "\n" +
            "½éÉÜ£º " + sideEffectDetail + "\n" +
            "²ãÊý£º " + thisSideEffect.SideEffectCount;
    }

    public void MoveTextBox()
    {
        sideEffectTextBoxObject.transform.SetParent(sideEffectTextBoxParent);
    }

    private void Update()
    {
        if (!IsMoved && HoverEffect.IsPointerOverUI(gameObject)) { MoveTextBox(); IsMoved = true; }
    }

}
