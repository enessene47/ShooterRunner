using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    #region Singleton

    public static ObjectManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            EventManager.ResetEvent();
        }
    }

    #endregion

    [SerializeField] private GameObject[] _obstacles;

    [SerializeField] private GameObject[] _guns;

    [SerializeField] private GameObject _gate;

    [SerializeField] private Transform _obstacleParent;

    [SerializeField] private Transform _gateParent;

    private List<Vector3> _obstaclePoints;

    private List<Vector3> _gatePoints;

    private void Start() => StartCoroutine(ObstacleCreate());

    public void AddObstaclePoint(Vector3 vec)
    {
        if (_obstaclePoints == null)
            _obstaclePoints = new List<Vector3>();

        _obstaclePoints.Add(vec);
    }
    public void AddGatePoint(Vector3 vec)
    {
        if (_gatePoints == null)
            _gatePoints = new List<Vector3>();

        _gatePoints.Add(vec);
    }

    private IEnumerator ObstacleCreate()
    {
        yield return new WaitForSeconds(.1f);

        var random = new System.Random();

        var suffleVec = _obstaclePoints.OrderBy(item => random.Next()).ToList();

        int length = _obstacles.Length;

        int _obstacleCount = Random.Range((int) (suffleVec.Count * .8f), suffleVec.Count);

        for (int i = 0; i < _obstacleCount; i++)
        {
            int rnd = Random.Range(0, length);

            Instantiate(_obstacles[rnd], suffleVec[i], _obstacles[rnd].transform.rotation, _obstacleParent);
        }

        if (_gatePoints != null)
        {
            suffleVec = _gatePoints.OrderBy(item => random.Next()).ToList();

            length = Random.Range((int)(suffleVec.Count * .8f), suffleVec.Count);

            for(int i = 0; i < length; i++)
                Instantiate(_gate, suffleVec[i], _gate.transform.rotation, _gateParent);
        }
    }

    public Transform GetGun(int i = 0) => Instantiate(_guns[i]).transform;
}
