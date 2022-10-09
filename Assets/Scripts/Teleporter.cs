using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
  public bool justUsed = false;

	public GameManager gameManager;
  public Transform destination;
  public Teleporter dest;

  void OnTriggerEnter(Collider other)
	{
    if (!justUsed)
    { 
      dest.setBool(true);
      gameManager.TeleportPlayer(destination);
    }
  }

  void OnTriggerExit(Collider other)
  {
    justUsed = false;
  }

  public void setBool(bool newBool)
  {
    justUsed = newBool;
  }
}
