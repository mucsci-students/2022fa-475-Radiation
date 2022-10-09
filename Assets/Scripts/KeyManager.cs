

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
  public GameManager gameManager;
  private bool isBossUnlocked = false;
  private bool UnlockNotification = false;

  private bool SequenceOn = false;
  private bool SequenceSolved = false;
  
  private int SequenceLevel = 0;
  

  public PlayerData player;

  [SerializeField] public float SequenceTimeLimit;

  public Key[] Keys;
  //public Key[] SecretKeys;
  
  public GameObject LightWall;
  public GameObject SecretLightWall;

  public Transform WallLocation;
  public Transform SecretWallLocation;
  
  public Transform Vault;
  
  /*
  void Start()
  {
    SequenceLevel = 0;
    keyCount = 0;
    turnOffAllKeys();
  }
*/
  public void turnOffAllKeys()
  {
    SequenceOn = false;
    SequenceSolved = false;
    isBossUnlocked = false;
    for(int i = 0; i < Keys.Length; i++)
    {  
      Keys[i].turnOff();
    }
    //turnOffSecretKeys();
    LockBossTeleporter();
    //LockSecretTeleporter();
  }

  public bool[] getBoolArray()
  {
    bool[] boolArray = new bool[Keys.Length];
    for(int i = 0; i < Keys.Length; i++)
      boolArray[i] = Keys[i].isKeyOn();
    
    return boolArray;
  }

  public void loadBools(bool[] boolArray)
  {
    if(boolArray.Length != Keys.Length)
      return;

    for(int i = 0; i < boolArray.Length; i++)
    {
      if(boolArray[i])
        Keys[i].turnOn();
      else
        Keys[i].turnOff();
    }
  }
/*
  public void turnOffSecretKeys()
  {
    SequenceOn = false;
    SequenceSolved = false;
    SequenceLevel = 0;
    for(int i = 0; i < SecretKeys.Length; i++)
    {  
      SecretKeys[i].turnOff();
    }
  }
*/

  bool bossUnlockCheck()
  {
    for(int i = 0; i < Keys.Length; i++)
    {
      if(!Keys[i].isKeyOn())
      {
        return false;
      }
    }

    unlockBoss();
    return true;
  }

  
  void unlockBoss()
  {
    LightWall.transform.position = Vault.position;
  }
/*
  void unlockSecretArea()
  {
    SecretLightWall.transform.position = Vault.transform.position;
  }
*/
  void LockBossTeleporter()
  {
    LightWall.transform.position = WallLocation.position;
  }
/*
  void LockSecretTeleporter()
  {
    SecretLightWall.transform.position = SecretWallLocation.position;
  }
*/
  public void decrement()
  {
    player.setKeyCount(player.getKeyCount() - 1);
    if(player.getKeyCount() < 0)
      player.setKeyCount(0);
  }

  public void increment()
  {
    int upgrades = player.getUpgradeCount();
    player.setKeyCount(player.getKeyCount() + 1);
    if(upgrades != player.getUpgradeCount());
    {
      player.setUpgrades(upgrades);
    }
    gameManager.keyGlitchOverride();
    if(Keys[0].isKeyOn() && Keys[1].isKeyOn() && Keys[2].isKeyOn())
      unlockBoss();
  }



  
}







