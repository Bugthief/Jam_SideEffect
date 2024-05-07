using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManage : MonoBehaviour
{
    public bool inSideEffect = false;

    public bool elder = false;
    public bool anger = false;
    public bool coffeePlus = false;
    public bool toxic = false;
    public bool numb = false;
    public bool shining = false;


    private void Update()
    {

        if (elder)
        {
            inSideEffect = true;
            Elder();
            StartCoroutine(KeepElderForSeconds(3f));
            elder = false; // 重置 bool 变量
        }

        if (anger)
        {
            inSideEffect = true;
            Anger();
        }

        if (coffeePlus)
        {
            inSideEffect = true;
            CoffeePlus();
        }

        // 中毒
        if (toxic)
        {
            inSideEffect = true;
            Toxic();
        }

        if (numb)
        {
            inSideEffect = true;
            Numb();
            StartCoroutine(KeepNumbForSeconds(3f));
            numb = false;
        }

        if (shining)
        {
            inSideEffect = true;
            Shining();
            StartCoroutine(KeepShiningForSeconds(3f));
            shining = false; // 重置 bool 变量
        }



        if (!inSideEffect)
            Reset();
    }

    public void Elder()
    {
        gameObject.GetComponent<ScanlinesEffect>().strength = 0.6f;
        gameObject.GetComponent<ScanlinesEffect>().noise = 1.5f;
    }

    IEnumerator KeepElderForSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(DecreaseElderOverTime(3f, 0f, 0.6f));
    }

    IEnumerator DecreaseElderOverTime(float duration, float targetThickDistort, float targetFineDistort)
    {
        var scanlinesEffect = gameObject.GetComponent<ScanlinesEffect>();
        float elapsedTime = 0f;
        float startThickDistort = scanlinesEffect.strength;
        float startFineDistort = scanlinesEffect.noise;

        // 等待 3 秒
        yield return new WaitForSeconds(3f);

        // 缓慢减少参数的值
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            scanlinesEffect.strength = Mathf.Lerp(startThickDistort, targetThickDistort, elapsedTime / duration);
            scanlinesEffect.noise = Mathf.Lerp(startFineDistort, targetFineDistort, elapsedTime / duration);
            yield return null;
        }

        scanlinesEffect.strength = targetThickDistort;
        scanlinesEffect.noise = targetFineDistort;
    }

    public void Anger()
    {

    }

    public void CoffeePlus()
    {

    }

    public void Toxic()
    {
        
        gameObject.GetComponent<RGBShiftEffect>().amount = 0.003f;
        gameObject.GetComponent<RGBShiftEffect>().speed = 3f;

        gameObject.GetComponent<BloomEffect>().strength = 0.6f;
        gameObject.GetComponent<BloomEffect>().size = 5f;
        gameObject.GetComponent<BloomEffect>().cutOff = 0.25f;
    }

    public void Numb()
    {
        gameObject.GetComponent<BadTVEffect>().thickDistort = 3f;
        gameObject.GetComponent<BadTVEffect>().fineDistort = 6f;
    }

    IEnumerator KeepNumbForSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(DecreaseDistortOverTime(3f, 0.9f, 0.1f));
    }

    IEnumerator DecreaseDistortOverTime(float duration, float targetThickDistort, float targetFineDistort)
    {
        var badTVEffect = gameObject.GetComponent<BadTVEffect>();
        float elapsedTime = 0f;
        float startThickDistort = badTVEffect.thickDistort;
        float startFineDistort = badTVEffect.fineDistort;

        // 等待 3 秒
        yield return new WaitForSeconds(3f);

        // 缓慢减少参数的值
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            badTVEffect.thickDistort = Mathf.Lerp(startThickDistort, targetThickDistort, elapsedTime / duration);
            badTVEffect.fineDistort = Mathf.Lerp(startFineDistort, targetFineDistort, elapsedTime / duration);
            yield return null;
        }

        badTVEffect.thickDistort = targetThickDistort;
        badTVEffect.fineDistort = targetFineDistort;
    }

    public void Shining()
    {
        gameObject.GetComponent<BloomEffect>().strength = 2f;
        gameObject.GetComponent<BloomEffect>().size = 6f;
        gameObject.GetComponent<BloomEffect>().cutOff = 0.1f;
    }

    IEnumerator KeepShiningForSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
        StartCoroutine(DecreaseStrengthOverTime(3f));
    }

    IEnumerator DecreaseStrengthOverTime(float duration)
    {
        var bloomEffect = gameObject.GetComponent<BloomEffect>();
        float elapsedTime = 0f;
        float startStrength = bloomEffect.strength;
        float targetStrength = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            bloomEffect.strength = Mathf.Lerp(startStrength, targetStrength, elapsedTime / duration);
            yield return null;
        }

        bloomEffect.strength = targetStrength;
    }


    void Reset()
    {
        gameObject.GetComponent<RGBShiftEffect>().amount = 0f;
        gameObject.GetComponent<RGBShiftEffect>().speed = 0f;

        gameObject.GetComponent<BloomEffect>().strength = 0.0f;
        gameObject.GetComponent<BloomEffect>().size = 0f;
        gameObject.GetComponent<BloomEffect>().cutOff = 0f;
    }
}
