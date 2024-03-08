using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    public Animator anit;
    public int id = 0;
    
    public void Abierto()
    {
        anit.SetTrigger("Abrir");
    }
}
