using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject Enemy;
    public Player Player;
    public float SpawnTime = 3f;            
    public int MaximumActiveSpawns = 7;
    public Transform[] SpawnPoints;

    public int ActiveSpawns = 0;         
    
    void Start () {
        InvokeRepeating("Spawn", SpawnTime, SpawnTime);
    }
	
	void Spawn() {
        if (Player.Health <= 0f)
        {
            CancelInvoke("Spawn");
            return;
        }

	    if (ActiveSpawns >= MaximumActiveSpawns)
	    {
	        return;
	    }

        ActiveSpawns++;
        
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        
        var enemy = (GameObject)Instantiate(Enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
        enemy.SetActive(true);
    }
}
