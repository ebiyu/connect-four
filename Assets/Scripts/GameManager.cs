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
	[SerializeField] Text clearText;
	[SerializeField] Image clearTextBackground;

	int frameSize = 100;
	GameObject[,] frameObjects;
	GameObject[,] circleObjects;
	int[,] circleStatus;
	int activePlayer = 0;
	bool finished = false;

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
		if(finished) return;
		for(int i = 0; i < rows; i++){
			if(circleStatus[i, col] == 0){
				circleStatus[i, col] = activePlayer;
				addCircleObject(i, col, activePlayer);
				if(activePlayer == 1){
					activePlayer = 2;
				}else{
					activePlayer = 1;
				}
				int winner = clearCheck();
				if(winner != 0)
				{
					if(winner == 1)
					{
						clearText.text = "Red win";
						clearText.color = Color.red;
					}else{
						clearText.text = "Yellow win";
						clearText.color = Color.yellow;
					}
					finished = true;
					clearText.enabled = true;
					clearTextBackground.enabled = true;
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

	private int clearCheck()
	{
		if(rows < 4 || cols < 4) return 0;


		int[][] wonPosition = new int[4][];
		Debug.Log(wonPosition);

		// check horizontal
		for(int i = 0; i < rows; i++)
		{
			for(int j = 0; j < cols - 3; j++)
			{
				int winner = circleStatus[i, j];
				if(winner == 0) continue;
				bool won = true;
				for(int k = 1; k < 4; k++)
				{
					if(circleStatus[i, j + k] != winner)
					{
						won = false;
					}
				}
				if(won) return winner;
			}
		}

		// check vertical
		for(int i = 0; i < rows - 3; i++)
		{
			for(int j = 0; j < cols; j++)
			{
				int winner = circleStatus[i, j];
				if(winner == 0) continue;
				Debug.Log(winner);
				bool won = true;
				for(int k = 1; k < 4; k++)
				{
					if(circleStatus[i + k, j] != winner)
					{
						won = false;
					}
				}
				if(won) return winner;
			}
		}

		// check diagonal "/"
		for(int i = 0; i < rows - 3; i++)
		{
			for(int j = 0; j < cols - 3; j++)
			{
				int winner = circleStatus[i, j];
				if(winner == 0) continue;
				Debug.Log(winner);
				bool won = true;
				for(int k = 1; k < 4; k++)
				{
					if(circleStatus[i + k, j + k] != winner)
					{
						won = false;
					}
				}
				if(won) return winner;
			}
		}

		// check diagonal "\"
		for(int i = 0; i < rows - 3; i++)
		{
			for(int j = 3; j < cols; j++)
			{
				int winner = circleStatus[i, j];
				if(winner == 0) continue;
				Debug.Log(winner);
				bool won = true;
				for(int k = 1; k < 4; k++)
				{
					if(circleStatus[i + k, j - k] != winner)
					{
						won = false;
					}
				}
				if(won) return winner;
			}
		}


		return 0;
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

		finished = false;

		clearText.enabled = false;
		clearTextBackground.enabled = false;
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
