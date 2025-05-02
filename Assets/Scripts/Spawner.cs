using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefab;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Boss bossPrefab;

    void Start()
    {
       StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies(){
        int level = 0;
        int maxLevel = 5;
        int maxWaves = 3;
        int maxEnemyNumber = 10;
        for (int levels = 0; levels < maxLevel; levels++){
            level = levels + 1;
            for(int enemyWave = 0; enemyWave < maxWaves; enemyWave++){
                waveText.text = $"Level {level} - Wave {enemyWave+1}";
                yield return new WaitForSeconds(2f);
                waveText.text = "";

                for(int enemyNumber = 0; enemyNumber < maxEnemyNumber; enemyNumber++){
                    GenerateEnemies(); 
                    yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitForSeconds(3f);
            }

            yield return new WaitForSeconds(4f);
        }
        level += 1;
        if (level == maxLevel+1){
            bossPrefab.gameObject.SetActive(true);
            waveText.text = $"Final Level";
            yield return new WaitForSeconds(2f);
            waveText.text = "";
        }
    }

    void GenerateEnemies(){
        int randomEnemy = Random.Range(0, enemyPrefab.Length);
        Enemy selectedEnemy = enemyPrefab[randomEnemy];

        float enemyOffset = selectedEnemy.GetComponent<BoxCollider2D>().bounds.extents.y;
        float verticalLimit = Camera.main.orthographicSize;
        
        float positionY = Random.Range(-verticalLimit + enemyOffset, verticalLimit - enemyOffset);
        Vector2 randomPoint = new(transform.position.x, positionY);
       
        Instantiate(selectedEnemy, randomPoint, Quaternion.identity);
    }
}
