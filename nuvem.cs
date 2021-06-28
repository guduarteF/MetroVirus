using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuvem : MonoBehaviour
{
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);    
    }
}
