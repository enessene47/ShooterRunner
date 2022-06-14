using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject _gameInCanvas;

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _diamondText;

    [SerializeField] private Image _diamondImage;

    private Vector2 _anchoredDiamondPos;

    private int _diamond;

    void Start()
    {
        if (PlayerPrefs.GetInt("FakeLevel") == 0)
            PlayerPrefs.SetInt("FakeLevel", 1);

        DiamondFirstSet();

        _levelText.text = "Level " + PlayerPrefs.GetInt("FakeLevel");

        EventManager.startEvent += () =>
        {
            _startCanvas.SetActive(false);
            _gameInCanvas.SetActive(true);
        };

        EventManager.failEvent += () => _failCanvas.SetActive(true);
        EventManager.successEvent += () => _successCanvas.SetActive(true);
    }

    public void StartEvent() => EventManager.AwakeStartEvent();

    private void DiamondFirstSet()
    {
        _diamond = PlayerPrefs.GetInt("Diamond", 0);
        _diamondText.text = _diamond.ToString();
        _anchoredDiamondPos = _diamondImage.GetComponent<RectTransform>().anchoredPosition;
    }
    private void DiamondTextUpdate()
    {
        _diamond++;

        _diamondText.text = _diamond.ToString();

        PlayerPrefs.SetInt("Diamond", _diamond);
    }

    public void DiamondCollectAnim(Vector3 diamondPos)
    {
        Vector2 screenPos = ObjectManager.instance.GetCam.WorldToScreenPoint(diamondPos);

        RectTransform rectClone = PoolManager.instance.GetDiamondEffect();

        rectClone.anchoredPosition = screenPos;

        rectClone.transform.SetParent(_gameInCanvas.transform);

        rectClone.MyDOAnchorPos(_anchoredDiamondPos, .5f, () =>
        {
            rectClone.gameObject.SetActive(false);

            DiamondTextUpdate();
        });
    }
}
