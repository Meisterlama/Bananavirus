using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameResourceType
{
    Aspinium = 0,
    Dolinium,
    Aspidos,
    Dolifront,
}

[Serializable]
public struct GameResource
{
    public GameResource(EGameResourceType type, float timeToProduce = 0f, float price = 0f, int quantity = 0)
    {
        this.type = type;
        this.timeToProduce = timeToProduce;
        this.price = price;
        this.quantity = quantity;
    }

    public EGameResourceType type;
    public float timeToProduce;
    public float price;
    public int quantity;

}

[Serializable]
public struct GameRecipe
{
    public GameRecipe(float aspiniumQuantity = 1, float doliniumQuantity = 1)
    {
        this.aspiniumQuantity = aspiniumQuantity;
        this.doliniumQuantity = doliniumQuantity;
    }

    public float aspiniumQuantity;
    public float doliniumQuantity;
}