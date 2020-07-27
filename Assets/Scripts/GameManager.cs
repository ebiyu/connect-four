using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	[SerializeField] int cols = 7;
	[SerializeField] int rows = 6;
	[SerializeField] GameObject framePrefab;
	[SerializeField] GameObject circlePrefab;
	[SerializeField] Transform frameParent;
	[SerializeField] Transform circleParent;
	[SerializeField] int width = 1024;
	[SerializeField] int height = 768;
	[SerializeField] Color firstPlayerColor;
	[SerializeField] Color secondPlayerColor;
	[SerializeField] Text turnText;

	int frameSize = 100;
	GameObject[,] frameObjects;
	GameObject[,] circleObjects;
	int[,] circleStatus;
	int activePlayer = 0;

	// Start is called before the first frame update
	void Start()
	{
		initialize();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void addCircle(int col)
	{
		for(int i = 0; i < rows; i++){
			if(circleStatus[i, col] == 0){
				circleStatus[i, col] = activePlayer;
				addCircleObject(i, col, activePlayer);
				if(activePlayer == 1){
					activePlayer = 2;
				}else{
					activePlayer = 1;
				}
				displayTurn();
				break;
			}
		}
	}

	private void addCircleObject(int row, int col, int player)
	{
		GameObject circle = Instantiate(circlePrefab);

		circle.transform.SetParent(circleParent, false);
		circle.transform.localScale = circle.transform.localScale * frameSize;
		circle.transform.localPosition = new Vector3((col - cols / 2.0f + 0.5f) * frameSize, (rows / 2.0f - 0.5f) * frameSize, 0);

		if(player == 1){
			circle.GetComponent<Renderer>().material.color = Color.red;
		}else{
			circle.GetComponent<Renderer>().material.color = Color.yellow;
		}

		Circle obj = circle.GetComponent<Circle>();
		obj.targetY = (row - rows / 2.0f + 0.5f) * frameSize;

		circleObjects[row, col] = circle;
	}


	public void reset()
	{
		for (int i = 0; i < rows; i++)
		{
			for(int j = 0; j < cols; j++)
			{
				if(circleObjects[i, j]){
					Destroy(circleObjects[i, j]);
					Destroy(frameObjects[i, j]);
				}
			}
		}
		initialize();
	}

	public void initialize()
	{
		frameSize = 350 / rows;
		frameObjects = new GameObject[rows, cols];
		circleObjects = new GameObject[rows, cols];
		circleStatus = new int[rows, cols];

		for (int i = 0; i < rows; i++)
		{
			for(int j = 0; j < cols; j++)
			{
				GameObject frame = Instantiate(framePrefab);
				frame.transform.SetParent(frameParent, false);
				frame.transform.localScale = frame.transform.localScale * frameSize;
				frame.transform.localPosition = new Vector3((j - cols / 2.0f + 0.5f) * frameSize, (i - rows / 2.0f + 0.5f) * frameSize, 0);

				Frame obj = frame.GetComponent<Frame>();
				obj.col = j;

				frameObjects[i, j] = frame;
			}
		}

		activePlayer = 1;
		displayTurn();
	}

	private void displayTurn(){
		if(activePlayer == 1){
			turnText.text = "Red turn";
			turnText.color = Color.red;
		}else{
			turnText.text = "Yellow turn";
			turnText.color = Color.yellow;
		}
	}
}
