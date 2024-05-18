using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CharmUIController : MonoBehaviour
{
    [SerializeField] TMP_Text ampLabel;
    int ampCount;
    [SerializeField] TMP_Text apolloLabel;
    int apolloCount;
    [SerializeField] TMP_Text smoothLabel;
    int smoothCount;
    [SerializeField] TMP_Text venomLabel;
    int venomCount;
    [SerializeField] TMP_Text loveLabel;
    int loveCount;

    private void Start()
    {
        EventManager.Instance.OnApplyCharm.AddListener(SetCharmUI);
    }
    private void OnDestroy()
    {
        EventManager.Instance.OnApplyCharm.RemoveListener(SetCharmUI);
    }

    private void SetCharmUI(CharmInfo charm)
    {
        if (charm.option == Charm.Amplifier_Amulet)
        {
            ampCount++;
            ampLabel.text = ampCount.ToString();
        }
        if (charm.option == Charm.Apollo_Teardrop)
        {
            apolloCount++;
            apolloLabel.text = apolloCount.ToString();
        }
        if (charm.option == Charm.Smooth_Criminal_Ring)
        {
            smoothCount++;
            smoothLabel.text = smoothCount.ToString();
        }
        if (charm.option == Charm.Venomstar_Locket)
        {
            venomCount++;
            venomLabel.text = venomCount.ToString();
        }
        if (charm.option == Charm.True_Love_Orb)
        {
            loveCount++;
            loveLabel.text = loveCount.ToString();
        }
    }
}
