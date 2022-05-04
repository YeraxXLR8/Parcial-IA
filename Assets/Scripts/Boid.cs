using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 _velocity;
    public float maxSpeed;
    public float maxForce;

    public float viewRadius;

    public float separationWeight;
    public float alignmentWeight;
    public float cohesionWeight;

    
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

        CheckBounds();
        ApplyForce(Separation() * separationWeight);
        ApplyForce(Alignment() * alignmentWeight);
        ApplyForce(Cohesion() * cohesionWeight);
    }

    Vector3 Separation()
    {
        Vector3 desired = new Vector3();

        foreach (var boid in BoidManager.instance.allBoids)
        {
            if (boid == this) continue;

           Vector3 distance = boid.transform.position - transform.position;
            if (distance.magnitude < viewRadius)
            {
                desired.x += distance.x;
                desired.z += distance.z;
            }
        }

        if (desired == Vector3.zero) return desired;
        desired *= -1;
        desired = desired.normalized * maxSpeed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce / 10);

        return (steering);
    }

    void CheckBounds()
    {
        if (transform.position.x >= 34 || transform.position.x <= -34)
            transform.position = new Vector3(transform.position.x * -0.95f, 0, transform.position.z);
        if (transform.position.z >= 20 || transform.position.z <= -20)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z * -0.95f);
    }

    Vector3 Alignment ()
    {
        Vector3 desired = new Vector3();
        int count = 0;

        foreach (var item in BoidManager.instance.allBoids)
        {
            if (item == this) continue;

            if (Vector3.Distance(item.transform.position, transform.position) < viewRadius)
            {
                desired += item._velocity;
                desired.y = 0;
                count++;
            }

        }

        if (count == 0) return desired;

        desired /= count;

        return CalculateSteering(desired);
    }

    Vector3 Cohesion()
    {
        Vector3 desired = new Vector3();
        int count = 0;

        foreach (var item in BoidManager.instance.allBoids)
        {
            if (item == this) continue;

            if (Vector3.Distance(item.transform.position,transform.position) < viewRadius)
            {
                desired += item.transform.position;
                desired.y = 0;
                count++;
            }
        }

        if (count == 0) return desired;
        desired /= count;
        desired = desired - transform.position;

        return (CalculateSteering(desired));
    }
    void ApplyForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, maxSpeed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,viewRadius);
    }

    Vector3 CalculateSteering(Vector3 desired)
    {
        desired = desired.normalized * maxSpeed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce / 10);

        return steering;
    }
}
