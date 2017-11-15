using UnityEngine;
using System.Collections;

public class Teleportation : MonoBehaviour {

    public GameObject teleportTo;
    RaycastHit lastRaycastHit;
    public AudioClip Sound1;
    public AudioClip Sound2;
    public AudioClip Sound3;
    public AudioSource AudioSource;
    IEnumerator r ; //Za odgodeno pustanje Sound2, jer nam je potrabno da bi stopirali corutinu 

	private GameObject GetLookAtObject()
    {
        Vector3 nasaPozicija = transform.position;
        Vector3 direction = Camera.main.transform.forward;
       
        float range = 30;
        if (Physics.Raycast(nasaPozicija, direction, out lastRaycastHit, range))
            return lastRaycastHit.collider.gameObject;
        else
        {
            print("null");
            return null;
        }
            
    }
    private void TeleportToLookAt()
    {
        transform.position = lastRaycastHit.point + lastRaycastHit.normal * 2;
        
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                teleportTo.SetActive(true);
                if (Sound1 != null)
                {
                    AudioSource.clip = Sound1;
                    AudioSource.Play();
                }
                r = WaitForSeconds(1.566f);
                StartCoroutine(r);
            }

            
            GameObject o = GetLookAtObject();
            Vector3 pozicija=new Vector3();
            if(o!=null) 
                pozicija = o.transform.position;
           

            if(o != null && pozicija!=transform.position)           
                teleportTo.transform.position = lastRaycastHit.point;

        }

        if(Input.GetMouseButtonUp(1))
        {
            AudioSource.loop = false;
            StopCoroutine(r);
            if (GetLookAtObject() != null)
            {                           
                AudioSource.clip = Sound3;
                AudioSource.Play();
            }
                TeleportToLookAt();
            teleportTo.SetActive(false);
        }
	}
    
    void PlaySound2()
    {
        if (Sound2 != null)
        {
            AudioSource.clip = Sound2;
            AudioSource.loop = true;
            AudioSource.Play();
        }
    }
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PlaySound2();
    }
}
