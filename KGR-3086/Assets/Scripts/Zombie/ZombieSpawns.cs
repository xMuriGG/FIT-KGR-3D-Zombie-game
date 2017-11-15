using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawns : MonoBehaviour {
    public int round = 1;
    public int zombiesInRound=3;
    int zombiesSpawnInRound;
    public Transform[] zombieSpawnPoints;
    public GameObject zombie;
	// Use this for initialization
	void Awake () {
        zombiesSpawnInRound = 0;
        if (zombiesInRound > zombieSpawnPoints.Length - 1)
            zombiesInRound = zombieSpawnPoints.Length - 1;


        IEnumerable<int> list=UniqueRandom(0,zombieSpawnPoints.Length-1);
        IEnumerator enumerator = list.GetEnumerator();

        for (int i = 0; i < zombiesInRound; i++)
        {
            enumerator.MoveNext();
            SpawnZombies((int)enumerator.Current);
            
        }
	}

    private void SpawnZombies(int spawnIndex)
    {
        Transform randomSpawnPoint = zombieSpawnPoints[spawnIndex];
        GameObject z=(GameObject) Instantiate(zombie, randomSpawnPoint.position, randomSpawnPoint.rotation);
        GameObject p = GameObject.Find("Player");
        z.transform.GetComponent<ZombieScript>().player=p;
        z.transform.GetChild(3).GetComponent<OduzimanjeEnergije>().playerInformationManager = p;
        
        //Vector3 relativePos = randomSpawnPoint - z.transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //z.transform.rotation = rotation;
        //z.transform.Rotate(randomSpawnPoint);
        zombiesSpawnInRound++;
    }
    IEnumerable<int> UniqueRandom(int minInclusive, int maxInclusive)
    {
        List<int> candidates = new List<int>();
        for (int i = minInclusive; i <= maxInclusive; i++)
        {
            candidates.Add(i);
        }
        System.Random rnd = new System.Random();
        while (candidates.Count > 0)
        {
            int index = rnd.Next(candidates.Count);
            yield return candidates[index];
            candidates.RemoveAt(index);
        }
    }

    public int GetZombiesSpawnedInRound()
    {
        return zombiesSpawnInRound;
    }
}


