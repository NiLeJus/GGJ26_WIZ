using System;
using TMPro;
using UnityEngine;

public class UI_Mana : MonoBehaviour
{
    //A ajouter au gameObject ManaUI

    [SerializeField] private TMP_Text mana; 
    public void UpdateManaText(int currentMana)
    {
        mana. text = currentMana. ToString() ;
    }

   
}