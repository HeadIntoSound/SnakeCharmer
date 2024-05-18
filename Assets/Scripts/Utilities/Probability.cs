using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Probability
{
    public static bool Simple(float chance)
    {
        return Random.Range(1, 101) <= chance;
    }

    public static RarityTier ItemRarityGenerator()
    {
        int rnd = Random.Range(0, 100);
        if (rnd == 99)
            return RarityTier.Legendary;
        if (rnd < 99 && rnd >= 85)
            return RarityTier.Epic;
        if (rnd < 85 && rnd >= 55)
            return RarityTier.Rare;
        return RarityTier.Common;
    }

    public static int ItemFromProbability(List<float> probabilityList)
    {
        var cumulativeProbability = MakeCumulativeProbability(probabilityList);

        if (MakeCumulativeProbability(probabilityList).Count < 1)
            return -1;

        float rnd = Random.Range(1, 101);

        for (int i = 0; i < probabilityList.Count; i++)
        {
            if (rnd <= cumulativeProbability[i])
            {
                return i;
            }
        }
        return -1;
    }

    static List<float> MakeCumulativeProbability(List<float> probabilityList)
    {
        float probabilitiesSum = 0;

        var cumulativeProbability = new List<float>();

        for (int i = 0; i < probabilityList.Count; i++)
        {
            probabilitiesSum += probabilityList[i];
            cumulativeProbability.Add(probabilitiesSum);

            if (probabilitiesSum > 100f)
            {
                Debug.LogError("Probabilities exceed 100%");
                return new List<float>();
            }
        }
        return cumulativeProbability;
    }

    public static int ItemFromRarity(List<float> probabilityRarity)
    {
        var list = CumulativeProbabilityByRarity(probabilityRarity);

        float rnd = Random.Range(1, 101);

        for (int i = 0; i < probabilityRarity.Count; i++)
        {
            if (rnd <= list[i])
            {
                return i;
            }
        }
        return -1;
    }


    static List<float> CumulativeProbabilityByRarity(List<float> probabilityRarity)
    {
        float probabilitiesSum = 0;

        var cumulativeByRarity = new List<float>();

        float ProbilityModifier = GetprobabilityByRarityModifer(probabilityRarity);

        for (int i = 0; i < probabilityRarity.Count; i++)
        {
            probabilitiesSum += probabilityRarity[i] * ProbilityModifier;
            cumulativeByRarity.Add(probabilitiesSum);
        }

        return cumulativeByRarity;
    }

    static float GetprobabilityByRarityModifer(List<float> probabilityRarity)
    {
        float itemRaritySum = 0;

        for (int i = 0; i < probabilityRarity.Count; i++)
            itemRaritySum += probabilityRarity[i];

        return 100 / itemRaritySum;
    }

    public static bool XinY(int x, int y = 1)
    {
        return Random.Range(1, x + 1) <= y;
    }
}