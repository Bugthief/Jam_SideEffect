using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StomachInfo : MonoBehaviour
{
    public Image img_stomachProgressBar;
    public VignetteEffect _vignetteEffect;

    [Header("以完全饱腹状态开始")]
    public bool init_fullStomach = true;
    [Header("以半饱状态开始")]
    public bool init_halfStomach = false;

    [Header("初始化")]
    public float initialValue = 100f;// 初始值
    public float maxValue = 100.0f;// 最大值
    [Header("shader满值时，胃的百分比（0-1）")]
    [Range(0f, 1f)]
    public float threshold = 0.2f;// 晕影到达满的阈值


    [Header("胃消耗速率（decayRate/s）")]
    public float decayRate = 1.0f; // 每秒减少的进度值

    [Header("【只读】当前值")]
    public float currentFillAmount;// 当前的填充值


    void Start()
    {
        if (init_fullStomach)
        {
            initialValue = maxValue;
            currentFillAmount = initialValue;// 初始化填充值，可以设置为0或其他初始值   
        }
        else if (init_halfStomach)
        {
            initialValue = maxValue / 2f;
            currentFillAmount = initialValue;// 初始化填充值，可以设置为0或其他初始值
        }

        UpdateProgressBar();
    }

    void Update()
    {
        // 随时间减少进度条的值
        AdjustProgressBar(-decayRate * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.I))
        {

            AdjustProgressBar(10.0f);// 假设按下空格键时增加10个单位的进度
        }

        if (Input.GetKeyDown(KeyCode.O))
        {

            AdjustProgressBar(-5.0f);// 假设按下左Shift键时减少5个单位的进度
        }


    }


    // 更新进度条的填充量
    void UpdateProgressBar()
    {
        img_stomachProgressBar.fillAmount = currentFillAmount;
        UpdateVignetteEffect();
    }

    // 加减值操作：加则传入正值，减则负值
    public void AdjustProgressBar(float amount)
    {
        currentFillAmount = Mathf.Clamp01(currentFillAmount + amount / maxValue);
        UpdateProgressBar();
    }


    // 启用shader
    public void ShaderEnable()
    {
        if (_vignetteEffect = null)
        {
            Debug.Log("Warning: Empty Shader!");
            return;
        }

        _vignetteEffect.on = true;
    }

    // 设置shader强度
    public void ShaderBuff(float mag)
    {

        mag = Mathf.Clamp(mag, 0.0f, 2.0f);// 将 mag 限制在 0 到 2 之间

        _vignetteEffect.amount = mag;// 晕影强度在0-2间
    }

    // 更新晕影效果
    void UpdateVignetteEffect()
    {
        if (_vignetteEffect == null)
        {
            return;
        }

        // 计算晕影强度
        float vignetteAmount = 0f;

        if (currentFillAmount <= threshold)
        {
            vignetteAmount = 2.0f;
        }
        else
        {
            vignetteAmount = Mathf.Lerp(2.0f, 0f, (currentFillAmount - threshold) / (1 - threshold));
        }

        _vignetteEffect.amount = vignetteAmount;
    }
}
