using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        ObjectCollection.Initialize(12, 5);
        ObjectCollection.StartGame(12, 5);
    }

    // Update is called once per frame
    void Update()
    {
    }
}