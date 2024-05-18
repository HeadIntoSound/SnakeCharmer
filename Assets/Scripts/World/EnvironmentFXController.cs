using UnityEngine;

public class EnvironmentFXController : MonoBehaviour
{
    [SerializeField] Transform rangeFX;
    [SerializeField] Transform speedFX;
    [SerializeField] Transform slowFX;
    [SerializeField] Transform player;

    private void Update()
    {
        CheckForRespawn(rangeFX);
        CheckForRespawn(speedFX);
        CheckForRespawn(slowFX);
    }

    void CheckForRespawn(Transform toCheck)
    {
        if (Vector3.Distance(player.position, toCheck.position) < 25)
            return;
        SetNewPosition(toCheck);
    }

    void SetNewPosition(Transform toSet)
    {
        Vector3 newPos = player.position - MathLib.RandomRestrictedV3(15, 40);
        if (Vector3.Distance(newPos, rangeFX.position) < 5 && toSet != rangeFX)
        {
            SetNewPosition(toSet);
            return;
        }
        if (Vector3.Distance(newPos, speedFX.position) < 5 && toSet != speedFX)
        {
            SetNewPosition(toSet);
            return;
        }
        if (Vector3.Distance(newPos, slowFX.position) < 5 && toSet != slowFX)
        {
            SetNewPosition(toSet);
            return;
        }
        toSet.position = newPos;
    }

}
