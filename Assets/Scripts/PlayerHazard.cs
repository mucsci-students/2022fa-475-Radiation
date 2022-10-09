using UnityEngine;
using System.Collections;

public class PlayerHazard : MonoBehaviour
{
	//A reference to the game manager
	public PlayerData p_Data;

	// Triggers when the player enters the lava
	void OnTriggerEnter(Collider other)
	{
    p_Data.adjustHealth(-9999);
	}
}
