using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Level001MenuScript : MonoBehaviour {
    public Text brojacOciscenihZombija;
    ZombieSpawns zombieInfo;
    int ZombiesSpawnedInRound;
    int ZombiesStillAlive;

    void Start()
    {
        zombieInfo = GameObject.Find("Spawns").GetComponent<ZombieSpawns>();
        ZombiesSpawnedInRound = zombieInfo.GetZombiesSpawnedInRound();
        ZombiesStillAlive = ZombiesSpawnedInRound;

        brojacOciscenihZombija.text = ZombiesStillAlive.ToString() + "/" + ZombiesSpawnedInRound;
    }

    public void ZombieDeathCounter()
    {
        --ZombiesStillAlive;
        brojacOciscenihZombija.text = ZombiesStillAlive.ToString() + "/" + ZombiesSpawnedInRound;

        if (ZombiesStillAlive <= 0)
        {
            gameObject.GetComponent<menuScript>().PrikaziUIPanelElemente();
            gameObject.transform.FindChild("Success").gameObject.SetActive(true);
        }
    }

}
