using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpKinRock : MonoBehaviour
{
    //public GameObject impactEffect;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        //GameObject impact =  Instantiate(impactEffect, transform.position, Quaternion.identity);
        //Destroy(impact, 2);
        Destroy(gameObject, 2);
    }
}
