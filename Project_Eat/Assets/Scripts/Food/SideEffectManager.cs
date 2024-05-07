using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Food;

public class SideEffectManager : MonoBehaviour
{
    public float timeA;
    public float timeB;
    //public float pointA;
    public float pointB;

    public float PicaFoodB;
    public float NonPicaFoodb;

    public bool isVeganBuffed = false;
    public bool isOnePlusOne = false;
    public bool isCarnivore = false;

    public TimeManager timeManager;
    public GameObject cam;

    public GameObject effectIconPrefab;
    public Transform effectIconParentTransform;
    public Transform effectTextBoxParentTransform;

    public GameObject effectWarningPrefab;
    public Transform effectWarningParentTransform;

    public ChangeSideEffectBar changeSideEffectBar;

    public Transform foodOntheBelfParent;
    public FoodManager foodManager;


    private void Start()
    {
        timeA = GameManager.Instance.timeA;
        timeB = GameManager.Instance.timeB;

        //pointA = GameManager.Instance.pointA;
        pointB = GameManager.Instance.pointB;

        PicaFoodB = 1f;
        NonPicaFoodb = 1f;

    }

    public (float, float) CalculateUnderEffect(Food food)
    {
        float time = food.FoodTime;
        float point = food.FoodPoint;

        point = CheckVegan(food, point);
        point = CheckPica(food, point);
        point = CheckOnePlusOne(food, point);
        CheckCarnivore(food);

        time = time * timeA + timeB;
        point += pointB;

        Debug.Log(food.FoodName + " ������ " + point + "ʱ�䣺 " + time);

        return (time, point);
    }

    public void BuffEffect(SideEffectTypeEnum sideEffectTypeEnum)
    {
        SideEffect thisSideEffect = GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum];
        thisSideEffect.AddSideEffectCount();

        if (GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum].IconGameObject == null)
        {
            GenerateEffectIcon(sideEffectTypeEnum);
        }
        else
        {
            UpdateEffectIcon(sideEffectTypeEnum);
        }

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

            case SideEffectTypeEnum.OnePlusOne:
                EffectOnePlusOne(thisSideEffect);
                return;
        }
    }

    public void GenerateEffectIcon(SideEffectTypeEnum sideEffectTypeEnum)
    {
        GameObject newSideEffectIconObject = Instantiate(effectIconPrefab, effectIconParentTransform);

        changeSideEffectBar.ChangeBackground();

        GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum].SetIconGameObject(newSideEffectIconObject);

        newSideEffectIconObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Arts/Side Effects Icon/" + sideEffectTypeEnum.ToString());

        newSideEffectIconObject.GetComponent<SideEffectIcon>().thisSideEffectKey = sideEffectTypeEnum;
        newSideEffectIconObject.GetComponent<SideEffectIcon>().UpdateSideEffectIntroText();
        newSideEffectIconObject.GetComponent<SideEffectIcon>().sideEffectTextBoxParent = effectTextBoxParentTransform;

        GameObject newSideEffectWarningObject = Instantiate(effectWarningPrefab, effectWarningParentTransform);

        newSideEffectWarningObject.GetComponent<SideEffectWarning>().UpdateSideEffectWarningText(sideEffectTypeEnum);

    }

    public void UpdateEffectIcon(SideEffectTypeEnum sideEffectTypeEnum)
    {
        GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum].IconGameObject.GetComponent<SideEffectIcon>().UpdateSideEffectIntroText();
    }

    public void EffectAnger(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                timeA *= 0.85f;
                return;
            case 2:
                //timeA *= 0.9f;
                return;
            case 3:
                if (GameManager.Instance.SideEffectDictionary[SideEffectTypeEnum.Shining] != null && GameManager.Instance.SideEffectDictionary[SideEffectTypeEnum.Shining].SideEffectCount > 0)
                {
                    
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
        timeA *= 1.15f;
    }

    public void EffectBurning(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case < 3:
                timeA *= 1.05f;
                return;

            case 3:
                timeA *= 1.1f;
                return;

            case > 3:
                //stop eating for 10 seconds.
                PassTimeToFoodManager(10f);
                return;

        }
    }

    public void EffectCoffeePlus(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                timeA *= 0.9f;
                return;
            case 2:
                timeA *= 0.9f;
                return;
            case 3:
                timeA /= 0.9f * 0.9f;
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
        cam.GetComponent<ShaderManage>().elder = true;
        BlurTheFood(10f);
    }

    public void EffectExcited(SideEffect sideEffect)
    {
        PassTimeToFoodManager(10f);
    }

    public void EffectImSick(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                timeA *= 1.15f;
                return;

            case 2:
                timeA *= 1.3f;
                PassTimeToFoodManager(10f);
                return;
        }
    }

    public void EffectInLove(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case < 4:

                PassTimeToFoodManager(10f);
                return;
            case 4:

                return;
        }
    }

    public void EffectMood(SideEffect sideEffect)
    {

    }

    public void EffectNumb(SideEffect sideEffect)
    {
        timeA *= 1.1f;
        cam.GetComponent<ShaderManage>().numb = true;
        timeManager.PauseCountDown(10f);
    }

    public void EffectPica(SideEffect sideEffect)
    {
        PicaFoodB = sideEffect.SideEffectCount;
        NonPicaFoodb = sideEffect.SideEffectCount * 0.5f;
    }

    public void EffectPungent(SideEffect sideEffect)
    {
        switch (sideEffect.SideEffectCount)
        {
            case 1:
                timeA *= 1.05f;
                return;
            case 2:
                timeA *= 1.1f;
                return;
            case > 2:
                BuffEffect(SideEffectTypeEnum.ThrowUp);
                return;
        }

    }

    public void EffectShining(SideEffect sideEffect)
    {
        cam.GetComponent<ShaderManage>().shining = true;
        DestoryTheFood();
    }

    public void EffectThrowUp(SideEffect sideEffect)
    {
        GameManager.Instance.currentPoint *= 0.8f;
        PassTimeToFoodManager(10f);
    }

    public void EffectToxic(SideEffect sideEffect)
    {

        cam.GetComponent<ShaderManage>().toxic = true;

        switch (sideEffect.SideEffectCount)
        {
            case 1:

                return;
            case 2:
                timeA *= 1.15f;
                pointB += 1f;
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

    public void EffectOnePlusOne(SideEffect sideEffect)
    {
        isOnePlusOne = true;
    }

    public void PassTimeToFoodManager(float time)
    {
        foodManager.timeByEffect += time;
    }

    public void BlurTheFood(float time)
    {

    }

    public void DestoryTheFood()
    {
        foreach (Transform child in foodOntheBelfParent)
        {
            Destroy(child.gameObject);
        }
    }

    public float CheckVegan(Food food, float point)
    {
        if (!isVeganBuffed) return point;

        if (!food.FoodTypeList.Contains(FoodTypeEnum.Vegetables))
        {
            point = 0f;
        }

        isVeganBuffed = false;

        return point;
    }

    public float CheckPica(Food food, float point)
    {
        if (food.FoodTypeList.Contains(FoodTypeEnum.Strange))
        {
            point += PicaFoodB;
        }
        else
        {
            point += NonPicaFoodb;
        }

        return point;
    }

    public float CheckOnePlusOne(Food food, float point)
    {
        if (!isOnePlusOne) return point;

        if (food.FoodTypeList.Contains(FoodTypeEnum.Drink))
        {
            point += food.FoodPoint;
            isOnePlusOne = false;
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