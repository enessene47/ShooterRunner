using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleScript : MonoBehaviour
{
    enum Type { Destructible, Indestructible };

    [SerializeField] Type type;

    [SerializeField] private TextMeshProUGUI _healtText;

    private int _healt;

    private void Start()
    {
        if(type == Type.Destructible)
        {
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
                    gameObject.SetActive(false);
            }
            else
                transform.MyDOMoveZ(transform.position.z + 1, .1f);
        }    
    }

    private void TextUpdate() => _healtText.text = _healt.ToString();
}
