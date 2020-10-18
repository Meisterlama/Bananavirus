using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public GameObject factoriesParent;
    public GameObject citiesParent;
    
    [HideInInspector]
    public List<Factory> factories;
    [HideInInspector]
    public List<City> cities;

    public int startingMoney;
    public int gameLength;
    public float dayLength;

    [HideInInspector]
    public int currentMoney;
    
    private float m_ElapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        currentMoney = startingMoney;
        for (int i = 0; i < factoriesParent.transform.childCount; i++)
        {
            factories.Add(factoriesParent.transform.GetChild(i).GetComponent<Factory>());
        }
        for (int i = 0; i < citiesParent.transform.childCount; i++)
        {
            cities.Add(citiesParent.transform.GetChild(i).GetComponent<City>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
