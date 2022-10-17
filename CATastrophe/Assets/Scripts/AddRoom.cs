using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

	private RoomTemplate templates;

	void Start(){

		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
		templates.rooms.Add(this.gameObject);
	}
}
