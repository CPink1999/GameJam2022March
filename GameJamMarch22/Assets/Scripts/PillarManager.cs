using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    public int Count {
        get
        {
            return jumpPoints.Length;
        }
    }
    public Transform Center
    {
        get
        {
            return center;
        }
    }
    [SerializeField] private Transform center;
    [SerializeField] private Transform[] jumpPoints = new Transform[0];

    public Transform GetPillar (int index)
    {
        return jumpPoints[index];
    }
}
