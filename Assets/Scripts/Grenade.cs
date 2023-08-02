using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenadeExplosion;

    void Update()
    {
        if(transform.position.y<=0.2f)
        {
            Instantiate(grenadeExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
