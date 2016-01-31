﻿using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

	public GameObject[] throwables;

	void Start () {
		InvokeRepeating ("spawnThings", 5f, 5f);
	}

	void spawnThings(){
		Vector3 randomVect = new Vector3 ( Random.Range(-8, 8), 0, Random.Range(-8, 8) );
		Instantiate (throwables [Mathf.FloorToInt (Random.Range (0, throwables.Length))], transform.position+randomVect, Quaternion.identity);
	}
}