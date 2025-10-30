using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;
    static private Dictionary<eWeaponType, WeaponDefinition> WEAP_DICT;

    [Header("Inscribed")]
    public bool spawnEnemies = true;
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    public float gameRestartDelay = 2;
    public GameObject prefabPowerUp;
    public GameManager gameManager;
    public WeaponDefinition[] weaponDefinitions;
    public eWeaponType[] powerUpFrequency = new eWeaponType[]
                                        {
                                            eWeaponType.blaster, eWeaponType.blaster,
                                            eWeaponType.spread, eWeaponType.shield
                                        };

    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        S.gameManager.gameOverUI.SetActive(false);

        bndCheck = GetComponent<BoundsCheck>();
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);

        // A generic Dictionary with eWeaponType as the key
        WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy()
    {
        // If spawnEnemies is false, skip to the next invoke of SpawnEnemy()
        if (!spawnEnemies)
        {
            Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
            return;
        }

        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //Position the enemy above the screen with random position
        float enemyInset = enemyInsetDefault;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyInset;
        float xMax = bndCheck.camWidth - enemyInset;

        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyInset;
        go.transform.position = pos;

        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    /* void DelayedRestart()
    {
        // Invoke the Restart() method in gameRestartDelay seconds
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        // Reload __Scene_0 to restart the game
        SceneManager.LoadScene("__Scene_0");
        HighScoreManager.S.ResetScore();
    }  */

    static public void HERO_DIED()
    {
        //S.DelayedRestart();
        S.Invoke(nameof(TriggerGameOver), S.gameRestartDelay);
    }

    void TriggerGameOver()
    {
        gameManager.GameOver();
    }

    static public WeaponDefinition GET_WEAPON_DEFINITION(eWeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }

        // If no entry of the correct type exists in WEAP_DICT, return a new WeaponDefinition with a type of eWeaponType.none (the default value)
        return (new WeaponDefinition());
    }

static public void SHIP_DESTROYED(Enemy e)
    {
        int points = 0;

        if (e is Enemy_0) points = 50;
        if (e is Enemy_1) points = 100;
        if (e is Enemy_2) points = 150;
        if (e is Enemy_3) points = 250;
        if (e is Enemy_4) points = 400;

        HighScoreManager.S.AddScore(points);

        if (Random.value <= e.powerUpDropChance)
         {
                int ndx = Random.Range(0, S.powerUpFrequency.Length);
                eWeaponType pUpType = S.powerUpFrequency[ndx];

                GameObject go = Instantiate<GameObject>(S.prefabPowerUp);
                PowerUp pUp = go.GetComponent<PowerUp>();
                pUp.SetType(pUpType);

                pUp.transform.position = e.transform.position;
            }
        }
    }
