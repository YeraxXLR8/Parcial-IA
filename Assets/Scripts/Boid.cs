using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 _velocity;
    public float maxSpeed;
    public float maxForce;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 desired = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        desired = desired.normalized * maxSpeed;

        ApplyForce(desired);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;

        CheckBounds();
    }
    void ApplyForce(Vector3 force)
    {
        _velocity += force;
    }

    void CheckBounds()
    {
        if (transform.position.x >= 34 || transform.position.x <= -34)
            transform.position = new Vector3(transform.position.x * -0.95f, 0, transform.position.z);
        if (transform.position.z >= 20 || transform.position.z <= -20)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z * -0.95f);
    }
}
