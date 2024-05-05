using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Food;

public class SideEffectManager : MonoBehaviour
{
    public float speedA;
    public float pointA;

    public float PicaFoodA;
    public float NonPicaFoodA;

    public List<SideEffectTypeEnum> currentEffectList;

    public bool isVeganBuffed = false;
    public bool isCarnivore = false;

    public TimeManager timeManager;

    public GameObject effectIconPrefab;
    public Transform effectIconParentTransform;

    //public GameObject


    private void Start()
    {
        speedA = GameManager.Instance.speedA; 
        pointA = GameManager.Instance.pointA;

        PicaFoodA = 1f;
        NonPicaFoodA = 1f;
    }

    public (float, float) CalculateUnderEffect(Food food)
    {
        float time = food.FoodTime;
        float point = food.FoodPoint;

        CheckVegan(food, time, point);
        CheckPica(food, point);
        CheckCarnivore(food);

        time /= speedA;
        point *= pointA;

        return (time, point);
    }

    public void BuffEffect(SideEffectTypeEnum sideEffectTypeEnum)
    {
        if (!currentEffectList.Contains(sideEffectTypeEnum))
        {
            currentEffectList.Add(sideEffectTypeEnum);
            GenerateEffectIcon(sideEffectTypeEnum);
        }
        

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
                EffectBurning(thisSideEffect);
                return;

            case SideEffectTypeEnum.CoffeePlus:
                EffectCoffeePlus(thisSideEffect);
                return;

            case SideEffectTypeEnum.Continuing6:
                EffectCountinue6(thisSideEffect);
                return;

            case SideEffectTypeEnum.Elder:
                EffectElder(thisSideEffect);
                return;

            case SideEffectTypeEnum.Excited:
                EffectExcited(thisSideEffect);
                return;

            case SideEffectTypeEnum.ImSick:
                EffectImSick(thisSideEffect);
                return;

            case SideEffectTypeEnum.InLove:
                EffectInLove(thisSideEffect);
                return;

            case SideEffectTypeEnum.Mood:
                EffectInLove(thisSideEffect);
                return;

            case SideEffectTypeEnum.Numb:
                EffectNumb(thisSideEffect);
                return;

            case SideEffectTypeEnum.Pica:
                EffectPica(thisSideEffect);
                return;

            case SideEffectTypeEnum.Pungent:
                EffectPungent(thisSideEffect);
                return;

            case SideEffectTypeEnum.Shining:
                EffectShining(thisSideEffect);
                return;

            case SideEffectTypeEnum.ThrowUp:
                EffectThrowUp(thisSideEffect);
                return;

            case SideEffectTypeEnum.Toxic:
                EffectToxic(thisSideEffect);
                return;

            case SideEffectTypeEnum.Vegan:
                EffectVegan(thisSideEffect);
                return;

            case SideEffectTypeEnum.Carnivore:
                EffectCarnivore(thisSideEffect);
                return;
        }
    }

    public void GenerateEffectIcon(SideEffectTypeEnum sideEffectTypeEnum)
    {
        GameObject newSideEffectObject = Instantiate(effectIconPrefab, effectIconParentTransform);
        GameObject TextContainer = newSideEffectObject.transform.GetChild(0).GetChild(0).gameObject;
        TextContainer.GetComponent<TMP_Text>().text = sideEffectTypeEnum.ToString();


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
                speedA *= 1.05f;
                return;
            case 2:
                speedA *= 1.1f;
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
        pointA *= 0.98f;
    }

    public void EffectBurning(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                speedA *= 0.99f;
                return;

            case 2:
                speedA *= 0.98f;
                return;

            case 3:
                speedA *= 0.97f;
                return;
            case > 3:
                //stop eating for 10 seconds.
                speedA *= 0.97f;
                StartCoroutine(StopEating(10f));
                return;

        }
    }

    public void EffectCoffeePlus(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                speedA *= 1.05f;
                return;
            case 2:
                speedA *= 1.1f;
                return;
            case 3:
                GameManager.Instance.speedA /= 1.05f * 1.1f;
                sideEffect.ClearSideEffectCound();

                BuffEffect(SideEffectTypeEnum.Excited);
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
        sideEffect.ClearSideEffectCound();
    }

    public void EffectExcited(SideEffect sideEffect)
    {
        StartCoroutine(StopEating(10f));
        sideEffect.ClearSideEffectCound();
    }

    public void EffectImSick(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                speedA *= 0.95f;
                return;

            case 2:
                speedA *= 0.95f;
                pointA *= 0.95f;
                StartCoroutine(StopEating(10f));
                return;
        }
    }

    public void EffectInLove(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 4:

                //
                return;
        }
    }

    public void EffectMood(SideEffect sideEffect)
    {

    }

    public void EffectNumb(SideEffect sideEffect)
    {
        timeManager.PauseCountDown(10f);
    }

    public void EffectPica(SideEffect sideEffect)
    {
        PicaFoodA = Mathf.Pow(1.2f, sideEffect.SideEffectCount);
        NonPicaFoodA = Mathf.Pow(0.9f, sideEffect.SideEffectCount);
    }

    public void EffectPungent(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                speedA *= 0.98f;
                return;
            case 2:
                speedA *= 0.97f;
                return;
            case > 2:
                BuffEffect(SideEffectTypeEnum.ThrowUp);
                return;
        }

    }

    public void EffectShining(SideEffect sideEffect)
    {
        DestoryTheFood();
    }

    public void EffectThrowUp(SideEffect sideEffect)
    {
        pointA *= 0.98f;
        GameManager.Instance.currentPoint -= 10f;
    }

    public void EffectToxic(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:

                return;
            case 2:
                speedA *= 0.98f;
                pointA *= 1.1f;
                return;
            case > 2:
                BuffEffect(SideEffectTypeEnum.ThrowUp);
                return;
        }
    }

    public void EffectVegan(SideEffect sideEffect)
    {
        isVeganBuffed = true;
    }

    public void EffectCarnivore(SideEffect sideEffect)
    {
        isCarnivore = true;
    }

    public IEnumerator StopEating(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void BlurTheFood(float time)
    {

    }

    public void DestoryTheFood()
    {

    }

    public (float, float) CheckVegan(Food food, float time, float point)
    {
        if (!isVeganBuffed) return (time, point);

        if (!food.FoodTypeList.Contains(FoodTypeEnum.Vegetables))
        {
            point = 0f;
            time = 0f;
        }

        return (time, point);
    }

    public float CheckPica(Food food, float point)
    {
        if (food.FoodTypeList.Contains(FoodTypeEnum.Strange))
        {
            point *= PicaFoodA;
        }
        else
        {
            point *= NonPicaFoodA;
        }

        return point;
    }

    public void CheckCarnivore(Food food)
    {
        if (food.FoodTypeList.Contains(FoodTypeEnum.Meat))
        {

        }
    }
}