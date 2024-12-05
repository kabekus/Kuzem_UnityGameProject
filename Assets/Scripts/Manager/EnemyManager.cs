using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Player player;
    public GameDirector gameDirector;
    public UraniumManager uraniumManager;

    public Enemy enemyPrefab;
    public Enemy fastEnemyPrefab;
    public Enemy shootingEnemyPrefab;
    public Enemy bossEnemyPrefab;

    public float enemyYSpacing;

    private int _spawnedEnemyCount;

    private Coroutine _enemyGenerationCoroutine;

    public List<Enemy> _enemies = new List<Enemy>();

    public void RestartEnemyManager()
    {
        DeleteEnemies();
        if (_enemyGenerationCoroutine != null)
        {
            StopCoroutine(_enemyGenerationCoroutine);
        }
        _enemyGenerationCoroutine = StartCoroutine(EnemyGenerationCoroutine());
        _spawnedEnemyCount = 0;
    }

    private void DeleteEnemies()
    {
        foreach (Enemy e in _enemies) 
        {
            Destroy(e.gameObject);
        }
        _enemies.Clear();
    }

    IEnumerator EnemyGenerationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f + Random.Range(0,2f));
            var enemyCountBonus = (gameDirector.levelNo - 1) * 5;
            enemyCountBonus = Mathf.Min(enemyCountBonus, 95);
            if (_spawnedEnemyCount < 5 + enemyCountBonus)
            {
                if (Random.value < .75f)
                {
                    SpawnEnemy();
                }
                else
                {
                    SpawnTwoEnemies();
                }
            }
            else
            {
                yield return new WaitForSeconds(4);
                SpawnBoss();
                break;
            }            
        }
    }
    void SpawnEnemy()
    {
        var selectedEnemyPrefab = enemyPrefab;
        var randomizer = Random.value;
        if (gameDirector.levelNo > 2 && randomizer < .33f)
        {
            selectedEnemyPrefab = fastEnemyPrefab;
        }
        else if (gameDirector.levelNo > 3 && randomizer < .66f)
        {
            selectedEnemyPrefab = shootingEnemyPrefab;
        }
        var newEnemy = Instantiate(selectedEnemyPrefab);
        var enemyXPos = Random.Range(-2.2f, 2.2f);
        var enemyYPos = 5 * enemyYSpacing;
        newEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        _spawnedEnemyCount++;
        _enemies.Add(newEnemy);
        newEnemy.StartEnemy(player);
    }

    void SpawnTwoEnemies()
    {
        var newEnemy = Instantiate(enemyPrefab);
        var enemyXPos = Random.Range(1f, 2.2f);
        var enemyYPos = 5 * enemyYSpacing;
        newEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        _spawnedEnemyCount++;
        _enemies.Add(newEnemy);
        newEnemy.StartEnemy(player);

        var newEnemy2 = Instantiate(enemyPrefab);
        var enemyXPos2 = Random.Range(-1f, -2.2f);
        var enemyYPos2 = 5 * enemyYSpacing;
        newEnemy2.transform.position = new Vector3(enemyXPos2, enemyYPos2, 0);
        _spawnedEnemyCount++;
        _enemies.Add(newEnemy2);
        newEnemy2.StartEnemy(player);
    }

    void SpawnBoss()
    {
        var newEnemy = Instantiate(bossEnemyPrefab);
        var enemyXPos = Random.Range(-2f, 2f);
        var enemyYPos = 5 * enemyYSpacing;
        newEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        _spawnedEnemyCount++;
        _enemies.Add(newEnemy);
        newEnemy.StartEnemy(player);
    }
}
