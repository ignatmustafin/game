using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOStore : MonoBehaviour
{
    public GameObject playerDeck;
    public GameObject enemyDeck;
    public GameObject playerHand;
    public GameObject enemyHand;
    public GameObject playerField1;
    public GameObject playerField2;
    public GameObject playerField3;
    public GameObject playerField4;
    public Hero playerHero;
    public Hero enemyHero;
    public GameObject greenMask;
    public static GOStore Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
