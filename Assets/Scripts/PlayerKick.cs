using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    Player player;

    public float lifeTime,addDamage;
    

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Destroy(this.gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.hp -= (player.attackPower+addDamage);

            if (enemy.hp <= 0)
            {
                enemy.DieScore();
                player.ComputeScore(enemy.score);
                int randomCoin = Random.Range(enemy.minCoin, enemy.maxCoin);
                player.ComputeCoin(randomCoin);
            }
        }
    }
}
