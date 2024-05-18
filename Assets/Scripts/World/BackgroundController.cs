using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] Transform[] background;
    [SerializeField] Transform player;
    [SerializeField] Transform center;

    void Move()
    {
        var distance = player.position - center.position;
        if (Mathf.Abs(distance.x) > 50)
        {
            if (distance.x > 0)
            {
                background[3].position += Vector3.right * 200;
                background[2].position += Vector3.right * 200;
            }
            else
            {
                background[0].position += Vector3.left * 200;
                background[1].position += Vector3.left * 200;
            }
            var temp = background[0];
            background[0] = background[3];
            background[3] = temp;
            temp = background[2];
            background[2] = background[1];
            background[1] = temp;
            center.position += Vector3.right * 100 * (distance.x / Mathf.Abs(distance.x));
            return;
        }
        if (Mathf.Abs(distance.y) > 50)
        {
            if (distance.y < 0)
            {
                background[0].position += Vector3.down * 200;
                background[3].position += Vector3.down * 200;
            }
            else
            {
                background[1].position += Vector3.up * 200;
                background[2].position += Vector3.up * 200;
            }
            var temp = background[0];
            background[0] = background[1];
            background[1] = temp;
            temp = background[3];
            background[3] = background[2];
            background[2] = temp;
            center.position += Vector3.up * 100 * (distance.y / Mathf.Abs(distance.y));
            return;
        }
    }

    private void Update()
    {
        Move();
    }

}