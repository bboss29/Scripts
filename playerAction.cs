using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAction : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col){
		Debug.Log(col.gameObject.name);
		Debug.Log(col.otherCollider.name);
	}
}
