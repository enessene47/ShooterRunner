using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject _failCanvas;
    [SerializeField] private GameObject _successCanvas;

    [SerializeField] private TextMeshProUGUI _levelText;

    void Start()
    {
        if (PlayerPrefs.GetInt("FakeLevel") == 0)
            PlayerPrefs.SetInt("FakeLevel", 1);

        _levelText.text = "Level " + PlayerPrefs.GetInt("FakeLevel");

        EventManager.startEvent += () => _startCanvas.SetActive(false);
        EventManager.failEvent += () => _failCanvas.SetActive(true);
        EventManager.successEvent += () => _successCanvas.SetActive(true);
    }

    public void StartEvent() => EventManager.AwakeStartEvent();
}
