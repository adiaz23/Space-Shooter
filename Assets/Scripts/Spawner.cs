using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefab;
    [SerializeField] private TextMeshProUGUI waveText;

    void Start()
    {
       StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies(){

        for (int level = 0; level < 5; level++){

            for(int enemyWave = 0; enemyWave < 3; enemyWave++){
                waveText.text = $"Level {level+1} - Wave {enemyWave+1}";
                yield return new WaitForSeconds(2f);
                waveText.text = "";

                for(int enemyNumber = 0; enemyNumber < 10; enemyNumber++){
                    GenerateEnemies(); 
                    yield return new WaitForSeconds(0.5f);
                }

                yield return new WaitForSeconds(3f);
            }

            yield return new WaitForSeconds(4f);
        }
    }

    void GenerateEnemies(){
        float posittionY = Random.Range(-4.10f, 4.10f);
        Vector2 randomPoint = new(transform.position.x, posittionY);
        int randomEnemy = Random.Range(0, 2);
        Instantiate(enemyPrefab[randomEnemy], randomPoint, Quaternion.identity);
    }
}
