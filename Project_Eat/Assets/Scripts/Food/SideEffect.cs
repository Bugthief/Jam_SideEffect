using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Food;

public class SideEffect
{
    public SideEffectTypeEnum SideEffectKey { get; private set; }
    public string SideEffectName { get; private set; }
    public List<string> SideEffectDetailsList { get; private set; }
    public int SideEffectCount { get; private set; }
    public GameObject IconGameObject { get; private set; }

    public SideEffect()
    {
        SideEffectDetailsList = new List<string>();
        SideEffectCount = 0;
    }

    public void SetSideEffectKey(SideEffectTypeEnum sideEffectKey)
    {
        SideEffectKey = sideEffectKey;
    }

    public void SetSideEffectName(string sideEffectName)
    {
        SideEffectName = sideEffectName;
    }

    public void AddSideEffectDetial(string sideEffectDetail)
    {
        SideEffectDetailsList.Add(sideEffectDetail);
    }

    public void AddSideEffectCount()
    {
        SideEffectCount++;
    }

    public void MinusSideEffectCount()
    {
        if (SideEffectCount == 0) return;

        SideEffectCount--;
    }

    public void ClearSideEffectCound()
    {
        SideEffectCount = 0;
    }

    public void SetIconGameObject(GameObject iconGameObject)
    {
        IconGameObject = iconGameObject;
    }

}
