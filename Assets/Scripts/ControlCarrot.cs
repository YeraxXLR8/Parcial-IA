using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCarrot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
