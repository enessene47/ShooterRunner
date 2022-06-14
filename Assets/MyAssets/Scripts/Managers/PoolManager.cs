using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Queue<RectTransform> _pooledDiamond;

    [SerializeField] private Queue<Rigidbody> _pooledBullet;

    [SerializeField] private Queue<ParticleSystem> _pooledBulletEffect;

    [SerializeField] private GameObject _objectPrefabDiamond;

    [SerializeField] private GameObject _objectPrefabBullet;

    [SerializeField] private GameObject _objectPrefabBulletEffect;

    [SerializeField] private int _poolSizeDiamond;

    [SerializeField] private int _poolSizeBullet;

    public static PoolManager instance;

    private void Awake()
    {
        if (instance == null) 
            instance = this;
        else
            return;

        _pooledDiamond = new Queue<RectTransform>();

        _pooledBullet = new Queue<Rigidbody>();

        _pooledBulletEffect = new Queue<ParticleSystem>();

        for(int i = 0; i < _poolSizeDiamond; i++)
        {
            GameObject effect = Instantiate(_objectPrefabDiamond);

            effect.SetActive(false);

            effect.hideFlags = HideFlags.HideInHierarchy;

            _pooledDiamond.Enqueue(effect.GetComponent<RectTransform>());
        }

        for (int i = 0; i < _poolSizeBullet; i++)
        {
            GameObject bullet = Instantiate(_objectPrefabBullet);

            GameObject effect = Instantiate(_objectPrefabBulletEffect);

            bullet.SetActive(false);

            effect.transform.position = Vector3.forward * -10;

            bullet.hideFlags = HideFlags.HideInHierarchy;

            effect.hideFlags = HideFlags.HideInHierarchy;

            _pooledBullet.Enqueue(bullet.GetComponent<Rigidbody>());

            _pooledBulletEffect.Enqueue(effect.GetComponent<ParticleSystem>());
        }
    }

    public Rigidbody GetBulletObject(Vector3 pos)
    {
        Rigidbody physics = _pooledBullet.Dequeue();

        physics.gameObject.SetActive(true);

        physics.velocity = Vector3.zero;

        physics.position = pos;

        _pooledBullet.Enqueue(physics);

        return physics; 
    }

    public void PlayBulletEffect(Vector3 pos)
    {
        ParticleSystem effect = _pooledBulletEffect.Dequeue();

        effect.transform.position = pos;

        effect.Play();

        _pooledBulletEffect.Enqueue(effect);
    }

    public RectTransform GetDiamondEffect()
    {
        RectTransform effect = _pooledDiamond.Dequeue();

        effect.gameObject.SetActive(true);

        effect.transform.SetParent(null);

        _pooledDiamond.Enqueue(effect);

        return effect;
    }
}
