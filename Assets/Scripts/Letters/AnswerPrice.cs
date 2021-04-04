using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerPrice : Answer
{
    private int Price;

    public override bool CheckCondition()
    {
        if (DeathStat.Money >= Price)
            return true;
        else
            return false;
    }

    public AnswerPrice() : base()
    {

    }

    public AnswerPrice(string text, int price) : base(text)
    {
        Price = price;
    }
}
