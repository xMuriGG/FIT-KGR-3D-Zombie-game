using UnityEngine;
using System.Collections;
public class OduzimanjeEnergije : MonoBehaviour {

    public GameObject playerInformationManager;
    PlayerInformationsMenager manager;

    void Start()
    {
        manager = playerInformationManager.GetComponent<PlayerInformationsMenager>();
    }
    void OnTriggerStay(Collider col)
    {
        manager.TakeDamage(5);
        //PlayerInformationsMenager.player.currentHealth -= 5;
    }
}
