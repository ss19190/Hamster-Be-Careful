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
    public Text goalText;
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
    public string[] goals;

    [Header("Powerup Goal Settings")]
    public bool[] powerupsCollected = new bool[5];
    private bool hasCollectedAnything = false;

    public bool tntTaken;
    private bool logHitAfterTnt;


    [System.Serializable]
    public class LevelObstacleSet
    {
        public GameObject[] obstacles; // Obstacle set for a level
    }

    // Start is called before the first frame update
    void Start()
    {
        hasCollectedAnything = false;
        PlayerPrefs.SetInt("Obstacles taken", 0);
        heartCount = 2;
        timeSinceLastSpawn = 0f;
        distance = 0;
        timer = 0f;
        ball.SetActive(false);
        tnt.SetActive(false);
        PlayerPrefs.SetInt("Ball taken", 0);
        isLevelMode = PlayerPrefs.GetInt("isLevelMode", 0) == 1;
        tntTaken = false;
        logHitAfterTnt = false;
        Debug.Log(isLevelMode);
        if (isLevelMode)
        {
            levelModeLevel = PlayerPrefs.GetInt("LevelModeLevel", 1);
            LoadObjectsForLevel(levelModeLevel);
            goalText.gameObject.SetActive(true);
            goalText.text = "Goal: " + goals[levelModeLevel - 1];
        }
        else
        {
            currentLevelObjects = objectsToSpawn;// Default
            goalText.gameObject.SetActive(false);
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
        timeText.text = "Time: " + timer.ToString("F2"); 
        if (isLevelMode)
        {
            checkGoal(levelModeLevel);
        }
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
            if (collision.CompareTag("Log")) {
                Debug.Log("Log hit");
                if(tntTaken)
                {
                    logHitAfterTnt = true;
                }
            }

            ObjectBase obj = collision.GetComponent<ObjectBase>();
            if (obj != null)
            {
                Debug.Log("Object met: " + obj.name);
                obj.OnPlayerHit(this);

                // Check if it's a powerup
                Powerups powerup = obj as Powerups;
                if (powerup != null)
                {
                    PlayerPrefs.SetInt("Total Powerups", PlayerPrefs.GetInt("Total Powerups", 0) + 1);
                    hasCollectedAnything = true; 
                    int index = powerup.powerupType;
                    if (index >= 0 && index < powerupsCollected.Length)
                    {
                        powerupsCollected[index] = true;
                        Debug.Log("Collected powerup at index: " + index);

                        // Check if all powerups were collected
                        bool allCollected = true;
                        foreach (bool collected in powerupsCollected)
                        {
                            if (!collected)
                            {
                                allCollected = false;
                                break;
                            }
                        }

                        if (allCollected)
                        {
                            Debug.Log("All powerups collected!");
                            PlayerPrefs.SetInt("GoalReached", 1);
                            PlayerPrefs.SetFloat("Final Time", timer);
                            SceneManager.LoadScene("GameOverScene");
                        }
                    }
                }

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

    public void checkGoal(int level)
    {
        switch (level)
        {
            case 1:
                if (distance >= 1000f)
                {
                    PlayerPrefs.SetInt("GoalReached", 1);
                    PlayerPrefs.SetFloat("Final Time", (float)timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                else if (timer >= 30f)
                {
                    PlayerPrefs.SetInt("GoalReached", 0);
                    PlayerPrefs.SetFloat("Final Time", (float)timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                break;
            case 2:
                if(PlayerPrefs.GetInt("Obstacles taken") >= 10)
                {
                    PlayerPrefs.SetInt("GoalReached", 1);
                    PlayerPrefs.SetFloat("Final Time", (float)timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                break;
            case 3:
                bool allCollected = true;
                foreach (bool collected in powerupsCollected)
                {
                    if (!collected)
                    {
                        allCollected = false;
                        break;
                    }
                }

                if (allCollected)
                {
                    PlayerPrefs.SetInt("GoalReached", 1);
                    PlayerPrefs.SetFloat("Final Time", timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                break;
            case 4:
                if (hasCollectedAnything)
                {
                    PlayerPrefs.SetInt("GoalReached", 0);
                    PlayerPrefs.SetFloat("Final Time", timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                else if(distance >=1000f)
                {
                    PlayerPrefs.SetInt("GoalReached", 1);
                    PlayerPrefs.SetFloat("Final Time", timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                break;
            case 5:
                if (logHitAfterTnt)
                {
                    PlayerPrefs.SetInt("GoalReached", 1);
                    PlayerPrefs.SetFloat("Final Time", timer);
                    SceneManager.LoadScene("GameOverScene");
                }
                break;

            default:
                break;
        }
    }


}
