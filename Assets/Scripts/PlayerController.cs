using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform camera;
    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    private GameObject playerGO;
    private Animator animator;

    private void Start()
    {
        animator = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
        //playerGO = GameObject.FindGameObjectWithTag("Character");
    }

    void Update()
    {

        float horizantalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(0f, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
            //playerGO.GetComponent<Animator>().enabled = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            //playerGO.GetComponent<Animator>().enabled = false;
        }

    }
}
