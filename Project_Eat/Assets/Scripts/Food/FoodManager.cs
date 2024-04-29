using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Food;

public class FoodManager : MonoBehaviour
{
    public void EatFood(string foodKey)
    {
        Food thisFood = GameManager.Instance.FoodDictionary[foodKey];
        GameManager.Instance.currentPoint += thisFood.FoodPoint * GameManager.Instance.pointA;
        GameManager.Instance.currentTime += thisFood.FoodTime * GameManager.Instance.speedA;

        foreach (SideEffectTypeEnum sideEffectTypeEnum in thisFood.SideEffectNameList)
        {
            BuffEffect(sideEffectTypeEnum);
        }
    }

    public void BuffEffect(SideEffectTypeEnum sideEffectTypeEnum)
    {
        SideEffect thisSideEffect = GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum];
        thisSideEffect.AddSideEffectCount();
    }
}
