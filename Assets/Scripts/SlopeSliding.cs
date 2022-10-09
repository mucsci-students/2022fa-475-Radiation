using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeSliding : MonoBehaviour
{/*
  [Header("Referenes")]
  public Transform orientation;
  public Transform playerObj;
  private Rigidbody rb;
  private FirstPersonController pm;

  [Header("Sliding")]
  public float maxSlideTime;
  public float slideForce;
  private float slideTimer;

  public float slideYScale;
  private float startYScale;
    
  [Header("Input")]
  public KeyCode slideKey = KeyCode.LeftControl;
  private float horizontalInput;
  private float verticalInput;

  private bool sliding;


  void Start()
  {
    rb = GetComponent<Rigidbody>();
    pm = GetComponent<FirstPersonController>();

    startYScale = playerObj.localScale.y;
  }

  void Update()
  {
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");

    if(Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
      StartSlide();

    if (Input.GetKeyUp(slideKey) && sliding)
      StopSlide();
  }

  void StartSlide()
  {
    sliding = true;
    playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale,
                              playerObj.localScale.z);

    rb.AddForce(Vector3.down)
  }

  void SlidingMovement()
  {

  }

  void StopSlide()
  {
      
  }*/
}
