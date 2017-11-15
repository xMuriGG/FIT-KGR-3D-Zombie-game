using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
public class menuScript : MonoBehaviour {
    public GameObject player;
	public void PlayGame()
    {
        print("Play Game");  
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        print("Exit Game");
        Application.Quit();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().buildIndex==1)
            {
                PrikaziUIPanelElemente();
            }
        }    
    }

    public void PrikaziUIPanelElemente()
    {
        if (player != null && !Cursor.visible)
        {
            player.transform.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            player.transform.GetComponent<FirstPersonController>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        GameObject panel = gameObject.transform.FindChild("Panel").gameObject;
        panel.SetActive(!panel.activeSelf);
    }
}
