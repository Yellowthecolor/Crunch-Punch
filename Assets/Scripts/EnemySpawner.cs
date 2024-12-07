using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    // [SerializeField] Guy badGuy;
    [SerializeField] Transform playerTransform;
    [SerializeField] float timeToSpawn = 1;
    [SerializeField] float spawnDistance = 20;

    [SerializeField] List<GameObject> pooledBadGuys;
    [SerializeField] GameObject badGuyToPool;
    [SerializeField] int poolSize;


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
        pooledBadGuys = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < poolSize; i++){
            tmp = Instantiate(badGuyToPool);
            tmp.SetActive(false);
            pooledBadGuys.Add(tmp);
        }

        StartCoroutine(SpawnEnemiesRoutine());
    }

    public GameObject GetPooledBadGuy(){
        for(int i = 0; i < poolSize; i++){
            if(!pooledBadGuys[i].activeInHierarchy){
                return pooledBadGuys[i];
            }
        }
        return null;
    }

    void SpawnEnemy(){
        Vector3 spawnVector = playerTransform.position + RandomVector();
        // Instantiate(badGuy, spawnVector, Quaternion.identity);
        GameObject badGuy = EnemySpawner.singleton.GetPooledBadGuy();
        if (badGuy != null){
            badGuy.transform.position = spawnVector;
            badGuy.transform.rotation = Quaternion.identity;
            badGuy.SetActive(true);
        }
    }

    Vector3 RandomVector(){
        return new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f), 0).normalized * spawnDistance;
    }

    public void DecreaseGuysSpawned(){
        guysSpawned--;
    }

    IEnumerator SpawnEnemiesRoutine(){
        while(!CityManager.singleton.GetIsGameOver()){
            yield return new WaitForSeconds(timeToSpawn);
            if (guysSpawned < poolSize){
                SpawnEnemy();
                guysSpawned++;
            }
        }
    }
}
