using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement p;
    public float velocidade;
    public float num_atual;
    public ParticleSystem spray;
    private Quaternion currentRotation;
    public RaycastHit2D range;
    public float tamanho_do_raio;
    public GameObject mascara_go;
    public GameObject childgo;
    public GameObject seringa;
    public int num_usos_mascara = 3, num_usos_spray = 3, num_usos_vacina = 3;
    public GameObject spray_spawn;
    public GameObject vacinado_part;
   


    void Start()
    {
        num_atual = 1;
        p = this;
        childgo = gameObject.transform.Find("playerSPRITE").gameObject;
    }

    // Update is called once per frame
    void Update()   
    {

        
        #region Movimento
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (uiscript.us.num_atual == 3)
        {
            seringa.SetActive(true);

        }
        else
        {
            seringa.SetActive(false);
        }

        if (hori > 0 || verti > 0 || hori < 0 || verti < 0)
        {
            if (num_atual == 2)
            {
                childgo.GetComponent<Animator>().Play("walk_maskara");
            }
            else
            {
                childgo.GetComponent<Animator>().Play("walk_Player");
            }
           
           

        }
        else
        {
           

            if (num_atual == 2)
            {
                childgo.GetComponent<Animator>().Play("idle_mascara");
            }
            else
            {
                childgo.GetComponent<Animator>().Play("idle_Player");
            }
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, verti * velocidade);
            currentRotation.eulerAngles = new Vector3(0, 0, -90);
            transform.rotation = currentRotation;
            
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(hori * velocidade, rb.velocity.y);
            currentRotation.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = currentRotation;
        }  

        if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, verti * velocidade);
            currentRotation.eulerAngles = new Vector3(0, 0, 90);
            transform.rotation = currentRotation;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(hori * velocidade, rb.velocity.y);
            currentRotation.eulerAngles = new Vector3(0, 0, 180);
            transform.rotation = currentRotation;
        }
        #endregion

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(num_atual == 1)
            {
              
                if(num_usos_spray > 0)
                {
                    FindObjectOfType<AudioManager>().Play("spray");
                    num_usos_spray--;
                    Instantiate(spray, spray_spawn.transform.position, gameObject.transform.rotation);
                }

               
            
            }
            else if(num_atual == 2)
            {
                if(num_usos_mascara > 0)
                {
                    LayerMask IA = LayerMask.GetMask("IA");
                    Debug.DrawRay(gameObject.transform.position, -transform.right, Color.red, tamanho_do_raio);
                    RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -transform.right, tamanho_do_raio,IA);
                    if (hit.collider)
                    {
                        if (hit.collider.gameObject.name == "IA")
                        {                        
                          
                            // Instantiate(mascara_go, hit.collider.gameObject.transform.position, mascara_go.transform.rotation);
                            GameObject AI_GO = hit.collider.gameObject;
                            if (!AI_GO.GetComponent<CharacterMovement>().cm.mascara && !AI_GO.GetComponent<CharacterMovement>().cm.infectado)
                            {
                                num_usos_mascara--;
                                FindObjectOfType<AudioManager>().Play("mascara");
                                //  Instantiate(mascara_go, AI_GO.transform);
                                AI_GO.GetComponent<CharacterMovement>().cm.mascara = true;
                            }
                        }
                    }
                    
                }
             
            }
            else
            {
                if(num_usos_vacina > 0)
                {
                    LayerMask IA = LayerMask.GetMask("IA");
                    Debug.DrawRay(gameObject.transform.position, -transform.right, Color.red, tamanho_do_raio);
                    RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -transform.right, tamanho_do_raio, IA);
                    if (hit.collider)
                    {
                        if (hit.collider.gameObject.name == "IA")
                        {
                        

                           
                            GameObject AI_GO = hit.collider.gameObject;
                            if (AI_GO.GetComponent<CharacterMovement>().cm.infectado == true)
                            {
                                num_usos_vacina--;
                                FindObjectOfType<AudioManager>().Play("desinfectado");
                                uiscript.us.num_infectados--;
                                AI_GO.GetComponent<CharacterMovement>().cm.infectado = false;
                                Instantiate(vacinado_part, AI_GO.transform.position, vacinado_part.transform.rotation);

                            }

                            //AI_GO.GetComponent<CharacterMovement>().cm.infectado = true;




                        }
                    }
                }
               
            }
        }
       

      
    }
   
    
}
