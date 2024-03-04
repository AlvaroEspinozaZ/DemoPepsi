using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] Animator anit;
    public int id = 0;
    private void Awake()
    {
        anit = GetComponent<Animator>();        
    }
}
