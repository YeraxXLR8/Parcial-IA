using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public List<Boid> allBoids = new List<Boid>();

    public static BoidManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else DestroyImmediate(gameObject);

    }

    public void AddBoid(Boid b)
    {
        if (!allBoids.Contains(b))
            allBoids.Add(b);
    }
}
