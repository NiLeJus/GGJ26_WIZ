using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
    
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
        StartCoroutine(InitializeAfterDelay());
    }

    private IEnumerator InitializeAfterDelay()  // ← Retour "IEnumerator" obligatoire
    {
        yield return new WaitForSeconds(15f);  // ← Fonctionne maintenant
        
        // Votre code s'exécute après 15s
        HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        CardSystem.Instance.Setup(heroData.Deck);
        PerkSystem.Instance.AddPerk(new Perk(perkData));
        GADrawCards gaDrawCards = new(5);
        ActionSystem.Instance.Perform(gaDrawCards);
    }
}