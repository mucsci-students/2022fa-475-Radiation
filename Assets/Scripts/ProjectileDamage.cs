using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
  [SerializeField] private int damage;

  public PlayerData player;

  void OnTriggerEnter(Collider other)
	{
    player.adjustHealth(-damage);
	}

}
