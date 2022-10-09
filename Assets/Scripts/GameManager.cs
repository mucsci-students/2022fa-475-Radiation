using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{	
	// Place holders to allow connecting to other objects
	public Transform actualSpawn;
  public GameObject actualSpawnObject;
  public Transform[] spawnPoints;
  public Transform checkpoint;
  public Upgrades[] upgrades;
  
  public GameObject player;
  public PlayerData player_data;

  
  public BossData boss_data;

  public GameObject boss;
  public TurretCommand[] turrets;
  public GameUIs gameUIs;

  public GameData checkpointData;
  public KeyManager keyData;

	// Flags that control the state of the game
	private float elapsedTime = 0;
	private bool isRunning = false;
	private bool isFinished = false;
  private bool isBossDefeated = false;
  private bool isPlayerDead = false;
  private bool isUpgrading = false;
  private bool isPaused = false;
  private bool breakingSequence = false;

  private bool spawnAtCheckpoint = false;
  private bool isKeyNotUpgrade = false;


  // Bools for the game modifiers


	// So that we can access the player's controller from this script
	private FirstPersonController fpsController;

  private float SequenceTime;
  

  // Default functions
  /*********************************************************/

	// Use this for initialization
	void Start ()
	{
		//Tell Unity to allow character controllers to have their position set directly. This will enable our respawn to work
		Physics.autoSyncTransforms = true;

		// Finds the First Person Controller script on the Player
		fpsController = player.GetComponent<FirstPersonController> ();
	
		// Disables controls at the start.
		fpsController.enabled = false;

    checkpointData.t_status = new bool[turrets.Length];
	}

  // Update is called once per frame
	void Update ()
	{
    
		// Add time to the clock if the game is running
		if (isRunning)
		{
			elapsedTime = elapsedTime + Time.deltaTime;
      Time.timeScale = 1f;
		}
    else
    Time.timeScale = 0f;

    if(isKeyNotUpgrade)
    {
      isKeyNotUpgrade = false;
    }
	}


  //Run-game functions
  /*********************************************************/

	//This resets to game back to the way it started
	private void StartGame()
	{
    Console.WriteLine("Test");
		elapsedTime = 0;
		isRunning = true;
		isFinished = false;
    isPlayerDead = false;
    spawnAtCheckpoint = false;
    isBossDefeated = false;
    player_data.SetPlayerData(500, 0, 0, 0);
    boss_data.resetBossHealth();
    player_data.updateUI();
    assignUpgrades();
    keyData.turnOffAllKeys();

    int s_randomizer = UnityEngine.Random.Range(0, spawnPoints.Length);
    PositionSpawn(spawnPoints[s_randomizer]);
    
    PositionUpgrades();

    // Move the player to the spawn point, and allow it to move.
    PositionPlayer();
		fpsController.enabled = true;
	}



  //Runs if player dies and continues
  void Respawn()
  {
    isRunning = true;
    isFinished = false;
    isPlayerDead = false;
    if(!spawnAtCheckpoint)
    {
      player_data.SetPlayerData(500, 0, 0, 0);
      PositionPlayer();
      reviveAllTurrets();
    }
    else
    {
      loadCheckpointData();
      SendPlayerToCheckpoint();
    }
    boss_data.resetBossHealth();
    fpsController.enabled = true;
    gameUIs.respawnPlayer();
  }


  void Resume()
  {
    isRunning = true;
    isFinished = false;
    //isPlayerDead = false;
    isUpgrading = false;
    breakingSequence = false;
    fpsController.enabled = true;
  }


  public void unpause()
  {
    isPaused = false;
    isRunning = true;
    fpsController.enabled = true;
  }


  
  //Positioning functions
  /*********************************************************/

	public void PositionSpawn(Transform spawn)
	{
		actualSpawnObject.transform.position = spawn.position;
		actualSpawnObject.transform.rotation = spawn.rotation;
	}

	//Runs when the player needs to be positioned back at the spawn point
	public void PositionPlayer()
	{
		player.transform.position = actualSpawn.position;
		player.transform.rotation = actualSpawn.rotation;
	}
  
  //Makes Player spawn at checkpoint
  void SendPlayerToCheckpoint()
  {
    player.transform.position = checkpoint.position;
  }
  
  public void TeleportPlayer(Transform destination)
  {
    player.transform.position = destination.position;
    player.transform.rotation = destination.rotation;
  }
  
  void PositionUpgrades()
  {
    for (int i = 0; i < upgrades.Length; i++)
    {
      upgrades[i].respawnUpgrade();
    }
  }
  //Suspend-game functions
  /*********************************************************/

	// Runs when the player enters the finish zone
	public void FinishedGame()
	{
		isRunning = false;
		isFinished = true;
		fpsController.enabled = false;
	}


  public void BossDefeated()
  {
    isRunning = false;
    isBossDefeated = true;
    fpsController.enabled = false;
  }


	public void KillPlayer()
	{
    isPlayerDead = true;
    isRunning = false;
    fpsController.enabled = false;
	}


  //Pauses the game to notify the player that they've obtained an upgrade.
  public void UpgradeObtained()
  {
    //if(keyData.SequenceOn)
    //  keyData.turnOffSecretKeys();
    isUpgrading = true;
    isRunning = false;
    fpsController.enabled = false;
  }


  public void pauseGame()
  {
    isRunning = false;
    isPaused = true;
    fpsController.enabled = false;
  }

  public void breakSequence()
  {/*
    keyData.turnOffSecretKeys();
    isRunning = false;
    breakingSequence = true;
    isUpgrading = false;
    fpsController.enabled = false;*/
  }

  //GUI
  /*********************************************************/
	
  //This section creates the Graphical User Interface (GUI)
	void OnGUI() {
		
		if(!isRunning && !isPaused)
		{
			string message;

			if(isFinished || isPlayerDead || isBossDefeated)
			{
				message = "Click or Press Enter to Play Again";
			}
      else if (isUpgrading || breakingSequence)
			{
				message = "Click or Press Enter to Continue";
			}
			else
			{
				message = "Click or Press Enter to Play";
			}

			//Define a new rectangle for the UI on the screen
			Rect startButton = new Rect(Screen.width/2 - 120, Screen.height/2, 240, 30);

			if (GUI.Button(startButton, message) || Input.GetKeyDown(KeyCode.Return))
			{
        if (isPlayerDead)
          Respawn();
        else if (isUpgrading)
          Resume();
        else if (isBossDefeated){
          SceneManager.LoadScene("Radiation");
        }
        else
          StartGame();
          
			}
		}
		
		// If the player finished the game, show the final time
		

    if (isFinished || isBossDefeated)
    {
      GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "VICTORY!");
			GUI.Label(new Rect(Screen.width / 2.12f - 10, 200, 130, 50), "Time Elapsed: " + ((int)elapsedTime).ToString());
    }

    else if(isPlayerDead)
    {
      GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "YOU DIED");
    }

    else if(isUpgrading)
    {
      GUI.Box(new Rect(Screen.width / 2.12f - 65, 185, 200, 40), player_data.getUpgradeName() + " obtained!");
      if(player_data.getUpgradeCount() == 2)
        GUI.Label(new Rect(Screen.width / 2.25f - 10, 200, 200, 30), "Press LShift + WASD to use.");
    }

   
