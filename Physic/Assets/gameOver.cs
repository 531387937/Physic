using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour {
    public Vector3 tr;
    public GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!player.active)
        {
            player.transform.position = tr;
            player.SetActive(true);
        }
	}
}
