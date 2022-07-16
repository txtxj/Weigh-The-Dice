using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private static void GameOver()
    {
        Debug.Log("Game Over!");
    }
    
    private static void Victory()
    {
        Debug.Log("Victory!");
    }
    
    public static void Solve(Dice dice)
    {
        dice.onWater = MapInfo.IsWater(dice.pos.x, dice.pos.y);
        dice.onSlope = MapInfo.IsSlope(dice.pos.x, dice.pos.y);
        dice.onCrack = MapInfo.IsCrack(dice.pos.x, dice.pos.y);
        dice.onHole = MapInfo.IsHole(dice.pos.x, dice.pos.y);

        if (MapInfo.IsTarget(dice.pos.x, dice.pos.y))
        {
            Victory();
            return;
        }

        if (dice.digFlag)
        {
            dice.digFlag = false;
            dice.DigHole(2);
        }

        if (dice.onHole)
        {
            dice.FallDown();
            GameOver();
            return;
        }
        
        if (dice.Number > 2 && dice.onWater)
        {
            dice.Drown();
            GameOver();
            return;
        }

        if (dice.Number > 3 && dice.onSlope)
        {
            dice.Landslide();
        }

        if (dice.Number >= 5 && dice.onCrack)
        {
            dice.DigHole(0);
        }
        
        if (dice.Number == 6 && !dice.onCrack)
        {
            dice.DigHole(1);
        }
    }
}
