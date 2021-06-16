using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGizmos : MonoBehaviour
{
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
