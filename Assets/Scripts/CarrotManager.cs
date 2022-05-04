using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    public List<Carrot> allCarrots = new List<Carrot>();

    public static CarrotManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else DestroyImmediate(gameObject);
    }
    // Update is called once per frame

    public void AddCarrot(Carrot c)
    {
        if (!allCarrots.Contains(c))
            allCarrots.Add(c);
    }
}
