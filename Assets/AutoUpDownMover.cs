using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoUpDownMover : MonoBehaviour {

    [SerializeField] private float distance = 0;
    [SerializeField] private bool startUp = true;
    [SerializeField] private Vector2 speed = new Vector2(0,2);

    private Vector2 initialPosition;
    private Rigidbody2D myBody;

	// Use this for initialization
	void Start () {
        initialPosition = GetComponent<Rigidbody2D>().position;
        myBody = GetComponent<Rigidbody2D>();
        speed = startUp ? speed : -speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (myBody.position.y > initialPosition.y + distance || myBody.position.y < initialPosition.y - distance)
            speed = -speed;
        myBody.velocity = speed;
        Mathf.Clamp(GetComponent<Rigidbody2D>().rotation, -45, 45);
	}
}
