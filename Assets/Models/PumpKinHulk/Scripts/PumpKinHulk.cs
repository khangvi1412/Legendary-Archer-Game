using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpKinHulk : MonoBehaviour
{
    public GameObject PumpKinHulkRock;
    public Transform PumpKinHulkRockPoint;

    public GameObject playerObject;
    Transform player;

    float distance;

    public float transForward = 140f;
    //public float transDown = 5f;

    public void Awake()
    {
        player = playerObject.transform;
    }

    public void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.position);
    }
    public void ThrowRock()
    {
        Vector3 dir = player.position - PumpKinHulkRockPoint.position;
        dir = dir + new Vector3(0, 3.5f, 0);
        Rigidbody rb = Instantiate(PumpKinHulkRock, PumpKinHulkRockPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(dir * distance * transForward / distance, ForceMode.Force);
        //rb.AddForce(-transform.up * transDown, ForceMode.Impulse);
    }
}
