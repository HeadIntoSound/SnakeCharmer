using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : SnakeBehaviour
{
    [SerializeField] GameObject fearController;
    public bool captured;

    void Start()
    {
        EventManager.Instance.OnSnakeCharmed.AddListener(OnCharm);
        EventManager.Instance.OnPotFill.AddListener(OnRelease);
    }

    void Update()
    {
        if (transform.parent == SnakeSpawnController.Instance.ActiveSnakesParent && !captured)
            MoveRandomly();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.CHARM))
        {
            currentSpeed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Tags.CHARM))
        {
            currentSpeed = speed;
        }
    }

    void OnCharm()
    {
        if (transform.parent == SnakeSpawnController.Instance.FollowerQueue)
        {
            captured = true;
            col.enabled = false;
            fearController.SetActive(false);
        }

    }

    void OnRelease()
    {
        if (transform.parent != SnakeSpawnController.Instance.FollowerQueue)
        {
            captured = false;
            col.enabled = true;
            fearController.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnSnakeCharmed.RemoveListener(OnCharm);
    }
}
