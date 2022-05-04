using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    private void Awake()
    {
        CarrotManager.instance.AddCarrot(this);
    }
    private void Update()
    {
        //Destroy(gameObject, 6f);
    }
}
