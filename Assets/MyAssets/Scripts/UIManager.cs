using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion


    [SerializeField] private GameObject _startCanvas;

    void Start()
    {
        EventManager.startEvent += () => _startCanvas.SetActive(false);
    }

    public void StartEvent() => EventManager.AwakeStartEvent();
}
