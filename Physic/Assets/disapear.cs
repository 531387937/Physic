using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Py")
        {
            other.gameObject.GetComponent<Physic>().velocity = Vector3.zero;
            other.gameObject.SetActive(false);
        }
    }
}
