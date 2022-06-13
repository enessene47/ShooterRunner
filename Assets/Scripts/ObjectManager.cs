using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    #region Singleton

    public static ObjectManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    #endregion

    [SerializeField] private Material _destructibleObstacleMat;

    [SerializeField] private Material _indestructibleObstacleMat;

    public Material DestructibleMaterial => _destructibleObstacleMat;
    public Material IndestructibleMaterial => _indestructibleObstacleMat;
}
