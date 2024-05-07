using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;

public class SideEffectLoader : MonoBehaviour
{
    public static TextAsset csvFile;

    public static Dictionary<SideEffectTypeEnum, SideEffect> BuildSideEffectDictionary()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/SideEffects");

        Dictionary<SideEffectTypeEnum, SideEffect> sideEffectDictionary = new Dictionary<SideEffectTypeEnum, SideEffect>();

        string csvText = csvFile.text;
        string[] lines = csvText.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrEmpty(line))
                continue;

            string[] fields = line.Split('|');

            SideEffect sideEffect = new SideEffect();

            string sideEffectKeyString = fields[0];
            if (Enum.TryParse(sideEffectKeyString, out SideEffectTypeEnum sideEffectKey))
            {
                sideEffect.SetSideEffectKey(sideEffectKey);
            }

            string sideEffectName = fields[1];
            sideEffect.SetSideEffectName(sideEffectName);

            string[] sideEffectDetailsList = fields[2].Split(';');
            foreach (string detail in sideEffectDetailsList)
            {
                sideEffect.AddSideEffectDetial(detail);
            }

            sideEffectDictionary.Add(sideEffectKey, sideEffect);
        }

        return sideEffectDictionary;
    }
}
