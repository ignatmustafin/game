using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Hero : MonoBehaviour
{
    public GameObject nameTextGO;
    public GameObject hpTextGO; 
 
    public void SetName(string value){
        nameTextGO.GetComponent<TextMeshPro>().text = value;
    }
    public void SetHP(int value){
        hpTextGO.GetComponent<TextMeshPro>().text = value.ToString();
    }
}
