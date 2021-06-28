using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    #region variaveis
    public CharacterMovement cm;
    public Vector2 destination;
    public bool reachedDestination;
    public float movementSpeed;
    public float rotationSpeed;
    public float stopDistance;
    private Vector2 velocity;
    private Vector2 lastPosition;
    public bool stopmove;
    public Waypoint waypoint;
    public Rigidbody2D rb;
    private Quaternion currentRotation;
    private Quaternion rotacaoatual;
    private float numdifX, numdifY;
    public float minimo;
    public bool somacima, somabaixo, somaesquerda, somadireita;
    public bool um, dois, tres, quatro, cinco, seis, sete, oito;
    public bool indoEsquerda, indoDireita, indoCima, indoBaixo;
    private float difx, difY;
    public int tempo_1, tempo_2, tempo_3;
    private float futureRotation;
    private bool girarPositivamente, girarNegativamente;
    public GameObject childgo;
    public bool mascara, infectado,vacinado;
    public bool destino_estacao_2, destino_estacao_3;
    public int randomTime;
    public float f_tempo;
    public double i_tempo;
    public GameObject particulas, part_suspensas;
    public bool dentrodometro;
    public GameObject particula_vacinado;


    #endregion
    private void Awake()
    {
        //if (vacinado)
        //{
        //    Instantiate(particula_vacinado, gameObject.transform.position, particula_vacinado.transform.rotation, gameObject.transform);

        //}

    }
    private void Start()
    {
       
       
        randomTime = 9;
        cm = this;
        um = true; dois = true; tres = true; quatro = true; cinco = true; seis = true; sete = true; oito = true;


        if (transform.rotation.eulerAngles.z == 270)
        {
            somacima = true;

        }
        else if (transform.rotation.eulerAngles.z == 180)
        {
            somadireita = true;

        }
        else if (transform.rotation.eulerAngles.z == 90)
        {
            somabaixo = true;

        }
        else
        {
            somaesquerda = true;
        }


        stopmove = true;
        velocity = GetComponent<Rigidbody2D>().velocity;
        StartCoroutine(tempo1());
        currentRotation = transform.rotation;
       

     
        childgo = gameObject.transform.Find("sprite").gameObject;
        

    }

    private void FixedUpdate()
    {

        Vector3 a = new Vector3(transform.position.x, transform.position.y, 12.7f);
        Vector3 b = new Vector3(destination.x, destination.y, 12.7f);
        transform.position = Vector3.MoveTowards(a, b, movementSpeed);
    }

    void Update()
    {
        f_tempo = Time.time;
        i_tempo = Math.Truncate(f_tempo);
       
     

        if(animportas.ap.estacao > 0)
        {
            if(dentrodometro)
            {
                if (infectado && !mascara && !vacinado)
                {
                    if (f_tempo - i_tempo < 0.2f)
                    {
                        if (UnityEngine.Random.Range(1, 300) == randomTime)
                        {
                            int randomtosse = UnityEngine.Random.Range(1,5);
                            switch (randomtosse)
                            {
                                case 1:
                                    FindObjectOfType<AudioManager>().Play("tosse01");
                                    break;
                                case 2:
                                    FindObjectOfType<AudioManager>().Play("tosse02");
                                    break;
                                case 3:
                                    FindObjectOfType<AudioManager>().Play("tosse03");
                                    break;
                                case 4:
                                    FindObjectOfType<AudioManager>().Play("tosse04");
                                    break;                         
                                    
                            }
                  
                            Instantiate(particulas, gameObject.transform.position, particulas.transform.rotation);
                            Instantiate(part_suspensas, gameObject.transform.position, particulas.transform.rotation);
                            childgo.GetComponent<Animator>().Play("infectado_tossindo");

                        }

                    }
                }
            }
           
        }
       
        


        if (destino_estacao_2)
        {
            if (animportas.ap.desembarque_estacao_2)
            {
                stopmove = false;
                reachedDestination = true;
            }


        }

        if(destino_estacao_3)
        {
            if (animportas.ap.desembarque_estacao_3)
            {
                stopmove = false;
                reachedDestination = true;
            }
        }


        if(stopmove)
        {
            if (infectado)
            {
                childgo.GetComponent<Animator>().Play("infectado_passageiro_idle");
            }
            else if(mascara)
            {
                childgo.GetComponent<Animator>().Play("commascara_passageiro_idle");
            }
            else
            {
                childgo.GetComponent<Animator>().Play("idle_passageiro");
            }
           
        }
        else
        {
            if(infectado)
            {
                childgo.GetComponent<Animator>().Play("infectado_passageiro_andando");
            }
            else if(mascara)
            {
                childgo.GetComponent<Animator>().Play("commascara_passageiro_andando");
            }
            else
            {
                childgo.GetComponent<Animator>().Play("andando_passageiro");
            }
            
        }

        if (girarPositivamente)
        {
            currentRotation.eulerAngles = new Vector3(0, 0, currentRotation.eulerAngles.z + rotationSpeed);
            transform.rotation = currentRotation;
        }

        if (girarNegativamente)
        {
            currentRotation.eulerAngles = new Vector3(0, 0, currentRotation.eulerAngles.z - rotationSpeed);
            transform.rotation = currentRotation;
        }


        if (gameObject.CompareTag("turno1"))
        {
            movimento();
        }
        else if (gameObject.CompareTag("turno2"))
        {
            movimento();

        }
        else if (gameObject.CompareTag("turno3"))
        {
            movimento();
        }

        #region DESTINO

        if (transform.position.x != destination.x && transform.position.y != destination.y)
        {
            Vector2 destinationDirection = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);


            float destinationDistance = destinationDirection.magnitude;
            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;


            }
            else
            {
                reachedDestination = true;
            }
            velocity = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y) / Time.deltaTime;

            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector2.Dot(transform.forward, velocity);
            var rightDotProduct = Vector2.Dot(transform.right, velocity);
        }

        #endregion
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
    


    #region rotacionar
    public void rotacionarDireita()
    {
       
        if (somacima)
        {
            if (um)
            {
                futureRotation = currentRotation.eulerAngles.z - 90;
                Negativar(1);
            }

            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 180);
                transform.rotation = currentRotation;
                TornarNegPos(4);
                girarNegativamente = false;
            }
            else
            {
                girarNegativamente = true;
            }
        }
        else if (somabaixo)
        {
            if (dois)
            {
                futureRotation = currentRotation.eulerAngles.z + 90;
                Negativar(2);
            }


            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 540);
                transform.rotation = currentRotation;

                TornarNegPos(4);
                girarPositivamente = false;
            }
            else
            {
                girarPositivamente = true;
            }

        }

    }


    public void rotacionarEsquerda()
    {


        if (somabaixo)
        {
            if (tres)
            {
                futureRotation = currentRotation.eulerAngles.z - 90;
                Negativar(3);
            }

            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 360);
                transform.rotation = currentRotation;
                TornarNegPos(3);
                girarNegativamente = false;
            }
            else
            {
                girarNegativamente = true;
            }
        }
        else if (somacima)
        {
            if (quatro)
            {
                futureRotation = currentRotation.eulerAngles.z + 90;
                Negativar(4);
            }


            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 360);
                transform.rotation = currentRotation;
                TornarNegPos(3);
                girarPositivamente = false;
            }
            else
            {
                girarPositivamente = true;

            }
        }


    }

    public void rotacionarCima()
    {

        if (somadireita)
        {
            if (cinco)
            {
                futureRotation = currentRotation.eulerAngles.z + 90;
                Negativar(5);
            }


            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 270);
                transform.rotation = currentRotation;
                TornarNegPos(1);
                girarPositivamente = false;
            }
            else
            {
                girarPositivamente = true;
            }

        }
        else if (somaesquerda)
        {
            if (seis)
            {
                
                    futureRotation = 270;
                   
                    Negativar(6);
                
            }


            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 270);
                transform.rotation = currentRotation;
                TornarNegPos(1);
                girarNegativamente = false;
            }
            else
            {
                girarNegativamente = true;
            }

        }

    }

    public void rotacionarBaixo()
    {

        if (somaesquerda)
        {

            if (sete)
            {

                futureRotation = currentRotation.eulerAngles.z + 90;

                Negativar(7);

            }


            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 90);
                transform.rotation = currentRotation;
                TornarNegPos(2);
                girarPositivamente = false;
            }
            else
            {
                girarPositivamente = true;
            }



        }
        else if (somadireita)
        {
            if (oito)
            {
                futureRotation = currentRotation.eulerAngles.z - 90;
                Negativar(8);
            }

            if (currentRotation.eulerAngles.z > futureRotation - 11 && currentRotation.eulerAngles.z < futureRotation + 11)
            {
                currentRotation.eulerAngles = new Vector3(0, 0, 90);
                transform.rotation = currentRotation;
                TornarNegPos(2);
                girarNegativamente = false;
            }
            else
            {
                girarNegativamente = true;
            }
        }
    }
    #endregion
    public IEnumerator tempo1()
    {
        yield return new WaitForSeconds(tempo_1);
        stopmove = false;
        reachedDestination = true;
    }
    public IEnumerator tempo2()
    {
        yield return new WaitForSeconds(tempo_2);
        stopmove = false;
        reachedDestination = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("dentrodometro"))
        {
            dentrodometro = true;
        }
        else
        {
            dentrodometro = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dentrodometro = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("parar"))
        {
            Debug.Log("parou");
            stopmove = true;
            StartCoroutine(tempo2());
        }

        if (collision.CompareTag("cima"))
        {

            currentRotation.eulerAngles = new Vector3(0, 0, 270);
            transform.rotation = currentRotation;
            somacima = true;
            somabaixo = false;
            stopmove = true;
           // StartCoroutine(tempo1());
        }

        if (collision.CompareTag("baixo"))
        {

            currentRotation.eulerAngles = new Vector3(0, 0, 90);
            transform.rotation = currentRotation;
            somabaixo = true;
            somacima = false;
            stopmove = true;
            //StartCoroutine(tempo1());
        }

        if(collision.CompareTag("nuvem"))
        {
            // Se um não infectado (sem mascara / sem vacina)  entrar na nuvem = infectado
            if(vacinado == false && mascara == false && infectado == false)
            {
                infectado = true;
                uiscript.us.num_infectados++;
            }
        }

    }

   



    #region movimento
    public void movimento()
    {
        // O objetivo do numdifx e numdify é ser sempre positivo .
        if (stopmove == false)
        {
            if (transform.position.x != destination.x && transform.position.y != destination.y)
            {
                Vector2 destinationDirection = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);

                if (destination.x > transform.position.x)
                {
                    if (destination.x > 0 && transform.position.x > 0)
                    {
                        numdifX = destination.x - transform.position.x;
                    }
                    else if (destination.x < 0 && transform.position.x < 0)
                    {
                        numdifX = destination.x - transform.position.x;
                    }
                    else
                    {
                        // numdifX = destination.x + transform.position.x;
                        numdifX = - destination.x - transform.position.x;

                    }
                }
                else
                {
                    if (transform.position.x > 0 && destination.x > 0)
                    {
                        numdifX = transform.position.x - destination.x;
                    }
                    else if (destination.x < 0 && transform.position.x < 0)
                    {
                        numdifX = transform.position.x - destination.x;
                    }
                    else
                    {
                        numdifX = transform.position.x + destination.x;
                    }

                }

                if (destination.y > transform.position.y)
                {
                    if (destination.y > 0 && transform.position.y > 0)
                    {
                        numdifY = destination.y - transform.position.y;
                    }
                    else if (destination.y < 0 && transform.position.y < 0)
                    {
                        numdifY = destination.y - transform.position.y;
                    }
                    else
                    {
                        numdifY = destination.y + transform.position.y;
                    }
                }
                else
                {
                    if (destination.y > 0 && transform.position.y > 0)
                    {
                        numdifY = transform.position.y - destination.y;
                    }
                    else if (destination.y < 0 && transform.position.y < 0)
                    {
                        numdifY = transform.position.y - destination.y;
                    }
                    else
                    {
                        numdifY = transform.position.y - destination.y;
                    }
                }


                if (numdifX > minimo)
                {
                    if (destination.x > transform.position.x)
                    {
                        if (!somadireita)
                            rotacionarDireita();

                    }
                    else
                    {
                        if (!somaesquerda)
                            rotacionarEsquerda();

                    }
                }

                if (numdifY > minimo)
                {
                    if (destination.y > transform.position.y)
                    {
                        if (!somacima)
                            rotacionarCima();

                    }
                    else
                    {
                        if (!somabaixo)
                            rotacionarBaixo();

                    }

                }

                float destinationDistance = destinationDirection.magnitude;
                if (destinationDistance >= stopDistance)
                {

                    reachedDestination = false;




                    // transform.Translate(destinationDirection * movementSpeed * Time.deltaTime, Space.World);

                }
                else
                {
                    reachedDestination = true;
                }

                velocity = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y) / Time.deltaTime;


            }

        }
        else
        {
            destination = transform.position;
        }
    }
    #endregion

    #region TornarNegsomacima
    public void TornarNegPos(int num)
    {
        if (num == 1)
        {
            somacima = true;
            somabaixo = false;
            somaesquerda = false;
            somadireita = false;
        }
        else if (num == 2)
        {
            somabaixo = true;
            somacima = false;
            somaesquerda = false;
            somadireita = false;
        }
        else if (num == 3)
        {
            somaesquerda = true;
            somabaixo = false;
            somacima = false;
            somadireita = false;
        }
        else
        {
            somadireita = true;
            somabaixo = false;
            somaesquerda = false;
            somacima = false;
        }

    }
    #endregion

    #region Negativar123
    public void Negativar(int num)
    {
        if (num == 1)
        {
            um = false;
            dois = true;
            tres = true;
            quatro = true;
            cinco = true;
            seis = true;
            sete = true;
            oito = true;
        }
        else if (num == 2)
        {
            um = true;
            dois = false;
            tres = true;
            quatro = true;
            cinco = true;
            seis = true;
            sete = true;
            oito = true;
        }
        else if (num == 3)
        {
            um = true;
            dois = true;
            tres = false;
            quatro = true;
            cinco = true;
            seis = true;
            sete = true;
            oito = true;
        }
        else if (num == 4)
        {
            um = true;
            dois = true;
            tres = true;
            quatro = false;
            cinco = true;
            seis = true;
            sete = true;
            oito = true;
        }
        else if (num == 5)
        {
            um = true;
            dois = true;
            tres = true;
            quatro = true;
            cinco = false;
            seis = true;
            sete = true;
            oito = true;
        }
        else if (num == 6)
        {
            um = true;
            dois = true;
            tres = true;
            quatro = true;
            cinco = true;
            seis = false;
            sete = true;
            oito = true;
        }
        else if (num == 7)
        {
            um = true;
            dois = true;
            tres = true;
            quatro = true;
            cinco = true;
            seis = true;
            sete = false;
            oito = true;
        }
        else
        {
            um = true;
            dois = true;
            tres = true;
            quatro = true;
            cinco = true;
            seis = true;
            sete = true;
            oito = false;
        }
    }
#endregion
}




