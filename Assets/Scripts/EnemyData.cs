using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{

  [SerializeField] public int EnemyHealth;
  [SerializeField] public int MaxEnemyHealth;
  public GameObject self;
  public Transform spawnPoint;
  public Transform vault;
  public CustomBullet playerBullet;
  public TurretAim behavior;
  public TurretHealthBar TurretHealthBar;
  private bool isDead = false;

  //public GameUIs gameUIs;

  public int getHealth()
  {
    return EnemyHealth;
  }

  public void takeDamage(int damage)
  {
    EnemyHealth -= damage;
    updateEnemyBar();
    
    if(EnemyHealth <= 0)
    {
      die();
    }
      
  }

  void OnTriggerEnter(Collider other)
  {
    //If the object is a player bullet.
    if(isPlayerInRange())
      takeDamage(playerBullet.damage);
  }

  public bool isEnemyDead()
  {
    return isDead;
  }
  
  public void spawn()
  {
    isDead = false;
    self.transform.position = spawnPoint.position;
    restoreHealth();
  }

  public void die()
  {
    TurretHealthBar.killTurret();
    isDead = true;
    self.transform.position = vault.position;
  }

  public void updateStatus(bool newStatus)
  {
    isDead = newStatus;
    if(isDead)
      die();
    else
      spawn();
  }

  public void restoreHealth()
  {
    EnemyHealth = MaxEnemyHealth;
  }

  public bool isPlayerInRange()
  {
    return behavior.isPlayerInRange();
  }

  public CustomBullet getPlayerBullet()
  {
    return playerBullet;
  }

  public void updateEnemyBar(){
    TurretHealthBar.updateBar(EnemyHealth, MaxEnemyHealth);
  }
}