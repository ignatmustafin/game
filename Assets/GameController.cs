using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInfo
{
    public string Name { get; set; }
    public int HP { get; set; }
    public CardInfo[] Cards { get; set; }

}
public class GameController : MonoBehaviour
{
    public GameObject cardPrefab;
    void Start()
    {
        PlayerInfo playerInfo = getPlayerInfo();
        Hero playerHero = GOStore.Instance.playerHero;
        playerHero.SetName(playerInfo.Name);
        playerHero.SetHP(playerInfo.HP);

        for (int i = 0; i < playerInfo.Cards.Length; i++) 
        {
            GameObject newCardInstance = getNewCardInstance(playerInfo.Cards[i]);

            CardMover.MoveToPlayerDeck(newCardInstance);
            CardMover.FlipFrontDown(newCardInstance);
        }

        PlayerInfo enemyInfo = getEnemyInfo();
        Hero enemyHero = GOStore.Instance.enemyHero;
        enemyHero.SetName(enemyInfo.Name);
        enemyHero.SetHP(enemyInfo.HP);

        
        for (int i = 0; i < enemyInfo.Cards.Length; i++) 
        {
            GameObject newCardInstance = getNewCardInstance(enemyInfo.Cards[i]);

            CardMover.MoveToEnemyDeck(newCardInstance);
            CardMover.FlipFrontDown(newCardInstance);
        }

        

    }

    GameObject getNewCardInstance(CardInfo cardInfo){
        GameObject newCardInstance = Instantiate(cardPrefab);
        Card cardScript = newCardInstance.GetComponent<Card>();
        cardScript.SetCardInfo(cardInfo);
        
        return newCardInstance;
    }

    PlayerInfo getPlayerInfo(){
        PlayerInfo player = new PlayerInfo();
        player.Name = "Sasha";
        player.HP = 30;

        CardInfo pion = new CardInfo();
        pion.name = "Pion";
        pion.atk = 1;
        pion.hp = 1;
        pion.manacost = 1;

        player.Cards = new CardInfo[] { pion };

        return player;
    }


    PlayerInfo getEnemyInfo(){
        PlayerInfo enemy = new PlayerInfo();
        enemy.Name = "Ignat";
        enemy.HP = 30;

        CardInfo footman = new CardInfo();
        footman.name = "Footman";
        footman.atk = 2;
        footman.hp = 2;
        footman.manacost = 2;

        enemy.Cards = new CardInfo[] { footman };
        
        return enemy;
    }


}
