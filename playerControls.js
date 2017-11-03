#pragma strict

var moveLeft : KeyCode;
var moveRight : KeyCode;

var speed : float;

function Update () {
	if (Input.GetKey(moveRight)){
		GetComponent.<Rigidbody2D>().velocity.x = speed;
	} else if (Input.GetKey(moveLeft)){
		GetComponent.<Rigidbody2D>().velocity.x = -speed;
	} else {
		GetComponent.<Rigidbody2D>().velocity.x = 0;
	}
}