using UnityEngine;
using System.Collections;

public class PlayerInformations {

    public int startingHealth ;
    public int currentHealth ;
    public string name;

    public PlayerInformations()
    {
        startingHealth = 1000;
        currentHealth = 1000;
        name = "Player";
    }
    public PlayerInformations(int startingHealth, string name)
    {
        this.startingHealth = startingHealth;
        this.currentHealth = 1000;
        this.name = name;
    }

   
}
