using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour {

    public int enemyHP = 50;

    public void ReducirajPoene(int HP)
    {
        enemyHP -= HP;
    }
	
	// Update is called once per frame
	void Update () {
        if (enemyHP <= 0)
            DestroyObject(gameObject);
	}
}
