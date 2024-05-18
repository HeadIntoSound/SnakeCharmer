using System.Collections;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    public float speed;
    public float specialSpeed;
    public float currentSpeed;
    public float moveRadius;
    public Collider2D col;
    public Vector3 currentTarget = Vector3.zero;
    [SerializeField] SpriteRenderer sr;

    public void MoveRandomly()
    {
        if (currentTarget == Vector3.zero)
            currentTarget = MathLib.RandomRestrictedV3(1, moveRadius);

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * currentSpeed);
        if(currentTarget.x<transform.position.x)
            sr.flipX = true;
        else
            sr.flipY = false;
        if (transform.position == currentTarget)
            currentTarget = Vector3.zero;
    }

    public void MoveInDirection(Vector3 direction)
    {
        currentTarget = transform.position + direction;
        StartCoroutine(SpeedChange());
    }

    public void MoveTo(Vector3 target)
    {
        currentTarget = target;
        StartCoroutine(SpeedChange());
    }

    IEnumerator SpeedChange()
    {
        currentSpeed = specialSpeed;
        yield return new WaitForSeconds(3);
        currentSpeed = speed;
    }

    public void StopSpeedBoost()
    {
        StopCoroutine(SpeedChange());
        currentSpeed = speed;
    }
}
