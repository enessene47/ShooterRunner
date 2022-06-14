using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [Header("Player Control")]

    [Space(25)]

    [SerializeField] private Transform[] _guns;

    [SerializeField] private Transform _bulletPoint;

    [SerializeField] private float _speed;

    private bool _userActive;

    void Start()
    {
        int gun = PlayerPrefs.GetInt("Gun");

        _guns[gun].gameObject.SetActive(true);

        _bulletPoint = _guns[gun].GetChild(0);

        base.BaseStart();

        EventManager.startEvent += () =>
        {
            Animator.SetTrigger("Run");

            StartCoroutine(ShotBullet(gun));
        };

        EventManager.failEvent += () =>
        {
            _userActive = false;

            transform.MyDOMoveZ(transform.position.z - 1);

            Animator.SetTrigger("Dead");
        };

        EventManager.successEvent += () => _userActive = false;
    }

    private void Update()
    {
        if (!_userActive)
        {
            ResetValues();

            return;
        }

        obj.position += Vector3.forward * Time.deltaTime * _speed;

        Swipe();
    }

    private IEnumerator ShotBullet(int gun)
    {
        _userActive = true;

        float bulletSpeed = .5f - gun * .1f;

        Vector3 force = Vector3.zero;

        yield return new WaitForSeconds(.5f);

        while (_userActive)
        {
            force = new Vector3(transform.rotation.y * 2500, -50, 2500);

            for (int i = 0; i <= gun; i++)
                PoolManager.instance.GetBulletObject(_bulletPoint.position + Vector3.one * .2f * i).AddForce(force);

            yield return new WaitForSeconds(bulletSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.enabled = false;

            EventManager.AwakeFailEvent();
        }
    }
}
