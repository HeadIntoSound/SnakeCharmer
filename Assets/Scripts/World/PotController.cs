using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distanceToPlayer;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > distanceToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 10);
        }
    }
}
