using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEditor.SearchService;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Guy badGuy;
    [SerializeField] Transform playerTransform;
    [SerializeField] float timeToSpawn = 1;
    [SerializeField] int maxBadGuys = 10;
    [SerializeField] float spawnDistance = 20;
    int guysSpawned = 0;

    public static EnemySpawner singleton;

    void Awake(){
        if (singleton == null){
            singleton = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start(){
        StartCoroutine(SpawnAsteroidsRoutine());
    }

    void SpawnEnemy(){
        Vector3 spawnVector = playerTransform.position + RandomVector();
        Instantiate(badGuy, spawnVector, Quaternion.identity);
    }

    Vector3 RandomVector(){
        return new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f), 0).normalized * spawnDistance;
    }

    IEnumerator SpawnAsteroidsRoutine(){
        while(guysSpawned < maxBadGuys ){
            yield return new WaitForSeconds(timeToSpawn);
            SpawnEnemy();
            guysSpawned = CityManager.singleton.GetGuys().Count - 1;
        }
    }
}
