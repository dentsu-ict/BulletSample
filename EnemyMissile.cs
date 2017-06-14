using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour {

	private Vector2 Camera_max,Camera_min;
	private Rigidbody2D _rigidbody=null;
	private Vector2 SP;

	void Awake(){
		_rigidbody=GetComponent<Rigidbody2D>();
	}

	void Start(){
	    Camera_min = Camera.main.ViewportToWorldPoint (Vector2.zero);
    	Camera_max = Camera.main.ViewportToWorldPoint (Vector2.one);
	}

	public void SetMissile(Vector2 pos,Vector2 direction,float speed){
		transform.position=pos;
		SP=speed*direction;
	}
	
	void FixedUpdate () {
		_rigidbody.velocity=SP;
		Vector2 pos=transform.position;
		if(pos.x>Camera_max.x || pos.x<Camera_min.x || pos.y>Camera_max.y || pos.y<Camera_min.y){
			Destroy(gameObject);
		}				
	}
}
