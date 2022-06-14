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
        if(type == Type.Destructible && other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);

            _healt--;

            TextUpdate();

            if (_healt == 0)
                gameObject.SetActive(false);
        }
        else if(other.CompareTag("Player"))
        {
            other.enabled = false;

            EventManager.AwakeFailEvent();
        }    
    }

    private void TextUpdate() => _healtText.text = _healt.ToString();
}
