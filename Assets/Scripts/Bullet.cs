using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed, attackPower;
    public GameObject hitEffect;
    Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        float distanceX = moveSpeed * Time.deltaTime;
        this.gameObject.transform.Translate(distanceX, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            GameObject HitEffect = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
            Destroy(HitEffect, 1.5f);
            //Rigidbody rd = other.GetComponent<Rigidbody>();
            //rd.AddForce(Vector3.right*80, ForceMode.Force);
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.hp -= attackPower+player.attackPower;
            if(enemy.hp<=0)
            {
                enemy.DieScore();
                player.ComputeScore(enemy.score);                
                int randomCoin = Random.Range(enemy.minCoin, enemy.maxCoin);
                player.ComputeCoin(randomCoin);
            }
            Destroy(this.gameObject);
        }
    }
}
