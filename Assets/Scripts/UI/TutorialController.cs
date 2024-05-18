using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] GameObject tutorialPopUp;
    bool show = true;

    public static TutorialController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (show)
        {
            Time.timeScale = 0;
            tutorialPopUp.SetActive(true);
        }
    }

    public void Play()
    {
        tutorialPopUp.SetActive(false);
        Time.timeScale = 1;
        show = false;
    }
}
