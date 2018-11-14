using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMover : MonoBehaviour {

    [SerializeField] private float speed = 20f;
	
	void Start () {
        this.GetComponent<Rigidbody2D>().velocity = transform.up * speed;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player2Knife" || collider.tag == "Player1Knife")
        {
            Destroy(collider.gameObject);
        }
    }
}
