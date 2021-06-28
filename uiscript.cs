using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiscript : MonoBehaviour
{
    public static uiscript us;
    public int num_atual;
    public GameObject mascara1, vacinar2, desinfectar3, sprite_fundo;
    public Text txt_num_masc, txt_num_vac, txt_num_spray, txt_horas, txt_minutos, txt_infectados;
    public float horas, minutos = 0;
    public int LimiteDosMinutos;
    public int num_infectados , num_vacinados , num_mascarados;
    public int num_min_infectados;
    public GameObject[] passageiros;
    public TextMeshProUGUI txt_estacao_atual;
    public GameObject vacinado_part;
    
    
    
    
  
    
    // Start is called before the first frame update
    void Start()
    {      
        us = this;
        num_atual = 1;
        sprite_fundo.transform.position = mascara1.transform.position;

        for (int i = 0; i < passageiros.Length ; i++)
        {
            int random = Random.Range(1, 6);
            if(random == 1 && num_infectados < num_min_infectados)
            {
                passageiros[i].GetComponent<CharacterMovement>().infectado = true;
                passageiros[i].GetComponent<CharacterMovement>().mascara = false;
                passageiros[i].GetComponent<CharacterMovement>().vacinado = false;
                num_infectados++;
            }
            else if(random == 2 && num_mascarados < 2)
            {
                passageiros[i].GetComponent<CharacterMovement>().mascara = true;
                passageiros[i].GetComponent<CharacterMovement>().infectado = false;
                passageiros[i].GetComponent<CharacterMovement>().vacinado = false;
                num_mascarados++;

            }
            else if(random == 3 && num_vacinados < num_min_infectados)
            {
                num_vacinados++;
                passageiros[i].GetComponent<CharacterMovement>().mascara = false;
                passageiros[i].GetComponent<CharacterMovement>().vacinado = true;
                passageiros[i].GetComponent<CharacterMovement>().infectado = false;
                Instantiate(vacinado_part, passageiros[i].transform.position, vacinado_part.transform.rotation, passageiros[i].transform);
                   
               
            }
            else if(random == 4 && num_infectados < num_min_infectados)
            {
                passageiros[i].GetComponent<CharacterMovement>().infectado = true;
                passageiros[i].GetComponent<CharacterMovement>().mascara = false;
                passageiros[i].GetComponent<CharacterMovement>().vacinado = false;
                num_infectados++;
            }
            else if(random == 5 && num_infectados < num_min_infectados)
            {
                passageiros[i].GetComponent<CharacterMovement>().infectado = true;
                passageiros[i].GetComponent<CharacterMovement>().mascara = false;
                passageiros[i].GetComponent<CharacterMovement>().vacinado = false;
                num_infectados++;
            }
        }
    }

    private void FixedUpdate()
    {
        txt_minutos.text = minutos.ToString("00");
        txt_horas.text = horas.ToString("00");
        minutos += Time.deltaTime;
        if(minutos >= LimiteDosMinutos)
        {
            horas++;
            minutos = 0 + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
       



        txt_num_masc.text = "x" + playerMovement.p.num_usos_mascara.ToString();
        txt_num_spray.text = "x" + playerMovement.p.num_usos_spray.ToString();
        txt_num_vac.text = "x" +  playerMovement.p.num_usos_vacina.ToString();
        txt_infectados.text = "Infectados : " + num_infectados;
        txt_estacao_atual.text = animportas.ap.estacao.ToString();

        
       


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (num_atual == 1)
            {
                FindObjectOfType<AudioManager>().Play("e");
                num_atual++;
                playerMovement.p.num_atual = num_atual;
                sprite_fundo.transform.position = new Vector2(vacinar2.transform.position.x, sprite_fundo.transform.position.y);
            }
            else if (num_atual == 2)
            {
                FindObjectOfType<AudioManager>().Play("e");
                num_atual++;
                playerMovement.p.num_atual = num_atual;
                sprite_fundo.transform.position = new Vector2(desinfectar3.transform.position.x, sprite_fundo.transform.position.y);
            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (num_atual == 2)
            {
                FindObjectOfType<AudioManager>().Play("q");
                num_atual--;
                playerMovement.p.num_atual = num_atual;
                sprite_fundo.transform.position = new Vector2(mascara1.transform.position.x, sprite_fundo.transform.position.y);
            }
            else if (num_atual == 3)
            {
                FindObjectOfType<AudioManager>().Play("q");
                num_atual--; 
                playerMovement.p.num_atual = num_atual;
                sprite_fundo.transform.position = new Vector2(vacinar2.transform.position.x,sprite_fundo.transform.position.y);
            }
        }
    }
}
