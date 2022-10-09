using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_TurretGun : MonoBehaviour
{
    
  [SerializeField] float turretRange = 50f;
  [SerializeField] float turretRotationSpeed = 7f;
  
  public GameObject player;

  private Transform PlayerLocation;

  private BossGun currentGun;
  private float fireRate;
  private float fireRateDelta;
  private bool inRange = false;
  // Start is called before the first frame update
  void Start()
  {
    PlayerLocation = player.transform;
                        //could replace this with a public function that sets target
                        //On Trigger Enter if there is multiple targets
    currentGun = GetComponentInChildren<BossGun>();
    fireRate = currentGun.GetRateOfFire();
  }

  // Update is called once per frame
  void Update()
  {
     Vector3 playerGroundPos = new Vector3(transform.position.x, 
                                  PlayerLocation.position.y, PlayerLocation.position.z);

    //Check if player is not in range, then do nothing
    if(Vector3.Distance(transform.position, playerGroundPos) > turretRange)
    {
      inRange = false;
      return; //do nothing because player is not in range
    }

    inRange = true;
    
    //PLAYER IN RANGE

    //Rotate Turret towards player
    Vector3 playerDirection = playerGroundPos - transform.position;

    float turretRotationStep = turretRotationSpeed * Time.deltaTime;
    Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, playerDirection,
                                   turretRotationStep, 0f);

    transform.rotation = Quaternion.LookRotation(newLookDirection);

    fireRateDelta -= Time.deltaTime;
    if(fireRateDelta <= 0)
    {
      currentGun.Fire();
      fireRateDelta = fireRate;
    }
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(transform.position, turretRange); //Show a gizmo when selected
  }

  public bool isPlayerInRange()
  {
    return inRange;
  }

  
}