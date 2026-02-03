using System.Collections.Generic;
using UnityEngine;


public class FonctionMatchSetupSystem : MonoBehaviour
{
//Remplacer tout le contenue par :

[SerializeField]private HeroData heroData;
    private void Start()
    {
        HeroSystem.Instance.Setup(heroData);
        //CardViewHoverSystem.Instance.Setup(heroData.Deck);
        //GADrawCard gaDrawCard = new(5);
        //ActionSystem.Instance.Perform(gaDrawCard);
    }
}
