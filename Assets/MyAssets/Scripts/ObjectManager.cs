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

            _obstaclePoints = new List<Vector3>();
        }
    }

    #endregion

    [SerializeField] private GameObject[] _obstacles;

    [SerializeField] private GameObject[] _guns;

    [SerializeField] private Transform _obstacleParent;

    private List<Vector3> _obstaclePoints;

    private int _obstacleCount;

    private void Start() => StartCoroutine(ObstacleCreate());

    public void AddObstaclePoint(Vector3 vec) => _obstaclePoints.Add(vec);

    private IEnumerator ObstacleCreate()
    {
        yield return new WaitForSeconds(.1f);

        var random = new System.Random();

        var suffleVec = _obstaclePoints.OrderBy(item => random.Next()).ToList();

        int length = _obstacles.Length;

        _obstacleCount = GameManager.instance.ActiveScene == 0 ? Random.Range(20, 30) : Random.Range(150, 200);

        for (int i = 0; i < _obstacleCount; i++)
        {
            int rnd = Random.Range(0, length);

            Instantiate(_obstacles[rnd], suffleVec[i], _obstacles[rnd].transform.rotation, _obstacleParent);
        }
    }

    public Transform GetGun(int i = 0) => Instantiate(_guns[i]).transform;
}
