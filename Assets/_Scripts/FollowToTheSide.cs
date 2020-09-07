using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTheSide : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    private void FixedUpdate()
    {
        transform.position = Target.position + Vector3.up * Offset.y
            + Vector3.ProjectOnPlane(Target.right, Vector3.up).normalized * Offset.x
            + Vector3.ProjectOnPlane(Target.forward, Vector3.up).normalized * Offset.z;

        transform.eulerAngles = new Vector3(0, Target.eulerAngles.y, 0);
    }
}
