using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float timeSinceLastSpawn;
    public float minimalTimeForSpawn;
    public GameObject[] objectsToSpawn;
    public GameObject[] spawners;
    public Text distanceText;
    public float distance;
    public GameObject[] hearts;
    public int heartCount;
    public GameObject hamster;
    public float newSpeed;
    public Text timeText;
    public float timer;
    public GameObject tnt;
    public GameObject ball;

    [Header("Level Mode Settings")]
    public LevelObstacleSet[] levelObstacleSets;
    private GameObject[] currentLevelObjects;
    private int levelModeLevel = 1;
    bool isLevelMode;

    [System.Serializable]
    public class LevelObstacleSet
    {
        public GameObject[] obstacles; // Obstacle set for a level
    }

    // Start is called before the first frame update
    void Start()
    {
        heartCount = 2;
        timeSinceLastSpawn = 0f;
        distance = 0;
        timer = 0f;
        ball.SetActive(false);
        tnt.SetActive(false);
        PlayerPrefs.SetInt("Ball taken", 0);
        isLevelMode = PlayerPrefs.GetInt("isLevelMode", 0) == 1;
        Debug.Log(isLevelMode);
        if (isLevelMode)
        {
            levelModeLevel = PlayerPrefs.GetInt("LevelModeLevel", 1);
            LoadObjectsForLevel(levelModeLevel);
        }
        else
        {
            currentLevelObjects = objectsToSpawn;// Default
        }
    }

    // Update is called once per frame
    void Update()
    {
        minimalTimeForSpawn = Mathf.Clamp(5f / (newSpeed + 1f), 0.5f, 3f);


        if (timeSinceLastSpawn > minimalTimeForSpawn)
        {
            spawnObject(currentLevelObjects);
            timeSinceLastSpawn = 0;
        }
        timeSinceLastSpawn += Time.deltaTime;
        distanceText.text = "Distance: " + distance.ToString("F2");
        distance += newSpeed * Time.deltaTime;
        newSpeed += 0.1f * Time.deltaTime; // Increase speed over time 
        timer += Time.deltaTime;
        timeText.text = "Time: " + Mathf.FloorToInt(timer).ToString("F2"); 
    }

    void deserializeFromPlayerPrefs()
    {

    }

    void spawnObject(GameObject[] objectsToSpawn)
    {
        int i = Random.Range(0, 3);
        int j = Random.Range(0, objectsToSpawn.Length);
        GameObject newObject = Instantiate(objectsToSpawn[j], spawners[i].transform);
        newObject.GetComponent<ObjectMover>().speed = newSpeed;
    }

    public void objectMet(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Objects"))
        {
            ObjectBase obj = collision.GetComponent<ObjectBase>();
            if (obj != null)
            {
                Debug.Log("Object met: " + obj.name);
                obj.OnPlayerHit(this);
                Destroy(collision.gameObject);
            }
        }
    }

    IEnumerator RotateHamster()
    {
        yield return RotateOverTime(hamster.transform, 30f, 1f);
        yield return RotateOverTime(hamster.transform, -60f, 2f);
        yield return RotateOverTime(hamster.transform, 30f, 1f);
        newSpeed = 5f;

        ObjectMover[] obstacles = FindObjectsOfType<ObjectMover>(); foreach (ObjectMover obstacle in obstacles)
        {
            obstacle.speed = newSpeed;
        }
    }

    IEnumerator RotateOverTime(Transform target, float angle, float duration)
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

        target.eulerAngles = new Vector3(0, 0, endZ); // Ensure exact final value
    }
    void LoadObjectsForLevel(int level)
{
    int index = level - 1;

    if (index >= 0 && index < levelObstacleSets.Length)
    {
        currentLevelObjects = levelObstacleSets[index].obstacles;
        Debug.Log($"Loaded {currentLevelObjects.Length} objects for level {level}");
    }
    else
    {
        Debug.LogWarning($"Invalid level index: {index}, using default obstacles.");
        currentLevelObjects = objectsToSpawn;
    }
}

}
