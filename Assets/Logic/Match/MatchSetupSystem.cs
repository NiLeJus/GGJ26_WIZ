using System;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;
    [SerializeField] private PerkData perkData; 

    private void Start()
    {
        OnStartGame();
    }

    public void OnStartGame()
    {
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);
        PerkSystem.Instance.AddPerk(new Perk(perkData));
        GADrawCards gaDrawCards = new (5);
        ActionSystem.Instance.Perform(gaDrawCards);    
    }
}