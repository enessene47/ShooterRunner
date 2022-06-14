using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Queue<Rigidbody> pooledBullet;

    public Queue<ParticleSystem> pooledBulletEffect;

    public GameObject objectPrefabBullet;

    public GameObject objectPrefabBulletEffect;

    public int poolSizeBullet;

    public static PoolManager instance;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            return;

        poolSizeBullet *= PlayerPrefs.GetInt("Gun") + 1;

        pooledBullet = new Queue<Rigidbody>();

        pooledBulletEffect = new Queue<ParticleSystem>();

        for (int i = 0; i < poolSizeBullet; i++)
        {
            GameObject bullet = Instantiate(objectPrefabBullet);

            GameObject effect = Instantiate(objectPrefabBulletEffect);

            bullet.SetActive(false);

            effect.transform.position = Vector3.forward * -10;

            bullet.hideFlags = HideFlags.HideInHierarchy;

            effect.hideFlags = HideFlags.HideInHierarchy;

            pooledBullet.Enqueue(bullet.GetComponent<Rigidbody>());

            pooledBulletEffect.Enqueue(effect.GetComponent<ParticleSystem>());
        }
    }

    public Rigidbody GetBulletObject(Vector3 pos)
    {
        Rigidbody physics = pooledBullet.Dequeue();

        physics.gameObject.SetActive(true);

        physics.velocity = Vector3.zero;

        physics.position = pos;

        pooledBullet.Enqueue(physics);

        return physics; 
    }

    public void PlayBulletEffect(Vector3 pos)
    {
        ParticleSystem effect = pooledBulletEffect.Dequeue();

        effect.transform.position = pos;

        effect.Play();

        pooledBulletEffect.Enqueue(effect);
    }
}
