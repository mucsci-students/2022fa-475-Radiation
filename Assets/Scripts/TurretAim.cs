
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
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
     Vector3 playerGroundPos = new Vector3(PlayerLocation.position.x, 
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



/*


using UnityEngine;

public class TurretAuto : MonoBehaviour
{
    public fireProjectile weapon;
    public Transform target;
    public Transform barrel;

    private void Update() 
    {
        Aim();
        weapon.fire();
    }

    private void Aim()
    {
        // TURN
        float targetPlaneAngle = vector3AngleOnPlane(target.position, transform.position, -transform.up, transform.forward);
        Vector3 newRotation = new Vector3(0, targetPlaneAngle, 0);
        transform.Rotate(newRotation, Space.Self);
        
        // UP/DOWN
        float upAngle = Vector3.Angle(target.position, barrel.transform.up);
        Vector3 upRotation = new Vector3(-upAngle + 90, 0, 0);
        barrel.transform.Rotate(upRotation, Space.Self);
    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

        return projectedVectorAngle;
    } 
}
*/