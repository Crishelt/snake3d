using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointsObjectPool : MonoBehaviour
{
    [Tooltip("Percent of the Environment area wehre points can be placed")]
    [SerializeField][Range(0, 1)] float spawnArea = 0.8f;
    [Tooltip("Amount of point objects that can spawn in the world")]
    [SerializeField][Range(0, 100)] int poolSize = 20;
    [Tooltip("Prefab object that will act as point")]
    [SerializeField] Point pointPrefab;


    List<Point> pointsPool = new List<Point>();
    Environment environment;

    private void Start()
    {
        environment = FindObjectOfType<Environment>();
        SpawnPoints();
        InvokeRepeating("RelocateEatenPoints", 2f, 1f);
    }

    public Vector3 GetSpawneablePosition()
    {
        //generate random value inside the range example: size 100 with spawnArea 1 => (range: -50, 50)
        Func<float, float> randValue = value =>
        {
            value = value * spawnArea / 2;
            return Random.Range(value * -1, value);
        };
        return new Vector3(
            randValue(environment.XSize),
            randValue(environment.YSize),
            randValue(environment.ZSize)
        );
    }

    private void SpawnPoints()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Point point = Instantiate(pointPrefab, environment.PointsParentTransform);
            point.name = $"Point {i}";
            pointsPool.Add(point);
            point.MoveToPosition(GetSpawneablePosition());
        }
    }

    private void RelocateEatenPoints()
    {
        foreach (Point point in pointsPool)
        {
            if (!point.isActiveAndEnabled)
            {
                point.MoveToPosition(GetSpawneablePosition());
                point.gameObject.SetActive(true);
            }
        }
    }

}
