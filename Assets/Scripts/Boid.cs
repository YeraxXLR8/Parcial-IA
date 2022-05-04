using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 _velocity;
    public float maxSpeed;
    public float maxForce;

    public float viewRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        BoidManager.instance.AddBoid(this);

        Vector3 desired = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        desired = desired.normalized * maxSpeed;

        ApplyForce(desired);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;

        Separation();
        CheckBounds();
    }
    void ApplyForce(Vector3 force)
    {
        _velocity += force;
    }

    void Separation()
    {
        Vector3 desired = new Vector3();

        foreach (var boid in BoidManager.instance.allBoids)
        {
            if (boid == this) continue;

           Vector3 distance = boid.transform.position - transform.position;
            if (distance.magnitude < viewRadius)
            {
                desired += distance; 
            }
        }

        if (desired == Vector3.zero) return;
        desired *= -1;
        desired = desired.normalized * maxSpeed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce / 10);

        ApplyForce(steering);
    }

    void CheckBounds()
    {
        if (transform.position.x >= 34 || transform.position.x <= -34)
            transform.position = new Vector3(transform.position.x * -0.95f, 0, transform.position.z);
        if (transform.position.z >= 20 || transform.position.z <= -20)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z * -0.95f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,viewRadius);
    }
}
