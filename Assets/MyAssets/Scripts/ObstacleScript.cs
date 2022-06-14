using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleScript : MonoBehaviour
{
    enum Type { Destructible, Indestructible };

    [SerializeField] Type type;

    [SerializeField] Rigidbody[] rigidbodies;

    [SerializeField] private TextMeshProUGUI _healtText;

    Collider coll;

    private int _healt;

    private void Start()
    {
        if(type == Type.Destructible)
        {
            coll = GetComponent<Collider>();

            _healt = Random.Range(1, 5);

            TextUpdate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);

            if (type == Type.Destructible)
            {
                _healt--;

                TextUpdate();

                if (_healt <= 0)
                {
                    coll.enabled = false;

                    _healtText.gameObject.SetActive(false);

                    foreach (Rigidbody rb in rigidbodies)
                        rb.constraints = RigidbodyConstraints.None;

                    Vector3 explosionPos = transform.position;

                    Collider[] colliders = Physics.OverlapSphere(explosionPos, 5);

                    foreach (Collider hit in colliders)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();

                        if (rb != null)
                            rb.AddExplosionForce(100, explosionPos, 5, 3.0F);
                    }
                }
            }
            else
                transform.MyDOMoveZ(transform.position.z + 1, .1f);
        }    
    }

    private void TextUpdate() => _healtText.text = _healt.ToString();
}