/*
    else if(isRunning && keyData.SequenceOn)
    {
      GUI.Box(new Rect(Screen.width / 2 - 65, 185, 130, 40), "Sequence Level: " + keyData.SequenceLevel);
			GUI.Label(new Rect(Screen.width / 2.12f - 10, 200, 130, 30), "Time Left: " + ((int)SequenceTime).ToString());
    }
    */
    else if(isRunning)
		{ 
			GUI.Box(new Rect(Screen.width / 2 - 65, Screen.height - 115, 130, 40), "Time Elapsed");
			GUI.Label(new Rect(Screen.width / 2 - 10, Screen.height - 100, 130, 30), ((int)elapsedTime).ToString());
		}
    
    
	}
  
  public bool didPlayerDie()
  {
    return isPlayerDead;
  }
  
  //////////////

  public void activateSpeedBoost()
  {
    fpsController.activateSpeedBoost();
  }

  public void activateJumpBoost()
  {
    fpsController.activateJumpBoost();
  }

  public void revertSpeed()
  {
    fpsController.revertSpeed();
  }

  public void revertJump()
  {
    fpsController.revertJump();
  }

///////////////

  public void updateCheckpoint()
  {
    
    spawnAtCheckpoint = true;
      
    bool[] turretStatus = new bool[turrets.Length];

    for (int i = 0; i < turrets.Length; i++)
    {
      turretStatus[i] = turrets[i].isDead();
    }
    checkpointData.p_Data.SetPlayerData(player_data);
    checkpointData.t_status = turretStatus;
    checkpointData.k_status = keyData.getBoolArray();
  }

  
  public void loadCheckpointData()
  {
    player_data.SetPlayerData(checkpointData.p_Data);
    for(int i = 0; i < turrets.Length; i++)
    {
      turrets[i].updateStatus(checkpointData.t_status[i]);
    }
    keyData.loadBools(checkpointData.k_status);
  }

  // Changes the abilities of the player based on how many upgrades there are.
  public void assignUpgrades()
  {
    if(isKeyNotUpgrade)
      return;

    if (player_data.getUpgradeCount() == 0)
    {
      revertSpeed();
      revertJump();
    }
    else if (player_data.getUpgradeCount() == 1)
    {
      revertSpeed();
      activateJumpBoost();
    }
    else if (player_data.getUpgradeCount() == 2)
    {
      activateSpeedBoost();
      activateJumpBoost();
    }
    player_data.updateUpgradeName();
  }
  ///////////////////////

  public void reviveAllTurrets()
  {
    for (int i = 0; i < turrets.Length; i++)
    {
      turrets[i].spawn();
    }
  }

  public void keyGlitchOverride()
  {
    isKeyNotUpgrade = true;
  }

  public bool checkForKeyGlitch()
  {
    return isKeyNotUpgrade;
  }

  public bool checkPause(){
    return isPaused;
  }


}




