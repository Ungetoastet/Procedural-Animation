using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringVisualizer : MonoBehaviour
{
    [SerializeField]
    private SpringJoint Spring;

    private void OnDrawGizmos()
    {
        if (Spring != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Spring.anchor, Spring.connectedAnchor);
        }
    }
}
