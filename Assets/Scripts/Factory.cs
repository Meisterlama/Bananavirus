using System;
using UnityEngine;

public struct FactoryInventory
{
    public GameResource Aspinium;
    public GameResource Dolinium;
    public GameResource Aspidos;
    public GameResource Dolifront;
}

public struct FactoryCommand
{
    public City TargetCity;
    public int ResourceQuantity;
    public EGameResourceType ResourceType;
    public float ElapsedTime;
    public float TimeToProduce;
    public int Price;
    public bool isDone;

    public FactoryCommand(City targetFactory, EGameResourceType resourceType, int resourceQuantity, float timeToProduce,
        int price)
    {
        TargetCity = targetFactory;
        ResourceQuantity = resourceQuantity;
        ResourceType = resourceType;
        TimeToProduce = timeToProduce;
        Price = price;
        ElapsedTime = 0f;
        isDone = false;
    }
}

[Serializable]
public class Factory : MonoBehaviour
{
    public FactoryInventory FactoryInventory;

    public GameRecipe aspidosRecipe;
    public GameRecipe dolifrontRecipe;

    [HideInInspector] public FactoryCommand CurrentCommand;

    public float aspidosPrice = 1f;
    public float aspidosTimeToProduce = 1f;
    public float dolifrontPrice = 1f;
    public float dolifrontTimeToProduce = 1f;

    public float timeToShip = 0f;

    private float m_Delay;

    private GameManager m_GameManager;

    private float m_ElapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        FactoryInventory.Aspinium = new GameResource(EGameResourceType.Aspinium, 0f, 0f, 0);
        FactoryInventory.Dolinium = new GameResource(EGameResourceType.Dolinium, 0f, 0f, 0);
        FactoryInventory.Aspidos = new GameResource(EGameResourceType.Aspidos, aspidosTimeToProduce, aspidosPrice, 0);
        FactoryInventory.Dolifront =
            new GameResource(EGameResourceType.Dolifront, dolifrontTimeToProduce, dolifrontPrice, 0);

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

    public void AddCommandResources(int resourceQuantity, EGameResourceType resourceType)
    {
        switch (resourceType)
        {
            case EGameResourceType.Aspinium:
                FactoryInventory.Aspinium.quantity += resourceQuantity;
                break;
            case EGameResourceType.Dolinium:
                FactoryInventory.Dolinium.quantity += resourceQuantity;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
    }

    public void AddCommandResources(int aspiniumQuantity, int doliniumQuantity)
    {
        FactoryInventory.Dolinium.quantity += doliniumQuantity;
        FactoryInventory.Aspinium.quantity += aspiniumQuantity;
    }

    // recipeCount * (currentResource.timeToProduce),
    // (int) (recipeCount * (currentResource.price))
    public bool MakeCommand(City targetCity, EGameResourceType resourceType, int resourceCount)
    {
        GameResource currentResource;
        switch (resourceType)
        {
            case EGameResourceType.Aspidos:
                currentResource = FactoryInventory.Aspidos;
                break;
            case EGameResourceType.Dolifront:
                currentResource = FactoryInventory.Dolifront;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }

        float timeToProduce = resourceCount * (currentResource.timeToProduce);
        int price = (int) (resourceCount * (currentResource.price));

        if (price > m_GameManager.currentMoney)
            return false;

        m_GameManager.currentMoney -= price;

        CurrentCommand = new FactoryCommand(targetCity, currentResource.type, resourceCount, timeToProduce, price);;
        return true;
    }

    private void ProcessCommand()
    {
        if (CurrentCommand.ElapsedTime > CurrentCommand.TimeToProduce)
        {
            CurrentCommand.TargetCity.AddCommandResources(CurrentCommand.ResourceQuantity,
                CurrentCommand.ResourceType);
            CurrentCommand.isDone = true;
        }

        CurrentCommand.ElapsedTime += Time.deltaTime;
    }

    public int MaxProductionQuantity(GameRecipe recipe)
    {
        return (int) Mathf.Min(FactoryInventory.Aspinium.quantity / recipe.aspiniumQuantity,
            FactoryInventory.Dolinium.quantity / recipe.doliniumQuantity);
    }

    private void OnMouseDown()
    {
        GameObject.Find("UI").GetComponent<UIScript>().ActivateFactoryPanel(this);
    }
}