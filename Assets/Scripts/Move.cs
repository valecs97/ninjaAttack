using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Margins{
	private float marginXTop = 20.0f,marginYTop=8.0f,marginXBottom=0.0f,marginYBottom = 0.0f;
    
    public void resetDefault()
    {
        marginXTop = 20.0f;
        marginYTop = 8.0f;
        marginXBottom = 0.0f;
        marginYBottom = 0.0f;
    }

	public void setMarginXTop(float marginXTop)
	{
		this.marginXTop = marginXTop;
	}

	public void setMarginYTop(float marginYTop)
	{
		this.marginYTop = marginYTop;
	}

	public void setMarginXBottom(float marginXBottom)
	{
		this.marginXBottom = marginXBottom;
	}

	public void setMarginYBottom(float marginYBottom)
	{
		this.marginYBottom = marginYBottom;
	}

	public float getMarginXTop()
	{
		return marginXTop;
	}

	public float getMarginYTop()
	{
		return marginYTop;
	}

	public float getMarginXBottom()
	{
		return marginXBottom;
	}

	public float getMarginYBottom()
	{
		return marginYBottom;
	}

    public string toString()
    {
        return marginXTop + " " + marginXBottom + " " + marginYTop + " " + marginYBottom;
    }
}

public class Move : MonoBehaviour {

	// Use this for initialization


	public int speed;
	public float powerJump;
	public float jumpHeight;

	private bool reachedMaxJump = false;
	private float lastPositionY= 0.0f,lastPositionX=0.0f;
    private float initialYJump = 0.0f;
	private Margins margins = new Margins();

	Rigidbody myBody;
	void Start () {
		myBody = this.GetComponent<Rigidbody> ();
	}

	void FixedUpdate () {
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		bool jump = checkJump();
		move (moveHorizontal, jump);
		checkIfMaxJump ();
		changeColorWhenMove ();
        //flipImage();
		saveLastPosition ();
        Debug.Log(GetComponent<Transform>().position.ToString());
        
	}

    private void flipImage()
    {
        Quaternion target = GetComponentInChildren<Rigidbody>().rotation;
        if (lastPositionX > myBody.position.x)
            target = Quaternion.Euler(0, 180, 0);
        else if (lastPositionX < myBody.position.x)
            target = Quaternion.Euler(0, 0, 0);
        GetComponentInChildren<Rigidbody>().rotation = target;
    }

	private void saveLastPosition()
	{
		lastPositionY = myBody.position.y;
		lastPositionX = myBody.position.x;
	}

	private void changeColorWhenMove()
	{
		//if (lastPositionX == myBody.position.x && lastPositionY == myBody.position.y) {
		//	GetComponent<Renderer> ().material.color = UnityEngine.Color.black;
		//}
		//else
		//	GetComponent<Renderer> ().material.color = UnityEngine.Color.yellow;
	}

	private bool checkJump()
	{
        if (Input.GetKeyDown("space"))
            initialYJump = margins.getMarginYBottom();
        return Input.GetKey ("space") && !reachedMaxJump?true:false;
	}

	private void checkIfMaxJump()
	{
        if (myBody.position.y >= initialYJump + jumpHeight || lastPositionY > myBody.position.y || myBody.position.y >= margins.getMarginYTop())
        {
            reachedMaxJump = true;
            initialYJump = margins.getMarginYBottom();
            //Debug.Log(margins.getMarginYBottom());
            //Debug.Log(margins.getMarginYTop());
        }
        else if (myBody.position.y <= margins.getMarginYBottom())
            reachedMaxJump = false;
	}

	private void move(float direction,bool jump)
	{
        Vector2 movement = new Vector2 (direction, jump?powerJump:(myBody.position.y <= margins.getMarginYBottom()?0.0f:-powerJump));
		myBody.velocity = movement*speed;
        GetComponent<Transform>().position = new Vector3
			(
				Mathf.Clamp(myBody.position.x,margins.getMarginXBottom(),margins.getMarginXTop()),
				Mathf.Clamp(myBody.position.y,margins.getMarginYBottom(),margins.getMarginYTop()),
                0
			);
    }

	public Margins getMargins()
	{
		return margins;
	}

	public float getJumpHeight()
	{
		return jumpHeight;
	}
}
