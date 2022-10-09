
/*
Author: Matt Giacoponello
Filename: Upgrades.cs
Description: Determines which potential upgrade locations have an actual upgrade.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Upgrades : MonoBehaviour
{
  
  public PlayerData player;
  public GameManager gameManager;
  public GameObject upgrade;
  public Transform upgradeLocation;
  public GameObject checkpointObject;
  public Transform vault;
  public KeyManager keyData;
  
  
  void OnTriggerEnter(Collider other)
  {
    int activeKeys = player.getKeyCount();
    gameManager.checkForKeyGlitch();
    if(!gameManager.checkForKeyGlitch() || !checkForDoubleUpgradeGlitch())
    {
      player.setUpgrades(player.getUpgradeCount() + 1);
      
      if(activeKeys != player.getKeyCount())
      {
        player.setKeyCount(activeKeys);
        player.setUpgrades(player.getUpgradeCount() - 1);
        return;
      }

      checkpointObject.transform.position = upgradeLocation.position;
      upgrade.transform.position = vault.position;
      gameManager.assignUpgrades();
      gameManager.updateCheckpoint();
      player.adjustHealth(9999);
      gameManager.UpgradeObtained();
    }
    
  }

  bool checkForDoubleUpgradeGlitch()
  {
    return upgrade.transform.position == vault.position;
  }

  public void respawnUpgrade()
  {
    upgrade.transform.position = upgradeLocation.position;
  }
}




