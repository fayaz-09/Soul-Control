using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform Player;
    public int MaxNumOfSpawn = 5;
    [SerializeField] private int totalSpawn;
    public int numOfBtSword = 1;
    public int numOfFSMSword = 1;
    public int numOfGOAPSword = 1;
    public int numOfHFSMSword = 1;
    public int numOfBtMage = 1;
    public int numOfFSMMage = 1;
    public int numOfGOAPMage = 1;
    public int numOfHFSMMage = 1;
    public float spawnDelay = 1f;
    public int totalEnemies;
    public List<Enemy> EnemyPrefabs = new List<Enemy>();
    public spawnMethod EnSpawnMethod = spawnMethod.Selection;

    [SerializeField] private Camera Camera;
    [SerializeField] private Canvas healthBarCanvas;

    private NavMeshTriangulation Triangulation;
    private Dictionary<int, ObjectPool> enemyObjectPool = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        for(int i = 0; i < EnemyPrefabs.Count; i++)
        {
            enemyObjectPool.Add(i, ObjectPool.createInstance(EnemyPrefabs[i], MaxNumOfSpawn));
        }

        
    }

    private void Start()
    {
        Triangulation = NavMesh.CalculateTriangulation();
        totalSpawn = numOfBtSword + numOfFSMSword + numOfGOAPSword + numOfHFSMSword + numOfBtMage;
        //StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        totalSpawn = numOfBtSword + numOfFSMSword + numOfGOAPSword + numOfHFSMSword + numOfBtMage;
        if(totalSpawn > 0 && Time.timeScale > 0f)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        int spawnedEn = 0;

        while(spawnedEn < totalSpawn)
        {
            if(EnSpawnMethod == spawnMethod.ordered)
            {
                SpawnOrderedEnemy(spawnedEn);
            }
            else if (EnSpawnMethod == spawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            else if(EnSpawnMethod == spawnMethod.Selection)
            {
                SpawnSelectedEnemy();
            }
            spawnedEn++;

            yield return wait;
        }
    }

    private void SpawnOrderedEnemy(int SpawnedEnemies)
    {
        int spawnIndex = SpawnedEnemies % EnemyPrefabs.Count;
        SpawnEnemy(spawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        SpawnEnemy(Random.Range(0, EnemyPrefabs.Count));
    }

    private void SpawnSelectedEnemy()
    {
        if(numOfBtSword > 0)
        {
            SpawnEnemy(0);
            numOfBtSword--;
        }
        else if(numOfFSMSword > 0)
        {
            SpawnEnemy(1);
            numOfFSMSword--;
        }
        else if (numOfGOAPSword > 0)
        {
            SpawnEnemy(2);
            numOfGOAPSword--;
        }
        else if (numOfHFSMSword > 0)
        {
            SpawnEnemy(3);
            numOfHFSMSword--;
        }
        else if (numOfBtMage > 0)
        {
            SpawnEnemy(4);
            numOfBtMage--;
        }
        else if (numOfFSMMage > 0)
        {
            SpawnEnemy(5);
            numOfFSMMage--;
        }
        else if (numOfGOAPMage > 0)
        {
            SpawnEnemy(6);
            numOfGOAPMage--;
        }
        else if (numOfHFSMMage > 0)
        {
            SpawnEnemy(7);
            numOfHFSMMage--;
        }
    }

    private void SpawnEnemy(int spawnIndex)
    {
        PoolableObject poolableObject = enemyObjectPool[spawnIndex].GetObject();
        Debug.Log($"index:  {spawnIndex}");
        if (poolableObject != null)
        {
            Enemy enemy = poolableObject.GetComponent<Enemy>();
            
            int VertexIndex = Random.Range(0, Triangulation.vertices.Length);

            NavMeshHit hit;
            if(NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out hit, 10f, 1))
            {
                Debug.Log($"place agent on nav mesh. {hit.position}");
                enemy.m_EnemyNavAgent.Warp(hit.position);
                enemy.m_EnemyNavAgent.enabled = true;
                enemy.setUpHealthBar(healthBarCanvas, Camera);
            }
            else
            {
                Debug.Log($"Unable to place agent on nav mesh. {Triangulation.vertices[VertexIndex]}");
            }

        }
        else
        {
            Debug.Log($"Unable to spawn enemy of type {spawnIndex} from pool");
        }
    }

    public enum spawnMethod
    {
        ordered,
        Random,
        Selection
    }

}
