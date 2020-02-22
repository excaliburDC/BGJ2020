using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // public vars
   
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 6;
    public float jumpForce = 220;
    public LayerMask groundedMask;
    public Animator cameraAnim;

    public GameObject[] layers;
   



    // System vars
    [SerializeField] bool grounded;
    [SerializeField] bool madeCrack;
    [SerializeField] bool madeHole;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    int jumpCount = 0;
    int currentLayer = 0;
    Transform cameraTransform;
    Rigidbody rigidbody;


    void Awake()
    {
  
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // Look rotation:
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
        //verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        //verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        //cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

        CalculateMovement();


        // Jump
        if (isGrounded())
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpCount++;
                rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                if(jumpCount==1)
                {
                    madeCrack = true;
                    Invoke("MoveAgain", 1.5f);
                }
                else if(jumpCount==2)
                {
                    madeHole = true;
                    Invoke("MoveAgain", 1.5f);
                    jumpCount = 0;
                }
              
              


            }

        }


            
        






    }

    void FixedUpdate()
    {
        // Apply movement to rigidbody
       
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }

    void CalculateMovement()
    {
        
        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        
    }

    bool isGrounded()
    {
        // Grounded check
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
        {
            Debug.DrawRay(transform.position, -transform.up);
            Debug.Log("Grounded");
            return true;
        }
        else
        {
           
            return false;
        }
    }

    void MoveAgain()
    {
        if(madeCrack)
            madeCrack = false;

        if (madeHole)
            madeHole = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Hole" && madeCrack)
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            cameraAnim.SetTrigger("CameraShake");

        }

       if(other.gameObject.tag=="Hole" && madeHole)
       {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.GetComponent<Renderer>().material.color = Color.black;
            cameraAnim.SetTrigger("CameraShake");
            layers[currentLayer].gameObject.SetActive(false);
            layers[++currentLayer].gameObject.SetActive(true);

        }
    }
}
