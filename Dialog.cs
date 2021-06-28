using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public GameObject continueButton;
    public bool diretora_falando, player_falando;
    public GameObject diretora, player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }

        if(index == 0)
        {
            diretora.GetComponent<Animator>().Play("diretora_01");
            player.GetComponent<Animator>().Play("player_dialog");
        }

        if(index == 1 || index == 3 || index == 5 )
        {
            diretora.GetComponent<Animator>().Play("diretora_falando");
            player.GetComponent<Animator>().Play("player_idle");
        }
       
       if(index == 2 || index == 4 || index == 6)
        { 
                player.GetComponent<Animator>().Play("player_falando");
                diretora.GetComponent<Animator>().Play("diretora_idle");
                       
        }

      


    }

    IEnumerator Type()
    {
        yield return new WaitForSeconds(1f);
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f); 
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index == sentences.Length - 1)
        {
            startgame();
        }

        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type()) ;
            
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(true); 
        }
    }

    public void startgame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
