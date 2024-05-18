using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using Utilities;
public class UIController : MonoBehaviour
{
    [System.Serializable]
    class CharmSelectionItem
    {
        public Button btn;
        public TMP_Text name;
        public Image image;
        public TMP_Text description;

    }

    [SerializeField] TMP_Text level;
    [SerializeField] TMP_Text currentExp;
    [SerializeField] TMP_Text time;
    [SerializeField] Slider expSlider;
    [SerializeField] GameObject charmSelectionScreenGO;
    [SerializeField] CharmSelectionItem[] charmSelectionItems;
    [SerializeField] TMP_Text totalSnakesText;

    float initialTime;

    void Start()
    {
        EventManager.Instance.OnLevelUp.AddListener(lvl => { level.text = lvl.ToString(); });
        EventManager.Instance.OnIncreasedSnakeCount.AddListener((current, max) =>
        {
            expSlider.maxValue = max;
            expSlider.value = current;
            currentExp.text = string.Format("{0}/{1}", current, max);
        });
        EventManager.Instance.OnCharmSelection.AddListener(CharmSelectionScreen);
        EventManager.Instance.OnSetScore.AddListener((totalSnakes) => { totalSnakesText.text = totalSnakes.ToString(); });
        initialTime = Time.time;
    }

    void Update()
    {
        Timer();
    }

    void Timer()
    {
        var t = TimeSpan.FromSeconds((double)Time.time - initialTime);
        time.text = string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
    }

    private void CharmSelectionScreen(CharmInfo[] items, UnityAction<CharmInfo> Callback)
    {
        for (int i = 0; i < items.Length; i++)
        {
            CharmInfo charm = items[i];
            charmSelectionItems[i].name.text = Info.GetCharmName(charm.option);
            charmSelectionItems[i].name.color = Info.GetRarityColor(charm.rarity);
            charmSelectionItems[i].image.sprite = ResourceManager.Instance.GetCharmImage(charm.option);
            charmSelectionItems[i].description.text = Info.GetCharmDescription(charm.option);
            charmSelectionItems[i].btn.onClick.AddListener(() =>
            {
                charmSelectionScreenGO.SetActive(false);
                EventManager.Instance.OnApplyCharm.Invoke(charm);
                ResetButtons();
                Callback(charm);
            });
        }
        charmSelectionScreenGO.SetActive(true);
    }

    void ResetButtons()
    {
        foreach (var item in charmSelectionItems)
        {
            item.btn.onClick.RemoveAllListeners();
        }
    }

}
