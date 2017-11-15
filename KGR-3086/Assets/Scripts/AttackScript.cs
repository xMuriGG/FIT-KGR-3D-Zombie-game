using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour
{
    public GameObject bloodSplat;
    public float reduceDamagePoints;
    float rangeToTarget;
    float toTarget;
    public AudioClip attackClip;
    public Animation animator;
    bool isAnimationPlayed = false;
    bool attackCoolDown = true;
    void Awake()
    {
        reduceDamagePoints = 33f;
        rangeToTarget = 5;
    }

    void Update()
    {
        if (!attackCoolDown)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            AudioSource.PlayClipAtPoint(attackClip, gameObject.transform.position);
            RaycastHit hit = new RaycastHit();
            isAnimationPlayed = false;
           
            if (Physics.Raycast(transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit))
            {
                toTarget = hit.distance;

                if (hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "child")
                {
                    if (toTarget < rangeToTarget)
                    {
                        //ako je udarac u collider box koji je na rukama
                        if (hit.collider.gameObject.tag == "child")
                        {
                            hit.transform.parent.transform.SendMessage("ApplyDamage", reduceDamagePoints, SendMessageOptions.DontRequireReceiver);
                        }
                        else//ako je udarac u collider box koji je na tijelu
                        {
                            if (hit.collider.gameObject.GetComponent<ZombieScript>().isIdle)
                            {
                                hit.transform.SendMessage("ApplyDamage", 100f, SendMessageOptions.DontRequireReceiver);
                                animator.Play("AttachStubAnimation");
                                isAnimationPlayed = true;
                            }
                            else
                            {
                                hit.transform.SendMessage("ApplyDamage", reduceDamagePoints, SendMessageOptions.DontRequireReceiver);
                            }
                            StartCoroutine(AttackCoolDown(0.833f));
                        }
                        Instantiate(bloodSplat, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                    }
                }
            }
            if (!isAnimationPlayed)
            {
                animator.Play("AttackAnimation");
                StartCoroutine(AttackCoolDown(0.383f));
            }

        }
    }
    IEnumerator AttackCoolDown(float wait)
    {
        attackCoolDown = false;
        yield return new WaitForSeconds(wait);
        attackCoolDown = true;
    }
}

