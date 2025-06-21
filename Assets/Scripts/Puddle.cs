using UnityEngine;
using System.Collections;

public class Puddle : Obstacle
{
    public override void OnPlayerHit(GameController controller)
    {
        if(PlayerPrefs.GetInt("Ball taken") == 0)
            controller.StartCoroutine(HandlePuddle(controller));
    }

    private IEnumerator HandlePuddle(GameController controller)
    {
        controller.newSpeed = 1f;

        foreach (ObjectMover obstacle in Object.FindObjectsOfType<ObjectMover>())
            obstacle.speed = controller.newSpeed;

        // Hamster animation
        yield return RotateOverTime(controller.hamster.transform, 30f, 1f);
        yield return RotateOverTime(controller.hamster.transform, -60f, 2f);
        yield return RotateOverTime(controller.hamster.transform, 30f, 1f);

        controller.newSpeed = 5f;

        foreach (ObjectMover obstacle in Object.FindObjectsOfType<ObjectMover>())
            obstacle.speed = controller.newSpeed;
    }

    private IEnumerator RotateOverTime(Transform target, float angle, float duration)
    {
        float elapsed = 0f;
        float startZ = target.eulerAngles.z;
        float endZ = startZ + angle;

        if (endZ > 360f) endZ -= 360f;
        if (endZ < 0f) endZ += 360f;

        while (elapsed < duration)
        {
            float z = Mathf.LerpAngle(startZ, endZ, elapsed / duration);
            target.eulerAngles = new Vector3(0, 0, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.eulerAngles = new Vector3(0, 0, endZ);
    }
}
