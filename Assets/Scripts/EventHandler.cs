using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static GameObject winMenu;
    public static GameObject dieMenu;
    
    public static void GameOver()
    {
        dieMenu.SetActive(true);
    }
    
    private static void Victory()
    {
        winMenu.SetActive(true);
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

        if (dice.onHole)
        {
            dice.FallDown();
            return;
        }
        
        if (dice.Number > 2 && dice.onWater)
        {
            dice.Drown();
            return;
        }
        
        if (dice.Number >= 5 && dice.onCrack)
        {
            dice.DigHole(0);
        }
        
        if (dice.Number == 6 && !dice.onCrack)
        {
            dice.DigHole(1);
        }

        if (dice.Number > 3 && dice.onSlope)
        {
            dice.Landslide();
        }
    }
}
