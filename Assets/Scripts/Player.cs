using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hpMax,attackPower,shoot1Cooltime;
    
    [Range(0.01f,0.9f)]
    public float defenseRate;
    
    int score, killCount, coin, skillUsed;
    float hp;
    bool enableShoot=true;

    public Slider hpBar;

    public GameObject bullet1,kickEffect,healEffect,attackPowerBufEffect, defPowerBufEffect,grenade,shootEffect;
    public Transform firePoint1,kickPoint,grenadePosition;

    public Animator animator;

    public GameManager gameManager;

    private void Start()
    {
        hp = hpMax;
        UpdateHP();
        attackPowerBufEffect.SetActive(false);
        defPowerBufEffect.SetActive(false);
        score = killCount = skillUsed=0;
    }

    /*
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(enableShoot)
            {
                Shoot();
            }    
        }

        if(Input.GetMouseButtonDown(1))
        {
            Kick();
        }
    }
    */

    public void Shoot()
    {
        if(enableShoot)
        {
            enableShoot = false;
            GameObject ShootEffect = Instantiate(shootEffect, firePoint1.position, Quaternion.Euler(0,250,0)) as GameObject;
            Destroy(ShootEffect, 0.2f);
            StartCoroutine(DisableShoot());
            animator.SetTrigger("Shoot1");
            GameObject Bullet1 = Instantiate(bullet1, firePoint1.position, Quaternion.identity) as GameObject;
            Destroy(Bullet1, 2);
        }

    }

    IEnumerator DisableShoot()
    {
        yield return new WaitForSeconds(shoot1Cooltime);
        enableShoot = true;
    }

    public void Kick()
    {
        animator.SetTrigger("Kick");
        StartCoroutine(WaitAndKick());
    }

    IEnumerator WaitAndKick()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject KickEffect = Instantiate(kickEffect, kickPoint.position, Quaternion.identity) as GameObject;
        Destroy(KickEffect, 0.2f);
    }

    public void Heal()
    {
        if(hp<hpMax)
        {
            GameObject HealEffect = Instantiate(healEffect, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity) as GameObject;
            hp += (hpMax * 0.3f);
            UpdateHP();
            Destroy(HealEffect, 1.5f);
            gameManager.UseHeal();
            UpdateSkillUsed();
        }
        
    }

    public void AttackBuff()
    {
        attackPowerBufEffect.SetActive(true);
        attackPower = attackPower + attackPower;
        UpdateSkillUsed();
        StartCoroutine(AttackPowerBuff());
    }

    IEnumerator AttackPowerBuff()
    {
        yield return new WaitForSeconds(10);
        attackPower = attackPower/2;
        attackPowerBufEffect.SetActive(false);
    }
    public void DefBuff()
    {
        defPowerBufEffect.SetActive(true);
        defenseRate = defenseRate + defenseRate;
        UpdateSkillUsed();
        StartCoroutine(DefPowerBuff());
    }

    IEnumerator DefPowerBuff()
    {
        yield return new WaitForSeconds(10);
        defenseRate = defenseRate / 2;
        defPowerBufEffect.SetActive(false);
    }

    public void OnClickGrenade()
    {
        GameObject Grenade=Instantiate(grenade, grenadePosition.position, Quaternion.identity)as GameObject;
        Rigidbody RD = Grenade.GetComponent<Rigidbody>();
        RD.AddForce(Vector3.right*200, ForceMode.Force);
        UpdateSkillUsed();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("x", 0.2, "y", 0.2, "time", 0.5f));
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.animator.SetTrigger("Attack");
            hp -= enemy.attackPower- enemy.attackPower*defenseRate;
            UpdateHP();
            enemy.DieNoScore();     
            if(hp<=0)
            {
                gameManager.UIchangeCGameOver();
            }
        }
    }

    public void UpdateHP()
    {
        hpBar.value = hp / hpMax;
        gameManager.hpLeft = hpBar.value;
    }

    public void ComputeScore(int num)
    {
        score += num;
        killCount++;
        gameManager.score = score;
        gameManager.killCount=killCount;
        gameManager.scoreText.text = score.ToString();
    }

    public void ComputeCoin(int num)
    {
        coin += num;
        gameManager.coinText.text = coin.ToString();
    }

    void UpdateSkillUsed()
    {
        skillUsed++;
        gameManager.skillUsed = skillUsed;
    }
}
