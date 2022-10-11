using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour

{

    public GameObject explosion; // drag your explosion prefab here

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter()
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(expl, 1); // delete the explosion after 1 seconds
    }

    private void OnEnable()
    {
        Destroy(gameObject, 2);
    }

}