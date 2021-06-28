using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_mangaer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fase2()
    {
        SceneManager.LoadScene("FaseDois");
    }

    public void Start_game()
    {
        SceneManager.LoadScene("Dialogo");
        FindObjectOfType<AudioManager>().Play("startsom");
    }

    public void Fase1()
    {
       
        SceneManager.LoadScene("SampleScene");
        
    }

    public void Quit()
    {
        Application.Quit();
        FindObjectOfType<AudioManager>().Play("quitsom");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void Controles()
    {
        SceneManager.LoadScene("Controles");
        
    }

    public void Video()
    {
        SceneManager.LoadScene("Video");
        
    }
    
    public void Menu2()
    {
        SceneManager.LoadScene("Menu2");
    }

    public void Proximo()
    {
        SceneManager.LoadScene("Proximo");
    }

    public void Anterior()
    {
        SceneManager.LoadScene("Proximo 1");
    }
}
