using System.Collections;
using UnityEngine;

public class Tnt : Powerups 
{
    public override void OnPlayerHit(GameController controller)
    {
        PlayerPrefs.SetInt("TNT taken", 1);
        controller.tnt.SetActive(true);
    }
}
