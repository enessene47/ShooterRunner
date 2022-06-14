using UnityEngine;

public class GatePoint : MonoBehaviour
{
    private void Start() => ObjectManager.instance.AddGatePoint(transform.position);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(transform.position + Vector3.up * .5f, .3f);
    }
}
