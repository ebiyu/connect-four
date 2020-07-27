using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
	[System.NonSerialized] public int col;

	GameManager main;
 
	void Start()
	{
		main = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void onClick()
	{
		main.addCircle(col);
	}
}
