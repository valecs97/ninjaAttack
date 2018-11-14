using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainColliderBoundaries : MonoBehaviour {

	private GameObject player1;
	private Move player1Class;
	private Margins player1Margins;

	void Start () {
		player1 = GameObject.FindWithTag ("Player1");
		player1Class = player1.GetComponent<Move> ();
		player1Margins = player1Class.getMargins ();
		//Debug.Log (player1Margins);
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider player) {
		if (player.tag == "Player1") {
			player1Margins.setMarginYBottom (gameObject.GetComponent<Transform>().position.y + 1);
            player1Margins.setMarginYTop(GetComponent<Transform>().position.y + GetComponent<BoxCollider>().size.y+ 1);
        }
    }

    void OnTriggerExit(Collider player)
    {
        player1Margins.resetDefault();
    }
}
