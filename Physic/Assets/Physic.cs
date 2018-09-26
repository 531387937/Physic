using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physic : MonoBehaviour {
    public Vector3 Force;
    public Vector3 velocity;
    public float maxSpeed;
    public float mass=1;
    Vector3 speed;
    float m;
    bool col = false;
    public float DynamicFriction;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 acceleration = Force / mass;
        velocity += acceleration * Time.deltaTime;
        if(velocity.magnitude>maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        transform.position += velocity * Time.deltaTime;
	}
    private void FixedUpdate()
    {
        velocity -= velocity.normalized * DynamicFriction * Time.fixedDeltaTime;

    }
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.tag == "Py")
        {
            m = coll.gameObject.GetComponent<Physic>().mass;
            speed = coll.gameObject.GetComponent<Physic>().velocity;
            //if (speed == Vector3.zero && mass == m)
            //{
            //    coll.gameObject.GetComponent<Physic>().velocity = velocity.magnitude * Mathf.Cos(ang) * dir.normalized;
            //    velocity = velocity.magnitude * Mathf.Sin(ang) * (Quaternion.AngleAxis((Mathf.PI / 2 - ang) * 180 / Mathf.PI, Vector3.up) * velocity).normalized;
            //}
            ////Vector3 a = velocity * Mathf.Cos(ang);
            //else if (ang == 0 || ang == Mathf.PI) 
            
                StartCoroutine(colli(coll.gameObject));
        }
        if(coll.transform.tag=="WallX")
        {
            velocity.x = -velocity.x;
        }
        if (coll.transform.tag == "WallY")
        {
            velocity.z = -velocity.z;
        }
        //Vector3 b = velocity * Mathf.Sin(ang);
        //velocity = a + b;coll.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    IEnumerator colli(GameObject coll)
    {
        yield return null;
        Oblique_touch(coll.gameObject);
        //velocity = ((mass - m) * velocity + 2 * m * speed) / (mass + m);
       
    }
    void Oblique_touch(GameObject coll)
    {
        Vector2 s = new Vector2(transform.position.x - coll.transform.position.x,
            transform.position.z - coll.transform.position.z).normalized;
        Vector2 t = new Vector2(transform.position.x - coll.transform.position.x,
            transform.position.z - coll.transform.position.z).normalized;
        t = new Vector2(t.y, -t.x);

        Vector2 v1 = new Vector2(velocity.x, velocity.z);
        Vector2 v2 = new Vector2(speed.x, speed.z);

        float v1s = Vector2.Dot(v1, s);
        float v1t = Vector2.Dot(v1, t);
        float v2s = Vector2.Dot(v2, s);
        float v2t = Vector2.Dot(v2, t);

        float m1 = mass;
        float m2 = m;

        float temp_v1s = ((m1 - m2) * v1s + 2 * m2 * v2s) / (m1 + m2);
        v2s = ((m2 - m1) * v2s + 2 * m1 * v1s) / (m1 + m2);
        v1s = temp_v1s;

        Vector2 v1tVector = t * v1t;
        Vector2 v1sVector = s * v1s;
        Vector2 v2tVector = t * v2t;
        Vector2 v2sVector = s * v2s;

        Vector2 v1_new = v1tVector + v1sVector;
        Vector2 v2_new = v2tVector + v2sVector;

        velocity = new Vector3(v1_new.x, 0, v1_new.y);
    }
}
