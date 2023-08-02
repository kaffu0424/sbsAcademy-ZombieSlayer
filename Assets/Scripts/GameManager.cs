using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Slider progressSlider,loadingSlider;
    public float progressSpeed;
    public int grenadeCountMax;
    int grenadeCount;
    public TextMeshProUGUI scoreText, coinText, healTimerText, attackBufTimerText, defBufTimerText, grenadeTimerText, grenadeCountText, reportText,finalScoreText;
    public int score, skillUsed, killCount;
    public float hpLeft;//timeLeft, 

    public GameObject pauseUI, playUI, finishCompleteUI, finishGameOVerUI, mainUI, introUI;
    public Button healBtn, attackBufBtn, defBufBtn, grenadeBtn;
    public float healCooltime, attackBufCooltime, defBufCooltime, grenadeCooltime;
    float healCooltimer, attackBufCooltimer, defBufCooltimer, grenadeCooltimer;

    public enum GameStatus {Intro,Main,Playing,FinishComplete,FinishGameOver,Shop };
    public GameStatus GS;

    private void Start()
    {
        UpdateScoreCoin(0, 0);
        GS = GameStatus.Intro;
        pauseUI.SetActive(false);
        healBtn.interactable = true;
        attackBufBtn.interactable = true;
        defBufBtn.interactable = true;
        grenadeBtn.interactable = true;
        healCooltimer = healCooltime;
        attackBufCooltimer = attackBufCooltime;
        defBufCooltimer = defBufCooltime;
        grenadeCooltimer = grenadeCooltime;
        grenadeCount = grenadeCountMax;
    }

    private void Update()
    {
        if(GS==GameStatus.Intro)
        {
            UIchangeIntro();
            if(loadingSlider.value<1)
            {
                loadingSlider.value += Time.deltaTime * 0.1f;
            }      
            else
            {
                UIchangeMain();
            }
        }
        
        else if(GS==GameStatus.Main)
        {
            UIchangeMain();
        }
        else if(GS==GameStatus.Playing)
        {
            UIchangePlay();
        }
        else if(GS==GameStatus.FinishComplete)
        {
            UIchangeComplete();
        }
        else
        {
            UIchangeCGameOver();
        }
        
        if(progressSlider.value>=1)
        {
            //print("Stage Complete");
            progressSlider.value = 0;
            UIchangeComplete();
        }
        else
        {
            if(GS==GameStatus.Playing)
            {
                progressSlider.value += (Time.deltaTime * progressSpeed);
            }            
        }        

        if(healCooltimer<healCooltime)
        {
            healBtn.interactable = false;
            healCooltimer += Time.deltaTime;
            healTimerText.text = ((int)(healCooltime - healCooltimer)).ToString();
        }
        else
        {
            healBtn.interactable = true;
            healTimerText.text = "";
        }

        if (attackBufCooltimer < attackBufCooltime)
        {
            attackBufBtn.interactable = false;
            attackBufCooltimer += Time.deltaTime;
            attackBufTimerText.text = ((int)(attackBufCooltime - attackBufCooltimer)).ToString();
        }
        else
        {
            attackBufBtn.interactable = true;
            attackBufTimerText.text = "";
        }

        if (defBufCooltimer < defBufCooltime)
        {
            defBufBtn.interactable = false;
            defBufCooltimer += Time.deltaTime;
            defBufTimerText.text = ((int)(defBufCooltime - defBufCooltimer)).ToString();
        }
        else
        {
            defBufBtn.interactable = true;
            defBufTimerText.text = "";
        }        

        
            if (grenadeCount > 0)
            {
                if (grenadeCooltimer < grenadeCooltime)
                {
                    grenadeBtn.interactable = false;
                    grenadeCooltimer += Time.deltaTime;
                    grenadeTimerText.text = ((int)(grenadeCooltime - grenadeCooltimer)).ToString();
                grenadeCountText.text = grenadeCount + "/" + grenadeCountMax;
            }
                else
                {
                    grenadeBtn.interactable = true;
                    grenadeTimerText.text = "";
                    grenadeCountText.text = grenadeCount + "/" + grenadeCountMax;
                }                
            }
            else
            {
                grenadeBtn.interactable = false;
                grenadeTimerText.text = "";
                grenadeCountText.text = "0/"+grenadeCountMax;
            }
        
    }

    public void UIchangeIntro()
    {
        GS = GameStatus.Intro; 
        introUI.SetActive(true);
        mainUI.SetActive(false);
        playUI.SetActive(false);
        finishCompleteUI.SetActive(false);
        finishGameOVerUI.SetActive(false);
    }

    public void UIchangeMain()
    {
        //print(Time.timeScale);
        Time.timeScale = 1;
        GS = GameStatus.Main;        
        introUI.SetActive(false);
        mainUI.SetActive(true);
        playUI.SetActive(false);
        finishCompleteUI.SetActive(false);
        finishGameOVerUI.SetActive(false);
    }

    public void UIchangePlay()
    {
        Time.timeScale = 1;
        GS = GameStatus.Playing;
        introUI.SetActive(false);
        mainUI.SetActive(false);
        playUI.SetActive(true);
        finishCompleteUI.SetActive(false);
        finishGameOVerUI.SetActive(false);
    }

    public void UIchangeComplete()
    {
        GS = GameStatus.FinishComplete;
        introUI.SetActive(false);
        mainUI.SetActive(false);
        playUI.SetActive(false);
        finishCompleteUI.SetActive(true);
        finishGameOVerUI.SetActive(false);

        reportText.text = ("Enemy Kill : " + killCount.ToString() + "\nSkill Used : " + skillUsed.ToString() + "\nHP Left : " + (hpLeft*100).ToString() + "% \nScore : " + score.ToString());
        int finalScore = killCount + (skillUsed * 10) + (int)(hpLeft * 100) + score;
        finalScoreText.text = ("Final Score : " + finalScore.ToString());
    }

    public void UIchangeCGameOver()
    {
        GS = GameStatus.FinishGameOver;
        introUI.SetActive(false);
        mainUI.SetActive(false);
        playUI.SetActive(false);
        finishCompleteUI.SetActive(false);
        finishGameOVerUI.SetActive(true);
    }

    public void UseHeal()
    {
        healCooltimer = 0;
    }

    public void UseAttackPowerBuf()
    {
        attackBufCooltimer = 0;
    }

    public void UseDefPowerBuf()
    {
        defBufCooltimer = 0;
    }

    public void UseGrenade()
    {
        grenadeCooltimer = 0;
        grenadeCount--;
    }

    public void UpdateScoreCoin(int _score,int _coin)
    {        
        scoreText.text = _score.ToString();
        coinText.text = _coin.ToString();
    }


    public void OnClickPause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickContinue()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void OnClickQuit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
