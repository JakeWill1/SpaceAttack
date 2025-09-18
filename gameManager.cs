using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public int currentLevel = 1;
    public GameObject enemyBig;
    public Text levelText;
    public bool noEnemies = true;
    public float timer = 0.0f;
    public float textTime = 3.0f;
    public string finalLevel;

    public List<GameObject> pickups = new List<GameObject>();
    public float releaseTimer = 0.0f;
    public float minReleaseTime, maxReleaseTime;
    public float pickupTime;
    public bool pickupOut = false;

    public bool pickupActive = false;
    float pickupTimer = 0.0f;
    public GameObject powerbar;
    public GameObject player;

    public bool fromGame = false;
    public Text score1, score2, score3, score4, score5, score6, score7, score8, score9, score10;
    public Text level1, level2, level3, level4, level5, level6, level7, level8, level9, level10;
    public bool newHighScore;
    public int finalScore;
    public int highlightText;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        pickupTime = Random.Range(minReleaseTime, maxReleaseTime);

        if (PlayerPrefs.HasKey("HighScore_1"))
        {
            Debug.Log("Highscore exists");
        }
        else
        {
            Debug.Log("Highscore exists");
            PlayerPrefs.SetInt("HighScore_1", 0);
            PlayerPrefs.SetString("GameLevel_1", "no level");
            PlayerPrefs.SetInt("HighScore_2", 0);
            PlayerPrefs.SetString("GameLevel_2", "no level");
            PlayerPrefs.SetInt("HighScore_3", 0);
            PlayerPrefs.SetString("GameLevel_3", "no level");
            PlayerPrefs.SetInt("HighScore_4", 0);
            PlayerPrefs.SetString("GameLevel_4", "no level");
            PlayerPrefs.SetInt("HighScore_5", 0);
            PlayerPrefs.SetString("GameLevel_5", "no level");
            PlayerPrefs.SetInt("HighScore_6", 0);
            PlayerPrefs.SetString("GameLevel_6", "no level");
            PlayerPrefs.SetInt("HighScore_7", 0);
            PlayerPrefs.SetString("GameLevel_7", "no level");
            PlayerPrefs.SetInt("HighScore_8", 0);
            PlayerPrefs.SetString("GameLevel_8", "no level");
            PlayerPrefs.SetInt("HighScore_9", 0);
            PlayerPrefs.SetString("GameLevel_9", "no level");
            PlayerPrefs.SetInt("HighScore_10", 0);
            PlayerPrefs.SetString("GameLevel_10", "no level");

            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "s2_menu")
        {
            ResetGame();
        }
            if (SceneManager.GetActiveScene().name == "s4_game")
        {
            if(levelText == null)
            {
                levelText = GameObject.Find("Text-Level Display").GetComponent<Text>();
                ResetGame();
            }
            if(GameObject.FindWithTag("EnemyBig") == null)
            {
                levelText.text = "Level: " + currentLevel;
                timer += Time.deltaTime;
                if(timer >= textTime)
                {
                    timer = 0.0f;
                    for(int i=0; i< currentLevel; i++)
                    {
                        GameObject tempEnemy = Instantiate(enemyBig, transform.position, Quaternion.identity);
                        int temp = i * 3;
                        tempEnemy.GetComponent<SpriteRenderer>().sortingOrder = temp;
                        tempEnemy.transform.Find("numbers").GetComponent<SpriteRenderer>().sortingOrder = temp + 1;
                    }
                    finalLevel = "Level: " + currentLevel;
                    currentLevel++;
                    levelText.text = "";
                }
            }
            if (pickupOut == false)
            {
                releaseTimer += Time.deltaTime;
                if (releaseTimer >= pickupTime)
                {
                    releaseTimer = 0.0f;
                    pickupTime = Random.Range(minReleaseTime, maxReleaseTime);
                    pickupOut = true;
                    Instantiate(pickups[Random.Range(0, pickups.Count)]);
                }
            }
            if(powerbar == null)
            {
                powerbar = GameObject.Find("Image - PowerBar");
            }
            if (player == null)
            {
                player = GameObject.Find("player");
            }
            if (pickupActive)
            {
                pickupTimer += Time.deltaTime;
                powerbar.transform.localScale = new Vector3(Mathf.Lerp(1.0f, 0.0f, pickupTimer / 3f), 1, 1);
                if(powerbar.transform.localScale.x <= 0.0f)
                {
                    pickupActive = false;
                    pickupTimer = 0.0f;
                    player.GetComponent<player>().TurnOffPickup();
                }
            }
        }
        if(SceneManager.GetActiveScene().name == "s5_scores")
        {
            if (score1 == null)
            {
                score1 = GameObject.Find("Text-Score (1)").GetComponent<Text>();
                score2 = GameObject.Find("Text-Score (2)").GetComponent<Text>();
                score3 = GameObject.Find("Text-Score (3)").GetComponent<Text>();
                score4 = GameObject.Find("Text-Score (4)").GetComponent<Text>();
                score5 = GameObject.Find("Text-Score (5)").GetComponent<Text>();
                score6 = GameObject.Find("Text-Score (6)").GetComponent<Text>();
                score7 = GameObject.Find("Text-Score (7)").GetComponent<Text>();
                score8 = GameObject.Find("Text-Score (8)").GetComponent<Text>();
                score9 = GameObject.Find("Text-Score (9)").GetComponent<Text>();
                score10 = GameObject.Find("Text-Score (10)").GetComponent<Text>();

                level1 = GameObject.Find("Text-Level (1)").GetComponent<Text>();
                level2 = GameObject.Find("Text-Level (2)").GetComponent<Text>();
                level3 = GameObject.Find("Text-Level (3)").GetComponent<Text>();
                level4 = GameObject.Find("Text-Level (4)").GetComponent<Text>();
                level5 = GameObject.Find("Text-Level (5)").GetComponent<Text>();
                level6 = GameObject.Find("Text-Level (6)").GetComponent<Text>();
                level7 = GameObject.Find("Text-Level (7)").GetComponent<Text>();
                level8 = GameObject.Find("Text-Level (8)").GetComponent<Text>();
                level9 = GameObject.Find("Text-Level (9)").GetComponent<Text>();
                level10 = GameObject.Find("Text-Level (10)").GetComponent<Text>();
            }

            if(fromGame == true)
            {
                if(newHighScore == false)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        if (finalScore > PlayerPrefs.GetInt("HighScore_" + i))
                        {
                            for(int x = 5; x > i; x--)
                            {
                                int value = PlayerPrefs.GetInt("HighScore_" + (x - 1));
                                string value2 = PlayerPrefs.GetString("GameLevel_" + (x-1));
                                PlayerPrefs.SetInt("HighScore_" + x, value);
                                PlayerPrefs.SetString("GameLevel_" + x, value2);
                            }
                            PlayerPrefs.SetInt("HighScore_" + i, finalScore);
                            PlayerPrefs.SetString("GameLevel_" + i, finalLevel);
                            highlightText = i;
                            break;
                        }
                    }
                    newHighScore = true;
                    switch (highlightText)
                    {
                        case 1:
                            score1.color = Color.blue;
                            level1.color = Color.blue;
                            GameObject.Find("Text-Rank (1)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 2:
                            score2.color = Color.blue;
                            level2.color = Color.blue;
                            GameObject.Find("Text-Rank (2)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 3:
                            score3.color = Color.blue;
                            level3.color = Color.blue;
                            GameObject.Find("Text-Rank (3)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 4:
                            score4.color = Color.blue;
                            level4.color = Color.blue;
                            GameObject.Find("Text-Rank (4)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 5:
                            score5.color = Color.blue;
                            level5.color = Color.blue;
                            GameObject.Find("Text-Rank (5)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 6:
                            score6.color = Color.blue;
                            level6.color = Color.blue;
                            GameObject.Find("Text-Rank (6)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 7:
                            score7.color = Color.blue;
                            level7.color = Color.blue;
                            GameObject.Find("Text-Rank (7)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 8:
                            score8.color = Color.blue;
                            level8.color = Color.blue;
                            GameObject.Find("Text-Rank (8)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 9:
                            score9.color = Color.blue;
                            level9.color = Color.blue;
                            GameObject.Find("Text-Rank (9)").GetComponent<Text>().color = Color.blue;
                            break;
                        case 10:
                            score10.color = Color.blue;
                            level10.color = Color.blue;
                            GameObject.Find("Text-Rank (10)").GetComponent<Text>().color = Color.blue;
                            break;
                        default:
                            break;
                    }
                }
            }
            score1.text = PlayerPrefs.GetInt("HighScore_1").ToString();
            level1.text = PlayerPrefs.GetString("GameLevel_1");
            score2.text = PlayerPrefs.GetInt("HighScore_2").ToString();
            level2.text = PlayerPrefs.GetString("GameLevel_2");
            score3.text = PlayerPrefs.GetInt("HighScore_3").ToString();
            level3.text = PlayerPrefs.GetString("GameLevel_3");
            score4.text = PlayerPrefs.GetInt("HighScore_4").ToString();
            level4.text = PlayerPrefs.GetString("GameLevel_4");
            score5.text = PlayerPrefs.GetInt("HighScore_5").ToString();
            level5.text = PlayerPrefs.GetString("GameLevel_5");
            score6.text = PlayerPrefs.GetInt("HighScore_6").ToString();
            level6.text = PlayerPrefs.GetString("GameLevel_6");
            score7.text = PlayerPrefs.GetInt("HighScore_7").ToString();
            level7.text = PlayerPrefs.GetString("GameLevel_7");
            score8.text = PlayerPrefs.GetInt("HighScore_8").ToString();
            level8.text = PlayerPrefs.GetString("GameLevel_8");
            score9.text = PlayerPrefs.GetInt("HighScore_9").ToString();
            level9.text = PlayerPrefs.GetString("GameLevel_9");
            score10.text = PlayerPrefs.GetInt("HighScore_10").ToString();
            level10.text = PlayerPrefs.GetString("GameLevel_10");
        }
    }
    public void PickupReset()
    {
        pickupOut = false;
        releaseTimer = 0.0f;
        pickupTime = Random.Range(minReleaseTime, maxReleaseTime);
    }
    public void PickupCaptured()
    {
        pickupActive = true;
        PickupReset();
    }
    public void ResetGame()
    {
        currentLevel = 1;
        timer = 0.0f;
        releaseTimer = 0.0f;
        pickupOut = false;
        pickupActive = false;
        newHighScore = false;
        highlightText = 0;
        fromGame = false;

    }
}
