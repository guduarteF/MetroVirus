using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metro : MonoBehaviour
{
    public GameObject particula;
    public static metro m;
    private void Start()
    {
        m = this;
    }
    private void Update()
    {
       
    }


    public void pararParticula()
    {
        particula.GetComponent<ParticleSystem>().emissionRate = 0;
    }

    public void playParticula()
    {
        particula.GetComponent<ParticleSystem>().emissionRate = 30;
    }
}
