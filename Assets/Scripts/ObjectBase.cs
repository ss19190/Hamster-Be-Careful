using UnityEngine;

public abstract class ObjectBase : MonoBehaviour
{
    public int levelToUnlock;
    public abstract void OnPlayerHit(GameController controller);
}
