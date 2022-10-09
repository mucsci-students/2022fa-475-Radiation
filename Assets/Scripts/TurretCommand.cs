using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCommand : MonoBehaviour
{
  public EnemyData Data;
  public TurretAim Behavior;

  public bool isDead()
  {
    return Data.isEnemyDead();
  }

  public void spawn()
  {
    Data.spawn();
  }

  public void die()
  {
    Data.spawn();
  }

  public void updateStatus(bool newStatus)
  {
    Data.updateStatus(newStatus);
  }

  void OnTriggerEnter(Collider other)
  {
    if(Data.isPlayerInRange())
      Data.takeDamage(Data.getPlayerBullet().damage);
  }
}
