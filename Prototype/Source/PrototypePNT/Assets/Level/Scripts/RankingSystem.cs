using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RankingSystem : MonoBehaviour
{
    public float distance;
    private Vector3 finish;
    public int rank;
    void Start()
    {
        finish = GameObject.Find("Finish").transform.position;
    }
    void Update()
    {
        CalculateTheDistance();
    }

    private void CalculateTheDistance()
    {
        distance = Vector3.Distance(transform.position, finish);

    }
}
