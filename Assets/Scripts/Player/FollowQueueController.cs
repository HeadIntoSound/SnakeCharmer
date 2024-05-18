using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowQueueController : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] int cols;
    [SerializeField] int currentCol;

    void Start()
    {
        EventManager.Instance.OnSnakeCharmed.AddListener(SortSnakes);
    }
    void SortSnakes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float yPos = Mathf.Floor(i / 5);
            transform.GetChild(i).transform.localPosition = new Vector3((i % 5) * offset.x, offset.y * yPos);
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnSnakeCharmed.RemoveListener(SortSnakes);
    }
}
