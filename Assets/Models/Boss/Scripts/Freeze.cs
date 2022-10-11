using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Freeze : MonoBehaviour
{
    public GameObject freeze;
    private GameObject player;

    private Rigidbody rigidBody;
    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        input = player.GetComponent<PlayerInput>();

        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            

            GameObject freezeP = Instantiate(freeze, transform.position, Quaternion.identity) as GameObject;
            Destroy(freezeP, 7);

            input.actions.Disable();
            Debug.Log("disable input");
        }
        
    }
}
