using System.Collections;
using TMPro;
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
        float posittionY = Random.Range(-4.10f, 4.10f);
        Vector2 randomPoint = new(transform.position.x, posittionY);
        int randomEnemy = Random.Range(0, 2);
        Instantiate(enemyPrefab[randomEnemy], randomPoint, Quaternion.identity);
    }
}
