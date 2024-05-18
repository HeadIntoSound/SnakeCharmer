using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseController : MonoBehaviour
{
    [SerializeField] EvilSnakeController evilSnakeController;
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag(Tags.PLAYER)))
        {
            evilSnakeController.MoveTo(other.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag(Tags.PLAYER)))
        {
            evilSnakeController.StopSpeedBoost();
        }
    }
}
