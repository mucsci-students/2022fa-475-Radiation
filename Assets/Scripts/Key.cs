

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
  public Transform Location;
  public Transform LightLocation;
  public Transform Vault;

  public GameObject OnKey;
  public GameObject OffKey;
  
  public GameObject OnLight;
  public GameObject OffLight;

  public KeyManager keyData;

  

  private bool isOn = false;
  public bool secretKey;

  public void turnOff()
  {
    isOn = false;
    OnKey.transform.position = Vault.position;
    OffKey.transform.position = Location.position;
    OnLight.transform.position  = Vault.position;
    OffLight.transform.position = LightLocation.position;

    if(!secretKey)
      keyData.decrement();
  }

  public void turnOn()
  {
    isOn = true;
    OffKey.transform.position = Vault.position;
    OnKey.transform.position = Location.position;
    OffLight.transform.position  = Vault.position;
    OnLight.transform.position = LightLocation.position;
    
    if(!secretKey)
      keyData.increment();
  } 

  void OnTriggerEnter(Collider other)
  {
    if(/*other.gameObject.name == "Player" ||*/ other.gameObject.name != "Deadly Laser")
      turnOn();
  }
  
  public bool isKeyOn()
  {
    return isOn;
  }
}
