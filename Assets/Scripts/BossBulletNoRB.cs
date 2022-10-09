using UnityEngine;

public class BossBulletNoRB : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] int damage;
    public GameObject thisBullet;

    public PlayerData player;
   
    private void Update()   //you can change this to a virtual function for multiple projectile types
    {
        transform.Translate(new Vector3(0f, 0f, projectileSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {

      if(collision.gameObject.tag == "Terrain")
        {
          Destroy(thisBullet);
        }
        //Don't count collisions with other bullets
        if (collision.collider.CompareTag("Bullet")) return;

        //Count up collisions
        //collisions++;

        //Explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Player")) 
          player.adjustHealth(-damage);


        
 
        
        
 
    }

    void OnTriggerEnter(Collider other)
	  {
      if(other.gameObject.tag == "Terrain")
        Destroy(thisBullet);
      if(other.gameObject.name == "Player")
        player.adjustHealth(-damage);
	  }



}