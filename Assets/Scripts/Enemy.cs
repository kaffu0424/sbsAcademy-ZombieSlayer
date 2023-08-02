using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveSpeed,attackPower,hp;
    public int score, minCoin, maxCoin;
    public GameObject dieEffect;
    public Animator animator;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {

        if(gameManager.GS==GameManager.GameStatus.Playing)
        {
            float distanceX = moveSpeed * Time.deltaTime;
            this.gameObject.transform.Translate(-1 * distanceX, 0, 0);
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }

    public void DieNoScore()
    {
        animator.SetTrigger("Death");
        GameObject DieEffect = Instantiate(dieEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(DieEffect, 1.5f);
        Destroy(this.gameObject);
    }

    public void DieScore()
    {
        animator.SetTrigger("Death");
        GameObject DieEffect = Instantiate(dieEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(DieEffect, 1.5f);
        Destroy(this.gameObject);
    }
}