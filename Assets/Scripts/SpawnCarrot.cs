using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarrot : MonoBehaviour
{
    public GameObject carrotPrefab;
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;

            float x = Random.Range(34f, -34f);
            float z = Random.Range(20f, -20f);
            Vector3 position = new Vector3(x, 0, z);
            Quaternion rotation = new Quaternion();


            Instantiate(carrotPrefab,position,rotation);
        }
    }
}
