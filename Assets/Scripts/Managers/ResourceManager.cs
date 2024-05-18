using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [System.Serializable]
    class CharmImage
    {
        public Charm charm;
        public Sprite img;
    }
    [SerializeField] Sprite defaultImage;
    [SerializeField] CharmImage[] charmImages;

    public Sprite GetCharmImage(Charm toSearch)
    {
        foreach (CharmImage charmImage in charmImages)
        {
            if (charmImage.charm == toSearch)
                return charmImage.img;
        }
        return defaultImage;
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

}
