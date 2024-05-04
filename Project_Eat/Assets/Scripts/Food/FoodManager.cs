using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Food;

public class FoodManager : MonoBehaviour
{
    public SideEffectManager SideEffectManager;

    public void EatFood(List<string> foodKeyList)
    {
        float timeThisRound = 0f;
        float pointThisRound = 0f;
        List<SideEffectTypeEnum> effectThisRound = new List<SideEffectTypeEnum>();

        foreach(string foodKey in foodKeyList)
        {
            Food thisFood = GameManager.Instance.FoodDictionary[foodKey];
            pointThisRound += thisFood.FoodPoint;
            timeThisRound += thisFood.FoodTime;

            foreach(SideEffectTypeEnum sideEffectTypeEnum in thisFood.SideEffectNameList)
            {
                effectThisRound.Add(sideEffectTypeEnum);
            }
        }

        GameManager.Instance.currentPoint += pointThisRound * GameManager.Instance.pointA;
        GameManager.Instance.currentTime += timeThisRound / GameManager.Instance.speedA;

        foreach (SideEffectTypeEnum sideEffectTypeEnum in effectThisRound)
        {
            SideEffectManager.BuffEffect(sideEffectTypeEnum);
        }
    }
}
