using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGizmos : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
