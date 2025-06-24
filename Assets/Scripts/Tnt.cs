using System.Collections;
using UnityEngine;

public class Tnt : Powerups 
{
    public Tnt()
    {
        powerupType = 4;
    }
    public override void OnPlayerHit(GameController controller)
    {
        controller.tntTaken = true; 
        PlayerPrefs.SetInt("TNT taken", 1);
        controller.tnt.SetActive(true);
    }
}
