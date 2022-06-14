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

    [SerializeField] private Transform _obstacleParent;

    private List<Vector3> _obstaclePoints;

    private void Start() => StartCoroutine(ObstacleCreate());

    public void AddObstaclePoint(Vector3 vec) => _obstaclePoints.Add(vec);

    private IEnumerator ObstacleCreate()
    {
        var rnd = new System.Random();

        var suffleVec = _obstaclePoints.OrderBy(item => rnd.Next());

        int length = _obstacles.Length;

        yield return new WaitForSeconds(.1f);

        foreach (Vector3 pos in suffleVec)
            Instantiate(_obstacles[Random.Range(0, length)], pos, Quaternion.identity, _obstacleParent);
    }
}
