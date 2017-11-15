using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerInformationsMenager : MonoBehaviour {

    public static PlayerInformations player;
    public Slider healtSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public AudioClip takingDamage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(0.493f, 0.022f, 0.022f,0.585f);
    
    Animation deathAnimation;
    FirstPersonController playerController;
    bool isDamaged=false;
    bool isDead=false;
    bool canBeDamaged = true;

    float regenerationCD = 0.0f;
    bool isWounded = false;

	// Use this for initialization
	void Start () {
        player = new PlayerInformations(1000,"Muri");
        playerController = gameObject.GetComponent<FirstPersonController>();
        deathAnimation = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead)
            return;

            if(isWounded)
            {
                if (regenerationCD < 2.0f)
                    regenerationCD += Time.deltaTime;
                else if (player.currentHealth >= player.startingHealth)
                {
                    player.currentHealth = player.startingHealth;
                    isWounded = false;
                }
                else
                    player.currentHealth += 5; ;
            }

        if (isDamaged && canBeDamaged)
        {
            damageImage.color = flashColour;
            AudioSource.PlayClipAtPoint(takingDamage, transform.position);
            canBeDamaged = false;
            StartCoroutine(WaitForSeconds());
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            
        }
        isDamaged = false;
        
        healtSlider.value = player.currentHealth;
	}

    public void TakeDamage(int damage)
    {
        if (player.currentHealth > 0)
        {
            player.currentHealth -= damage;
            isDamaged = true;
            
            regenerationCD = 0.0f;
            isWounded = true;
        }
        else
            Death();     
    }

    private void Death()
    {
        if (isDead)
            return; 
        isDead = true;
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
        deathAnimation.Play("PlayerDeathAnimation");
        playerController.enabled = false;
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(1);
        canBeDamaged = true;
    }
    //void OnGUI()
    //{
    //    GUIStyle sty = new GUIStyle();
    //    sty.fontStyle = FontStyle.Bold;
    //    sty.fontSize = 20;
    //    sty.normal.textColor = Color.cyan;

    //    GUI.contentColor = Color.yellow;

    //    GUI.Label(new Rect(10, 30, 90, 40), "HP: " + player.HP, sty);
    //    GUI.Label(new Rect(10, 50, 90, 40), "Ime: " + player.name, sty);

    //}   
}
