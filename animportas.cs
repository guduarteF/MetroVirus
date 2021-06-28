using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animportas : MonoBehaviour
{
    public int estacao;
    public static animportas ap;
    public bool desembarque_estacao_2, desembarque_estacao_3;
    public GameObject vitoriaUI, derrotaUI;
   
    
    void Start()
    {
        ap = this;
        StartCoroutine(abrir_e_fechar_portas());
      
        
    }

    void Update()
    {
        if (estacao == 4 || uiscript.us.num_infectados == 0)
        {
            endGame();
        }

    }

    public IEnumerator abrir_e_fechar_portas()
    {
        yield return new WaitForSeconds(20f);
        GetComponent<Animator>().Play("portasAbrindo");
        metro.m.pararParticula();
        if(estacao == 1)
        {
            desembarque_estacao_2 = true;           
        }
        else if(estacao == 2)
        {
            desembarque_estacao_3 = true;
        }
        estacao++;
        Debug.Log("estacao : " + estacao);
        yield return new WaitForSeconds(8f);
        GetComponent<Animator>().Play("portasFechando");
        metro.m.playParticula();
        StartCoroutine(abrir_e_fechar_portas());
    }

    public void endGame()
    {
       
            if (uiscript.us.num_infectados == 0 )
            {
                vitoria();
                
            }
            else
            {

                derrota();
                
            }
       
    }

    public void vitoria()
    {
        vitoriaUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void derrota()
    {
        derrotaUI.SetActive(true);
        Time.timeScale = 0;
    }

    
}
