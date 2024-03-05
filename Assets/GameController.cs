using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text txtScore;
    [SerializeField] Text txtCoins;
    [SerializeField] Points _score;
    [SerializeField] Points _coins;
    [Header("Gameplay")]
    [SerializeField] GameObject slotsRight;
    [SerializeField] GameObject slotsLeft;
    [SerializeField] int LEVELGAME;
    [SerializeField] PlayerController _playerController;
    [SerializeField] SlotController[] Options;
    [Header("Pause")]
    [SerializeField] GameObject PanelPause;
    [SerializeField] Button[] btnPause;
    [SerializeField] SlotController[] SlotsPlayer;
    [SerializeField] SlotController[] SlotsEnemy;
    [Header("WIN")]
    [SerializeField] GameObject PanelWin;
    [Header("Lose")]
    [SerializeField] GameObject PanelLose;
    int count=0,countEnemy=0,limitSlots=0,coins=0,score =0,tmp=0;
    float x=0, y=0;
    float m = 0, n = 0;
    bool isPause = false;

    private void Awake()
    {
        txtCoins.text = "" + _coins.ScoreData;
        txtScore.text = "Score: " + _score.ScoreData;
        for (int i =0; i< btnPause.Length; i++)
        {
            btnPause[i].onClick.AddListener(delegate () { ScreamPause(); });
        }
        StartCoroutine(LevelGame(LEVELGAME));      

        PressButtons();


    }
    private void Update()
    {
        if (count > 7)
        {
            count = 7;
        }
        if (countEnemy > 7)
        {
            countEnemy = 7;
        }      
    }
    void PressButtons()
    {
        for (int i = 0; i < _playerController.btnsGame.Length; i++)
        {
            _playerController.btnsGame[i].onClick.AddListener(delegate () { NextButton(); });
        }      
    }
    void NextButton()
    {          
        
        if(SlotsPlayer[count].id == _playerController.idPress)
        {            
            //Debug.Log("Haber COmparando ids: "+ count );
            SlotsPlayer[count].GetComponent<SpriteRenderer>().sprite = Options[4].GetComponent<SpriteRenderer>().sprite;
            SlotsPlayer[count].GetComponent<SpriteRenderer>().color = Options[4].GetComponent<SpriteRenderer>().color;
            ++count;
            if (SlotsPlayer[count].GetComponent<SpriteRenderer>().sprite == Options[4].GetComponent<SpriteRenderer>().sprite 
                && SlotsPlayer[count].GetComponent<SpriteRenderer>().color == Options[4].GetComponent<SpriteRenderer>().color
                )
            {
                StartCoroutine(WinEvent());
            }
        }
    }
    IEnumerator LevelGame(int level)
    {
        limitSlots = LimitRamdomSlots();
        for (int i = 0; i < limitSlots; i++)
        {       
            int j = Random.Range(0, level);

            SlotsPlayer[i].GetComponent<SpriteRenderer>().color = Options[j].GetComponent<SpriteRenderer>().color;
            SlotsPlayer[i].GetComponent<SpriteRenderer>().sprite = Options[j].GetComponent<SpriteRenderer>().sprite;
            SlotsPlayer[i].id = Options[j].id;

            SlotsEnemy[i].GetComponent<SpriteRenderer>().color = Options[j].GetComponent<SpriteRenderer>().color;
            SlotsEnemy[i].GetComponent<SpriteRenderer>().sprite = Options[j].GetComponent<SpriteRenderer>().sprite;
            yield return new WaitForSecondsRealtime(0.35f);
        }
        for (int i = 0; i < _playerController.btnsGame.Length; i++)
        {
            _playerController.btnsGame[i].interactable = true;
        }
        StartCoroutine(EnemySlots());
    }
    public int LimitRamdomSlots()
    {
        int x = Random.Range(1,8) ;
        return x;
    }
    public void ScreamPause()
    {
        if (!isPause)
        {
            PanelPause.SetActive(true);
            isPause = true;
        }
        else
        {
            PanelPause.SetActive(false);
            isPause = false;
        }
    }  
    IEnumerator EnemySlots()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        if (!isPause)
        {
            SlotsEnemy[countEnemy].GetComponent<SpriteRenderer>().color = Options[4].GetComponent<SpriteRenderer>().color;
            SlotsEnemy[countEnemy].GetComponent<SpriteRenderer>().sprite = Options[4].GetComponent<SpriteRenderer>().sprite;
            ++countEnemy;
        }
        if (SlotsEnemy[countEnemy].GetComponent<SpriteRenderer>().sprite == Options[4].GetComponent<SpriteRenderer>().sprite
                      && SlotsEnemy[countEnemy].GetComponent<SpriteRenderer>().color == Options[4].GetComponent<SpriteRenderer>().color
                      )
        {
            StartCoroutine(LoseEvent());
        }
        StartCoroutine(EnemySlots());
        //for (int i = 0; i < limitSlots; i++)
        //{
        //    if (!isPause)
        //    {
        //        SlotsEnemy[i].GetComponent<SpriteRenderer>().color = Options[4].GetComponent<SpriteRenderer>().color;
        //        SlotsEnemy[i].GetComponent<SpriteRenderer>().sprite = Options[4].GetComponent<SpriteRenderer>().sprite;
        //        ++countEnemy;
        //        Debug.Log("ANtes de..." + countEnemy);
        //        yield return new WaitForSecondsRealtime(0.9f);
        //        Debug.Log("LUEGO de..." + countEnemy);
        //    }
        //}
    }

    IEnumerator WinEvent()
    {
        ActualizarScores();
        for (int i = 0; i < _playerController.btnsGame.Length; i++)
        {
            _playerController.btnsGame[i].interactable = false;
        }
        slotsRight.SetActive(false);
        slotsLeft.SetActive(false);
        isPause = true;        
        yield return new WaitForSecondsRealtime(1f);
        PanelWin.SetActive(true);        
    }
    IEnumerator LoseEvent()
    {
        for(int i = 0; i < _playerController.btnsGame.Length; i++)
        {
            _playerController.btnsGame[i].interactable = false;
        }
        slotsRight.SetActive(false);
        slotsLeft.SetActive(false);
        isPause = true;
        yield return new WaitForSecondsRealtime(1f);
        PanelLose.SetActive(true);
    }
    IEnumerator ChangedData()
    {
        if(tmp < _score.ScoreData)
        {
            x += 1;
            y = Mathf.Clamp(x, _coins.ScoreData - coins, _coins.ScoreData);
            txtCoins.text = "" + (int)y;

            m += 1;
            n = Mathf.Clamp(m, _score.ScoreData - score, _score.ScoreData);
            txtScore.text = "Score: " + (int)n;
            yield return new WaitForSecondsRealtime(0.015f);
        }
        StartCoroutine(ChangedData());
    }
    void ActualizarScores()
    {
        tmp = _score.ScoreData;
        int plusScore = Random.Range(8, 30);
        int plusCoins = Random.Range(5, 16);
        score = (limitSlots * plusScore);
        coins = (limitSlots * plusCoins);
        _coins.ScoreData = _coins.ScoreData + coins;
        _score.ScoreData = _score.ScoreData + score;
        StartCoroutine(ChangedData());
    }
}
 