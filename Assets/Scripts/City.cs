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

    private float m_DayLength;
    
    private float m_ElapsedDayTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        m_CurrentAspidosInfectedHabitants = baseAspidosInfectedHabitants;
        m_CurrentDolifrontInfectedHabitants = baseDolifrontInfectedHabitants;
        m_CurrentInfectedHabitants = m_CurrentAspidosInfectedHabitants + m_CurrentDolifrontInfectedHabitants;
        shownAspidosInfectedHabitants = m_CurrentAspidosInfectedHabitants;
        shownDolifrontInfectedHabitants = m_CurrentDolifrontInfectedHabitants;
        shownInfectedHabitants = shownAspidosInfectedHabitants + shownDolifrontInfectedHabitants;

        m_DayLength = GameObject.Find("GameManager").GetComponent<GameManager>().dayLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ElapsedDayTime > m_DayLength)
        {
            m_ElapsedDayTime = 0f;
            shownAspidosInfectedHabitants = m_CurrentAspidosInfectedHabitants;
            shownDolifrontInfectedHabitants = m_CurrentDolifrontInfectedHabitants;
            shownInfectedHabitants = shownAspidosInfectedHabitants + shownDolifrontInfectedHabitants
                ;
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

    public bool CheckWinCondition()
    {
        return aspidos.quantity >= m_CurrentAspidosInfectedHabitants &&
               dolifront.quantity >= m_CurrentDolifrontInfectedHabitants;
    }

    private void OnMouseDown()
    {
        GameObject.Find("UI").GetComponent<UIScript>().ActivateCityPanel(this);
    }
}
