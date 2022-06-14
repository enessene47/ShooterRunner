using Mechanics;
using System.Collections;
using UnityEngine;

public class PlayerController : SwipeMecLast
{
    [Header("Player Control")]

    [Space(25)]

    [SerializeField] private Transform[] _guns;

    [SerializeField] private Transform _bulletPoint;

    [SerializeField] private float _speed;

    private int _GunIndex;

    public int GunIndex
    {
        get => _GunIndex;
        set
        {
            _guns[_GunIndex].gameObject.SetActive(false);

            _GunIndex = value;

            _guns[_GunIndex].gameObject.SetActive(true);

            _bulletPoint = _guns[_GunIndex].GetChild(0);
        }
    }

    private bool _userActive;

    void Start()
    {
        base.BaseStart();

        GunIndex = 0;

        EventManager.startEvent += () =>
        {
            Animator.SetTrigger("Run");

            StartCoroutine(ShotBullet(GunIndex));
        };

        EventManager.failEvent += () =>
        {
            _userActive = false;

            _guns[GunIndex].gameObject.SetActive(false);

            transform.MyDOMoveZ(transform.position.z - 1);

            Animator.SetTrigger("Fail");
        };

        EventManager.successEvent += () =>
        {
            _userActive = false;

            _guns[GunIndex].gameObject.SetActive(false);

            Animator.SetTrigger("Success");
        };
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

        float bulletSpeed = .5f - GunIndex * .1f;

        Vector3 force = Vector3.zero;

        float yNumber = 1500.0f / 30.0f;

        yield return new WaitForSeconds(.5f);

        while (_userActive)
        {
            for (int i = 0; i <= GunIndex; i++)
            {
                force = new Vector3(transform.localRotation.y * 100 * yNumber, -50, 2500);

                PoolManager.instance.GetBulletObject(_bulletPoint.position + Vector3.one * .2f * i).AddForce(force);
            }

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
        else if (other.CompareTag("Gate"))
        {
            other.enabled = false;

            other.transform.GetChild(0).gameObject.SetActive(false);

            GunIndex = other.GetComponent<GateDoorScript>().GunType;
        }
        else if (other.CompareTag("EndGame"))
            EventManager.AwakeSuccessEvent();
    }
}
