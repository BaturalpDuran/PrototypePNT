using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private GameObject[] runners;
    List<RankingSystem> sortArray = new List<RankingSystem>();
    public UnityEngine.UI.Text RankText;
    public int boyRank;

    private void Awake()
    {
        instance = this;
        runners = GameObject.FindGameObjectsWithTag("Runners");
    }

    private void Start()
    {
        for (int i = 0; i < runners.Length; i++)
        {
            sortArray.Add(runners[i].GetComponent<RankingSystem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateRank();
       
    }

    private void CalculateRank()
    {
        sortArray = sortArray.OrderBy(x => x.distance).ToList();
        

        switch (sortArray.Count)
        {
            case 11:
                sortArray[0].rank = 1;
                sortArray[1].rank = 2;
                sortArray[2].rank = 3;
                sortArray[3].rank = 4;
                sortArray[4].rank = 5;
                sortArray[5].rank = 6;
                sortArray[6].rank = 7;
                sortArray[7].rank = 8;
                sortArray[8].rank = 9;
                sortArray[9].rank = 10;
                sortArray[10].rank = 11;
                break;

        }
        foreach (RankingSystem rs in sortArray)
        {
            if (rs.gameObject.name == "Boy")
            {
                RankText.text = "Your Rank :" + rs.rank;
                boyRank = rs.rank;
            }
        }   
    }
}
