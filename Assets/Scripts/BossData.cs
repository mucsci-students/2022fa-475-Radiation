using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossData : MonoBehaviour
{

  [SerializeField] public int BossHealth;
  [SerializeField] public int MaxBossHealth;
  public CustomBullet playerBullet;
  public GameManager gameManager;
  public TurretAim bossBehavior;

  public GameUIs gameUIs;
  

  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {
    
  }

  public int getHealth()
  {
    return BossHealth;
  }
  
  public void resetBossHealth()
  {
    BossHealth = MaxBossHealth;
    gameUIs.updateBossHealthBar(BossHealth, MaxBossHealth);
  }

  public void takeDamage(int damage)
  {
    BossHealth -= damage;
    gameUIs.updateBossHealthBar(BossHealth, MaxBossHealth);
    if (BossHealth <= 0)
      gameManager.BossDefeated();
  }

  void OnTriggerEnter(Collider other)
  {
    // If the player isn't in range, take no damage. (Fixes an issue where the player restarts 
    // the game, but the projectiles from the previous game still remain, and damage the boss.)
    if(bossBehavior.isPlayerInRange())
      takeDamage(playerBullet.damage);
  }
  
  
}