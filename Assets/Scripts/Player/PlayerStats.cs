[System.Serializable]
public class PlayerStats
{
    public int level;
    public int currentSnakes;
    public int snakesToNextLvl;
    public float[] lvlCoefficients = new float[2];
    public float moveSpeed;
    public float charmTime;
    public float charmRange;
    public int maxFollowers;
    public int totalSnakes;
}
