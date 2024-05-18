using System.Collections;
using UnityEngine;
using Utilities;
using System.Linq;
public class PowerUpController : MonoBehaviour
{
    CharmInfo[] items = new CharmInfo[2];
    [SerializeField] int timesToDisplay;
    bool displaying;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.OnLevelUp.AddListener(OfferCharm);
        items[0] = new CharmInfo();
        items[1] = new CharmInfo();
    }

    void OfferCharm(int lvl)
    {
        timesToDisplay++;
        if (!displaying)
            StartCoroutine(QueueDisplay());
    }

    void CharmSelection()
    {
        displaying = true;
        Time.timeScale = 0;
        // pick 2 charms to offer
        int op1 = Probability.ItemFromRarity(Info.CharmsProbabilities().ToList());
        int op2;
        do
        {
            op2 = Probability.ItemFromRarity(Info.CharmsProbabilities().ToList());
        } while (op1 == op2);

        items[0].option = (Charm)op1;
        items[0].rarity = Probability.ItemRarityGenerator();

        items[1].option = (Charm)op2;
        items[1].rarity = Probability.ItemRarityGenerator();

        // send it to the UI controller

        EventManager.Instance.OnCharmSelection.Invoke(items, (charm) =>
        {
            Time.timeScale = 1;
            timesToDisplay = timesToDisplay <= 0 ? 0 : timesToDisplay - 1;
            displaying = false;
        });
    }

    IEnumerator QueueDisplay()
    {
        while (timesToDisplay > 0)
        {
            CharmSelection();
            yield return new WaitUntil(() => displaying == false);
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
