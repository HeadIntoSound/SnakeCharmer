using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class CharmController : MonoBehaviour
{
    public Transform followersQ;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Collider2D col;
    [SerializeField] List<GameObject> snakes = new List<GameObject>();
    public bool isCharming;


    void Start()
    {
        EventManager.Instance.OnStartCharm.AddListener((charmTime, maxCapacity) => { StartCoroutine(CharmProcess(charmTime, maxCapacity)); });
        EventManager.Instance.OnStopCharm.AddListener(() =>
        {
            StopAllCoroutines();
            sr.enabled = false;
            col.enabled = false;
            isCharming = false;
        });
    }

    IEnumerator CharmProcess(float charmTime, int maxCapacity)
    {
        isCharming = true;
        sr.enabled = true;
        col.enabled = true;
        yield return new WaitForSeconds(charmTime);
        snakes.ForEach(snake =>
        {
            if (followersQ.childCount < maxCapacity)
            {
                snake.transform.SetParent(followersQ);
                snake.transform.localPosition = Vector3.zero;
            }
        });
        if (snakes.Count > 0)
            EventManager.Instance.OnSnakeCharmed.Invoke();
        isCharming = false;
        sr.enabled = false;
        col.enabled = false;
        yield break;
    }

    public void SetSize(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.SNAKE) && isCharming)
            snakes.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Tags.SNAKE) && isCharming)
            snakes.Remove(other.gameObject);
    }
}
