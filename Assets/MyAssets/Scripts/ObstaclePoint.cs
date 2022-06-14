using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoint : MonoBehaviour
{
    void Start() => ObjectManager.instance.AddObstaclePoint(transform.position);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position + Vector3.up * .5f, .3f);
    }
}
