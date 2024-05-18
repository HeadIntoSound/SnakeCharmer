using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSnakeController : SnakeBehaviour
{
    void Update()
    {
        AvoidStacking();
        if (transform.parent == SnakeSpawnController.Instance.EvilSnakesParent)
            MoveRandomly();
    }
    void AvoidStacking()
    {
        Transform parent = SnakeSpawnController.Instance.EvilSnakesParent;
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            if (Vector3.Distance(transform.position, parent.GetChild(i).position) < 7 && transform != parent.GetChild(i))
                SnakeSpawnController.Instance.DisableSnake(parent.GetChild(i));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag(Tags.PLAYER))
            MoveInDirection(5 * (transform.position - other.transform.position));
    }
}
