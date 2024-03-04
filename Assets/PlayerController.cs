using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject[] SlotsPlayer;
    [SerializeField] public Button[] btnsGame;
    public int idPress;
    private void Awake()
    {
        DelegateButtons();
        for (int i = 0; i < btnsGame.Length; i++)
        {
            btnsGame[i].interactable = false;
        }
    }

    void Update()
    {
        
    }
    void DelegateButtons()
    {
        for (int i = 0; i < btnsGame.Length; i++)
        {
            int id = i;
            btnsGame[i].onClick.AddListener(delegate () { PressButtons(id); });
        }
    }
    public void PressButtons(int id)
    {       
       Debug.Log("Soy button "+ id);
        idPress = id;
    }
}



