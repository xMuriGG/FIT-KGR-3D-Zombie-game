using UnityEngine;
using System.Collections;


public class ZombieScript : MonoBehaviour
{
    public GameObject player;
    public AudioClip zombieDeathClip;
    public AudioClip zombieAttackClip;

    NavMeshAgent navAgent;
    Animator myAnimation;    
    Vector3 pocetnalokacija;
    float zombieHP ;
    bool isDead = false;
    bool canAttack = true;
    public bool isIdle = true;

    void Start()
    {
        zombieHP = 100;
        myAnimation = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        pocetnalokacija = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
       
        if (gameObject == null)
            return;

        if (myAnimation.GetBool("isDead"))
            return;
       
            if (Vector3.Distance(player.transform.position, this.transform.position) < 20)
            {
                Vector3 direction = GetUdaljenostObjekata(player.transform.position, this.transform.position);
                float angle = Vector3.Angle(direction, transform.forward);
              
                if (angle < 140)
                {
                    isIdle = false;                   
                    navAgent.SetDestination(player.transform.position);

                    //Ako si unutar x "metara" nece hodat 
                    if (direction.magnitude > 2)
                    {           
                        myAnimation.SetBool("isWalking", true);
                        myAnimation.SetBool("isAttacking", false);                 
                    }
                    else
                    {
                        if (canAttack)
                            StartCoroutine(ZombieAttackAndCoolDown());

                        myAnimation.SetBool("isWalking", false);

                        if (!myAnimation.GetBool("isAttacking"))
                            myAnimation.SetFloat("AttackType", Random.Range(1, 3));

                        myAnimation.SetBool("isAttacking", true);
                    }
                }
            }
            else
            {
                Vector3 direction = GetUdaljenostObjekata(pocetnalokacija, this.transform.position);
                if (myAnimation.GetBool("isWalking"))
                {
                    if (direction.magnitude > 1)               
                        navAgent.SetDestination(pocetnalokacija);
                    else
                    {
                        myAnimation.SetBool("isWalking", false);
                        isIdle = true;
                    }
                }
            }       
    }

    Vector3 GetUdaljenostObjekata(Vector3 o1, Vector3 o2)
    {
        Vector3 v = o1 - o2;
        v.y = 0;
        return v;
    }

    void ApplyDamage(float dealDMG)
    {

        zombieHP -= dealDMG;     
        if (zombieHP <= 0)
        {
            isDead = true;
            myAnimation.SetBool("isDead", isDead);
            AudioSource.PlayClipAtPoint(zombieDeathClip, transform.position,0.5f);

            //disablovanje boxCollidera nakon smrti zombija
            navAgent.Stop();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.transform.FindChild("LowRetopoUV").GetComponent<BoxCollider>().enabled = false;

            Destroy(gameObject, 9f);
            GetComponent<AudioSource>().Stop();
            GameObject.Find("Canvas").GetComponent<Level001MenuScript>().ZombieDeathCounter();

        }
        else
        {
            myAnimation.SetBool("isDead", isDead);
            myAnimation.SetFloat("TakeDemageType", Random.Range(1, 4));
            myAnimation.Play("TakingDamagesTree");
        }

        if (canAttack)
        {
            StartCoroutine(ZombieAttackAndCoolDown());     
        }
        
    }

    IEnumerator ZombieAttackAndCoolDown()
    {
        AudioSource.PlayClipAtPoint(zombieAttackClip, transform.position, 0.8f);
        canAttack = false;
        yield return new WaitForSeconds(1.25f);
        canAttack = true;

    }
}
