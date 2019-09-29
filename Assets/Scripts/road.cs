using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road : MonoBehaviour {

	public Transform transformPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transformPlayer.position.z-transform.position.z>= 100) {
			transform.position += new Vector3 (0, 0, 200);
		}
	}
}
