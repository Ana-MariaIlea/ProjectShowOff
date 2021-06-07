using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Time to stay on the waypoint")]
    public float timeToStay=1f;

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .3f);

    }
}
