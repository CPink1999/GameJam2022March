using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>Contains references for the points that the player can jump to.</summary>
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


    /// <summary>Gets the pillar at a given index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    public Transform GetPillar (int index)
    {
        return jumpPoints[index];
    }
}
