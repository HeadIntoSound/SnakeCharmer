using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);

    [SerializeField] float smoothing = 0.125f;

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void SetToPlayerPosition()
    {
        if (player == null)
            return;
        transform.position = player.position;
    }
}
