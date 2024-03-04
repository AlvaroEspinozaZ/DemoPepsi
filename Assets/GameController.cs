using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] int LEVELGAME;
    [SerializeField] PlayerController _playerController;
    [SerializeField] SlotController[] Options;
    [Header("Pause")]
    [SerializeField] GameObject PanelPause;
    [SerializeField] Button btnPause;
    [SerializeField] SlotController[] SlotsPlayer;
    [SerializeField] SlotController[] SlotsEnemy;
    [Header("WIN")]
    [SerializeField] GameObject PanelWin;
    [Header("Lose")]
    [SerializeField] GameObject PanelLose;
    int count;
    bool isPause = false;
    private void Awake()
    {
        btnPause.onClick.AddListener(delegate () { ScreamPause(); });
        StartCoroutine(LevelGame(LEVELGAME));      

        PressButtons();


    }
    private void Update()
    {
  
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
            
            Debug.Log("Haber COmparando ids: "+ count );
            SlotsPlayer[count].GetComponent<SpriteRenderer>().sprite = Options[4].GetComponent<SpriteRenderer>().sprite;
            SlotsPlayer[count].GetComponent<SpriteRenderer>().color = Options[4].GetComponent<SpriteRenderer>().color;
            ++count;
            if (SlotsPlayer[count].GetComponent<SpriteRenderer>().sprite == Options[4].GetComponent<SpriteRenderer>().sprite 
                && SlotsPlayer[count].GetComponent<SpriteRenderer>().color == Options[4].GetComponent<SpriteRenderer>().color
                )
            {
                PanelWin.SetActive(true);
                isPause = true ;
            }
        }
    }

    IEnumerator LevelGame(int level)
    {
        Debug.Log(LimitRamdomSlots());
        for (int i = 0; i < LimitRamdomSlots(); i++)
        {       
                int j = Random.Range(0, level);
                SlotsPlayer[i].GetComponent<SpriteRenderer>().color = Options[j].GetComponent<SpriteRenderer>().color;
                SlotsPlayer[i].GetComponent<SpriteRenderer>().sprite = Options[j].GetComponent<SpriteRenderer>().sprite;
                SlotsPlayer[i].id = Options[j].id;
            yield return new WaitForSecondsRealtime(0.25f);
        }
        for (int i = 0; i < _playerController.btnsGame.Length; i++)
        {
            _playerController.btnsGame[i].interactable = true;
        }
    }
    public int LimitRamdomSlots()
    {
        int x = Random.Range(1,8) ;
        return x;
    }
    public void ScreamPause()
    {
        PanelPause.SetActive(true);
    }


    IEnumerator EnemySlots()
    {
        yield return new WaitForSecondsRealtime(0.9f);
        if (!isPause)
        {

        }
        StartCoroutine(EnemySlots());
    }
   
}
 