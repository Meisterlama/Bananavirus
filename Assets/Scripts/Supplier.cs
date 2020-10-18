using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SupplierResource
{

    public GameResource Resource;
    public float price;
    public float timeToProduce;
}

public struct SupplierCommand
{
    public Factory TargetFactory;
    public int AspiniumQuantity;
    public int DoliniumQuantity;
    public float ElapsedTime;
    public float TimeToProduce;
    public int Price;
    public bool isDone;

    public SupplierCommand(Factory targetFactory, int aspiniumQuantity, int doliniumQuantity, float timeToProduce, int price)
    {
        TargetFactory = targetFactory;
        AspiniumQuantity = aspiniumQuantity;
        DoliniumQuantity = doliniumQuantity;
        TimeToProduce = timeToProduce;
        Price = price;
        ElapsedTime = 0f;
        isDone = false;
    }
}

public class Supplier : MonoBehaviour
{
    public int aspiniumQuantity = 100;
    public float aspiniumPrice = 1f;
    public float aspiniumTimeToProduce = 1f;
    public int doliniumQuantity = 100;
    public float doliniumPrice = 1f;
    public float doliniumTimeToProduce = 1f;

    public float timeToShip = 0f;
    
    private GameManager m_GameManager;

    [HideInInspector]
    public SupplierCommand CurrentCommand;

    [HideInInspector]
    public GameResource aspinium;
    [HideInInspector]
    public GameResource dolinium;

    // Start is called before the first frame update
    void Start()
    {
        aspinium = new GameResource(EGameResourceType.Aspinium, aspiniumTimeToProduce, aspiniumPrice, aspiniumQuantity  );
        dolinium = new GameResource(EGameResourceType.Dolinium, doliniumTimeToProduce, doliniumPrice, doliniumQuantity );
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CurrentCommand.isDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CurrentCommand.isDone)
        {
            ProcessCommand();
        }
    }

    public bool MakeCommand(Factory targetFactory, int aspiniumQuantity, int doliniumQuantity)
    {
        //,
        // Mathf.Max(aspiniumCount * (m_CurrentSupplier.aspinium.timeToProduce),
        //     doliniumCount * (m_CurrentSupplier.dolinium.timeToProduce)),
        // (int) (aspiniumCount * (m_CurrentSupplier.aspinium.price) +
        //        doliniumCount * (m_CurrentSupplier.dolinium.price)

        float timeToProduce =
            Mathf.Max(aspiniumQuantity * (aspinium.timeToProduce), doliniumQuantity * (dolinium.timeToProduce));
        int price = (int) (aspiniumQuantity * (aspinium.price) + doliniumQuantity * (dolinium.price));

        if (price > m_GameManager.currentMoney)
            return false;

        m_GameManager.currentMoney -= price;

        CurrentCommand = new SupplierCommand(targetFactory, aspiniumQuantity, doliniumQuantity, timeToProduce, price);
        return true;
    }

    private void ProcessCommand()
    {
        if (CurrentCommand.ElapsedTime > CurrentCommand.TimeToProduce)
        {
            CurrentCommand.TargetFactory.AddCommandResources(CurrentCommand.AspiniumQuantity,
                CurrentCommand.DoliniumQuantity);
            CurrentCommand.isDone = true;
        }
        CurrentCommand.ElapsedTime += Time.deltaTime;
    }

    private void OnMouseDown()
    {
        GameObject.Find("UI").GetComponent<UIScript>().ActivateSupplierPanel(this);
    }
}