using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        PoolManager.instance.PlayBulletEffect(transform.position);

        gameObject.SetActive(false);
    }
}
