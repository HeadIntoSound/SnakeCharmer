using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Transform pot;
    [SerializeField] float offset;

    // Update is called once per frame
    void Update()
    {
        PointToPot();
    }

    void PointToPot()
    {
        if (Vector3.Distance(transform.parent.position, pot.position) > 10)
        {
            sr.enabled = true;
            Vector3 dir = (pot.position - transform.parent.position).normalized;
            transform.position = transform.parent.position + dir * offset;
        }
        else
        {
            sr.enabled = false;
        }
    }
}
