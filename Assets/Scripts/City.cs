using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [HideInInspector]
    public GameResource aspidos;
    [HideInInspector]
    public GameResource dolifront;

    public int totalHabitants;
    public int baseAspidosInfectedHabitants;
    public int baseDolifrontInfectedHabitants;

    private int m_CurrentInfectedHabitants;
    private int m_CurrentAspidosInfectedHabitants;
    private int m_CurrentDolifrontInfectedHabitants;

    [HideInInspector] public int shownInfectedHabitants;
    [HideInInspector] public int shownAspidosInfectedHabitants;
    [HideInInspector] public int shownDolifrontInfectedHabitants;

    public int newAspidosInfected;
    public int newDolifrontInfected;

    public float dayTime = 2.0f;

    public float infectionTime = 0.5f;

    private float m_ElapsedDayTime = 0f;
    private float m_ElapsedInfectionTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentAspidosInfectedHabitants = baseAspidosInfectedHabitants;
        m_CurrentDolifrontInfectedHabitants = baseDolifrontInfectedHabitants;
        m_CurrentInfectedHabitants = m_CurrentAspidosInfectedHabitants + m_CurrentDolifrontInfectedHabitants;
        shownAspidosInfectedHabitants = m_CurrentAspidosInfectedHabitants;
        shownDolifrontInfectedHabitants = m_CurrentDolifrontInfectedHabitants;
        shownInfectedHabitants = shownAspidosInfectedHabitants + shownDolifrontInfectedHabitants;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ElapsedDayTime > dayTime)
        {
            m_ElapsedDayTime = 0f;
            shownAspidosInfectedHabitants = m_CurrentAspidosInfectedHabitants;
            shownDolifrontInfectedHabitants = m_CurrentDolifrontInfectedHabitants;
            shownInfectedHabitants = shownAspidosInfectedHabitants + shownDolifrontInfectedHabitants;
        }

        if (m_ElapsedInfectionTime > infectionTime)
        {
            m_ElapsedInfectionTime = 0;
            if (m_CurrentInfectedHabitants < totalHabitants)
            {
                m_CurrentAspidosInfectedHabitants += newAspidosInfected;
                if (m_CurrentAspidosInfectedHabitants + m_CurrentDolifrontInfectedHabitants > totalHabitants)
                    m_CurrentAspidosInfectedHabitants = totalHabitants - m_CurrentDolifrontInfectedHabitants;
                m_CurrentDolifrontInfectedHabitants += newDolifrontInfected;
                if (m_CurrentAspidosInfectedHabitants + m_CurrentDolifrontInfectedHabitants > totalHabitants)
                    m_CurrentDolifrontInfectedHabitants = totalHabitants - m_CurrentAspidosInfectedHabitants;
                m_CurrentInfectedHabitants = m_CurrentAspidosInfectedHabitants + m_CurrentDolifrontInfectedHabitants;
            }
        }
        
        m_ElapsedDayTime += Time.deltaTime;
        m_ElapsedInfectionTime += Time.deltaTime;
    }
    
    public void AddCommandResources(int resourceQuantity, EGameResourceType resourceType)
    {
        switch (resourceType)
        {
            case EGameResourceType.Aspidos:
                aspidos.quantity += resourceQuantity;
                break;
            case EGameResourceType.Dolifront:
                dolifront.quantity += resourceQuantity;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
    }

    private void OnMouseDown()
    {
        GameObject.Find("UI").GetComponent<UIScript>().ActivateCityPanel(this);
    }
}
