using System.Collections;
using UnityEngine;

public class Ball : Powerups
{
    public override void OnPlayerHit(GameController controller)
    {
        controller.StartCoroutine(HandleBall(controller));
    }

    private IEnumerator HandleBall(GameController controller)
    {
        controller.ball.SetActive(true);
        PlayerPrefs.SetInt("Ball taken", 1);

        controller.newSpeed += 10f;

        foreach (ObjectMover obstacle in Object.FindObjectsOfType<ObjectMover>())
            obstacle.speed = controller.newSpeed;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        controller.newSpeed -= 10f;

        foreach (ObjectMover obstacle in Object.FindObjectsOfType<ObjectMover>())
            obstacle.speed = controller.newSpeed;
            
        PlayerPrefs.SetInt("Ball taken", 0);
        controller.ball.SetActive(false); // optional: hide the ball again
    }
}
