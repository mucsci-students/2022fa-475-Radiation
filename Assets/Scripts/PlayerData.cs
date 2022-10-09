using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

  public GameManager gameManager;
  public GameUIs gameUIs;

  private int health;
  private int ammoCount;
  private int upgradeCount;
  private int magnetCount;
  private int MAX_HEALTH;
  private string upgradeName;
  private int keyCount;

  //Initialization functions
  /**********************************************/

  // Start is called before the first frame update
  void Start()
  {
    MAX_HEALTH = 500;
  }

  // Update is called once per frame
  /*
  void Update()
  {
    if (health <= 0)
      isDead = true;
  }*/

  public void SetPlayerData(int newHealth, int newAmmo, int newUpgrade, int newMagCount)
  {
    setHealth(newHealth);
    setAmmo(newAmmo);
    setUpgrades(newUpgrade);
    setMagCount(newMagCount);
  }

  public void SetPlayerData(PlayerData data)
  {
    SetPlayerData(data.getHealth(), data.getAmmo(), data.getUpgradeCount(), data.getMagnetCount());
  }
  // Data retrieval functions
  /**********************************************/

  public int getHealth()
  {
    return health;
  }

  public int getAmmo()
  {
    return ammoCount;
  }

  public int getUpgradeCount()
  {
    return upgradeCount;
  }

  public int getMagnetCount()
  {
    return magnetCount;
  }

  public string getUpgradeName()
  {
    return upgradeName;
  }

  public int getKeyCount()
  {
    return keyCount;
  }
  // Data manipulation functions
  /**********************************************/

  public void setHealth(int newHealth)
  {
    health = newHealth;
    //adjustHealth(0);
  }

  public void setAmmo(int newAmmo)
  {
    ammoCount = newAmmo;
  }

  public void setUpgrades(int newUpgrade)
  {
    upgradeCount = newUpgrade;
  }

  public void setMagCount(int newMagCount)
  {
    magnetCount = newMagCount;
  }

  public void setKeyCount(int newKeys)
  {
    keyCount = newKeys;
  }
  
  // Misc functions
  /**********************************************/

  // Runs whenever player is damaged or healed
  public void adjustHealth(int deltaHealth)
  {
    health += deltaHealth;

    if (health > MAX_HEALTH)
      health = MAX_HEALTH;
    
    else if (health <= 0)
    {
      health = 0;

      
      if (!gameManager.didPlayerDie())
        gameManager.KillPlayer();
    }
    
    gameUIs.updatePlayerHealthBar(health, MAX_HEALTH);
  }

  //Converts Radioactive Magnets into Stun Pistol Ammo
  public bool convertToAmmo()
  {
    if(magnetCount != 0 && ammoCount < 50)
    {
      magnetCount--;
      ammoCount += 5;

      if (ammoCount > 50)
        ammoCount = 50;

      return true;
    }
    else
      return false;
  }

  public void updateUpgradeName()
  {
    if (upgradeCount == 0)
    {
      upgradeName = "";
    }
    else if (upgradeCount == 1)
    {
      upgradeName = "Jump Boost";
    }
    else if (upgradeCount == 2)
    {
      upgradeName = "Speed Boost";
    }
  }

  public void magnetObtained()
  {
    magnetCount++;
  }

  public void magnetConsumed()
  {
    magnetCount--;
  }

  public void shootAmmo()
  {
    ammoCount--;
  }

  public void updateUI()
  {
    gameUIs.updatePlayerHealthBar(health, MAX_HEALTH);
  }

  
}
