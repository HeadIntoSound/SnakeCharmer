using UnityEngine;
public static class MathLib
{
    public static Vector3 RandomV3(Vector3 minVal, Vector3 maxVal)
    {
        return new Vector3(Random.Range(minVal.x, maxVal.x), Random.Range(minVal.y, maxVal.y), 0);
    }

    public static Vector3 RandomRestrictedV3(float minVal, float maxVal)
    {
        return new Vector3(Random.Range(minVal, maxVal) * Operand(), Random.Range(minVal, maxVal) * Operand(), 0);
    }
    static int Operand()
    {
        return Random.Range(0, 2) == 0 ? -1 : 1;
    }

    public static int ExpLog10(float a, float b, int level)
    {
        return (int)Mathf.Round(a * Mathf.Log10(level) + b);
    }

    public static int ExpLinear(float a, float b, int level)
    {
        return (int)Mathf.Round(a * level + b);
    }

    public static int ExpExponential(float a, float b, int level)
    {
        return (int)Mathf.Round(a * Mathf.Pow(b, level));
    }

    public static int ExpLogPoly(float a, float b, int level)
    {
        return (int)Mathf.Round(a * Mathf.Log(level) + b * Mathf.Pow(level, 2)) + 1;
    }
}
