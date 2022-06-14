using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("EndGame"))
            return;
        else if(other.CompareTag("Floor"))
            gameObject.SetActive(false);

        PoolManager.instance.PlayBulletEffect(transform.position);
    }
}
