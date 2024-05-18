public enum Charm
{
    True_Love_Orb,
    Apollo_Teardrop,
    Amplifier_Amulet,
    Smooth_Criminal_Ring,
    Venomstar_Locket
}

public enum RarityTier
{
    Common,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public class CharmInfo
{
    public Charm option;
    public RarityTier rarity;
}
namespace Utilities
{
    public static class Info
    {
        public static string GetCharmName(Charm charm)
        {
            switch (charm)
            {
                case Charm.True_Love_Orb: return "True Love Ring";
                case Charm.Apollo_Teardrop: return "Apollo's Ring";
                case Charm.Amplifier_Amulet: return "Amplifier Ring";
                case Charm.Smooth_Criminal_Ring: return "Smooth Criminal Ring";
                case Charm.Venomstar_Locket: return "VenomStar Ring";
            }
            return "Empty Ring";
        }

        public static string GetCharmDescription(Charm charm)
        {
            switch (charm)
            {
                case Charm.True_Love_Orb: return "Instantly level up";
                case Charm.Apollo_Teardrop: return "Increase movement speed";
                case Charm.Amplifier_Amulet: return "Increase charm range ";
                case Charm.Smooth_Criminal_Ring: return "Decrease charm time";
                case Charm.Venomstar_Locket: return "Increase snake following capacity";
            }
            return "Does nothing.";
        }

        public static int GetRarityModifier(RarityTier tier)
        {
            switch (tier)
            {
                case RarityTier.Legendary: return Constants.LEGENDARY;
                case RarityTier.Epic: return Constants.EPIC;
                case RarityTier.Rare: return Constants.RARE;
                case RarityTier.Common: return Constants.COMMON;
            }
            return 1;
        }

        public static UnityEngine.Color GetRarityColor(RarityTier tier)
        {
            switch (tier)
            {
                case RarityTier.Legendary: return new UnityEngine.Color(0.9960785f, 0.7960785f, 0.3137255f, 1); // Gold 
                case RarityTier.Epic: return new UnityEngine.Color(0.8941177f, 0.4196079f, 0.3568628f, 1);      // Red
                case RarityTier.Rare: return new UnityEngine.Color(0.4392157f, 0.3686275f, 0.9215687f, 1);      // Blue
                case RarityTier.Common: return new UnityEngine.Color(0.5960785f, 0.5803922f, 0.4784314f, 1);    // Gray
            }
            return UnityEngine.Color.white;
        }

        public static float[] CharmsProbabilities()
        {
            return new float[5]{
                Constants.LVLCHANCE,
                Constants.SPDCHANCE,
                Constants.RANGECHANCE,
                Constants.CHARMTIMECHANCE,
                Constants.FOLLOWCHANCE
            };
        }
    }
}