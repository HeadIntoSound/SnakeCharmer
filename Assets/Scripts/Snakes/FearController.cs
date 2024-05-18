using UnityEngine;

public class FearController : MonoBehaviour
{
    [SerializeField] SnakeController snakeController;
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag(Tags.PLAYER) || other.CompareTag(Tags.POT)) && !snakeController.captured)
        {
            snakeController.MoveInDirection(6 * (transform.position - other.transform.position).normalized);
        }
    }
}
