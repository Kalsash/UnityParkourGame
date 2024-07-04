using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private float walkSpeed = 6f;
    [SerializeField]
    protected float runSpeed = 12f;
    [SerializeField]
    private float jumpPower = 7f;
    [SerializeField]
    private float gravity = 10f;
    [SerializeField]
    private float lookSpeed = 2f;
    public float LookSpeed
    {
        get { return lookSpeed; }
        set { lookSpeed = value; }
    }
    [SerializeField]
    private float lookXLimit = 45f;
    [SerializeField]
    private float defaultHeight = 2f;
    [SerializeField]
    private float crouchHeight = 1f;
    [SerializeField]
    private float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool canRun = false;
    private bool canCrouch = false;
    private bool GoToSpawn = false;


    private KeyCode runButton = KeyCode.LeftShift;
    private KeyCode crouchButton = KeyCode.LeftControl;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //transform.position = new Vector3(0, 0, 118);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (transform.position.z == 1)
        {
            Spawn();
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(runButton) && canRun;
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(crouchButton) && canMove && canCrouch)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);
        // ѕровер€ем, если была нажата клавиша дл€ завершени€ игры (например, клавиша Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
        Winning();

        if (canMove)
        {
            if (GoToSpawn)
            {
                transform.position = new Vector3(0, 0, 1);


            }
            else
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

        }

        if (characterController.isGrounded)
        {
            // ≈сли персонаж стоит на земле, обнул€ем вертикальную скорость
            moveDirection.y = 0;
        }

        else
        {
            // ≈сли персонаж в воздухе, увеличиваем вертикальную скорость вниз
            moveDirection.y -= gravity * Time.deltaTime;

            // ѕровер€ем, не оказалс€ ли персонаж ниже уровн€ земли
            if (transform.position.y < -10)
            {
                if (transform.position.z >= 35)
                {

                    if (transform.position.z >= 70)
                    {
                        transform.position = new Vector3(0, 0, 75);
                    }
                    else
                    {
                        transform.position = new Vector3(0, 0, 40);
                    }
                }

                else
                {
                    transform.position = new Vector3(0, 0, 0);
                }
                moveDirection.y = 0;
            }

        }
    }
    public async Task Spawn()
    {
        await Task.Delay(10);
        GoToSpawn = false;
    }   
    public void Winning()
    {
        if (transform.position.z >= 121)
        {
            Debug.Log("WIN");
            QuitGame();
        }
    }
    public void AddSpeedAbility(Collider player)
    {
        if (player.tag == "Player")
        {
            canRun = true;
        }
    }
    public void AddCrouchAbility(Collider player)
    {
        if (player.tag == "Player")
        {
            canCrouch = true;
        }
    }
    public async void MoveDisability(Collider player)
    {
        if (player.tag == "Player")
        {
            canMove = false;
            await Task.Delay(3000);
            canMove = true;
        }
    }
   public void GotoSpawn()
    {
        GoToSpawn = true;
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}