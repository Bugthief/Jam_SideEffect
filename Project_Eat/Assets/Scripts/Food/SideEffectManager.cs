using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Food;

public class SideEffectManager : MonoBehaviour
{
    public void BuffEffect(SideEffectTypeEnum sideEffectTypeEnum)
    {
        SideEffect thisSideEffect = GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum];
        thisSideEffect.AddSideEffectCount();

        switch (sideEffectTypeEnum)
        {
            case SideEffectTypeEnum.Anger:
                EffectAnger(thisSideEffect);
                return;

            case SideEffectTypeEnum.Bloating:
                EffectBloating(thisSideEffect);
                return;

            case SideEffectTypeEnum.Burning:
                return;

            case SideEffectTypeEnum.CoffeePlus:
                return;

            case SideEffectTypeEnum.Continuing6:
                return;

            case SideEffectTypeEnum.Elder:
                return;

            case SideEffectTypeEnum.Excited:
                return;

            case SideEffectTypeEnum.ImSick:
                return;

            case SideEffectTypeEnum.InLove:
                return;

            case SideEffectTypeEnum.Mood:
                return;

            case SideEffectTypeEnum.Numb:
                return;

            case SideEffectTypeEnum.Pica:
                return;

            case SideEffectTypeEnum.Pungent:
                return;

            case SideEffectTypeEnum.Shining:
                return;

            case SideEffectTypeEnum.ThrowUp:
                return;

            case SideEffectTypeEnum.Toxic:
                return;

            case SideEffectTypeEnum.Vegan:
                return;
        }
    }

    public void GenerateEffectIcon(SideEffectTypeEnum sideEffectTypeEnum)
    {
        //generate the icon from the prefab

        //assign the icon gameobject to the SideEffectClass
    }

    public void UpdateEffectIcon(SideEffectTypeEnum sideEffectTypeEnum)
    {
        //update the text
    }

    public void EffectAnger(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                GameManager.Instance.speedA *= 1.05f;
                return;
            case 2:
                GameManager.Instance.speedA *= 1.1f;
                return;
            case 3:
                if (GameManager.Instance.SideEffectDictionary[SideEffectTypeEnum.Shining] != null && GameManager.Instance.SideEffectDictionary[SideEffectTypeEnum.Shining].SideEffectCount > 0)
                {
                    //success
                }
                else
                {
                    //failure
                }
                return;
        }
    }

    public void EffectBloating(SideEffect sideEffect)
    {
        GameManager.Instance.pointA *= 0.95f;
    }

    public void EffectBurning(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                GameManager.Instance.speedA *= 0.99f;
                return;

            case 2:
                GameManager.Instance.speedA *= 0.98f;
                return;

            case 3:
                GameManager.Instance.speedA *= 0.97f;
                return;
            case > 3:
                //stop eating for 10 seconds.
                return;

        }
    }

    public void EffectCoffeePlus(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                GameManager.Instance.speedA *= 1.05f;
                return;
            case 2:
                GameManager.Instance.speedA *= 1.1f;
                return;
            case 3:
                GameManager.Instance.speedA /= 1.05f * 1.1f;
                sideEffect.ClearSideEffectCound();
                EffectExcited();
                return;
        }
    }

    public void EffectCountinue6(SideEffect sideEffect)
    {
        GameManager.Instance.currentPoint += 10f;
    }

    public void EffectElder(SideEffect sideEffect)
    {
        BlurTheFood(10f);
        FrozenCountDown(10f);
    }

    public void EffectExcited()
    {
        FrozenCountDown(10f);
    }

    public void EffectImSick(SideEffect sideEffect)
    {

    }

    public void EffectInLove(SideEffect sideEffect)
    {

    }

    public void EffectMood(SideEffect sideEffect)
    {

    }

    public void EffectNumb(SideEffect sideEffect)
    {

    }

    public void EffectPica(SideEffect sideEffect)
    {

    }

    public void EffectPungent(SideEffect sideEffect)
    {

    }

    public void EffectShining(SideEffect sideEffect)
    {

    }

    public void EffectThrowUp(SideEffect sideEffect)
    {

    }

    public void EffectToxic(SideEffect sideEffect)
    {

    }

    public void EffectVegan(SideEffect sideEffect)
    {

    }

    public void FrozenCountDown(float time)
    {

    }

    public void BlurTheFood(float time)
    {

    }

    public void DestoryTheFood()
    {

    }

}