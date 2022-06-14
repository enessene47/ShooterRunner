using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDoorScript : MonoBehaviour
{
    private int _gunType;

    void Start()
    {
        _gunType = Random.Range(0, 3);

        Transform trs = ObjectManager.instance.GetGun(_gunType);

        trs.parent = transform;

        trs.localPosition = new Vector3(.85f, 1.2f, 0);

        trs.localRotation = Quaternion.Euler(Vector3.up * -90);

        trs.localScale = Vector3.one * 1.5f;
    }

    public int GunType => _gunType;
}
