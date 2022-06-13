using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [SerializeField] private Transform[] guns;

    [SerializeField] private Transform bulletPoint;

    [SerializeField] private float _speed;

    private bool userActive;

    void Start()
    {
        int gun = PlayerPrefs.GetInt("Gun");

        guns[gun].gameObject.SetActive(true);

        bulletPoint = guns[gun].GetChild(0);

        base.BaseStart();

        userActive = true;

        StartCoroutine(ShotBullet(gun));
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        obj.position += Vector3.forward * Time.deltaTime * _speed;

        Swipe();
    }

    private IEnumerator ShotBullet(int gun)
    {
        Vector3 force = new Vector3(0, 75, 1500);

        float bulletSpeed = .8f - gun * .2f;

        while(userActive)
        {
            yield return new WaitForSeconds(bulletSpeed);

            for(int i = 0; i <= gun; i++)
                PoolManager.instance.GetBulletObject(bulletPoint.position + Vector3.one * .2f * i).AddForce(force);
        }
    }
}
