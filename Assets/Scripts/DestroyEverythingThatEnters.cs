using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEverythingThatEnters : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
