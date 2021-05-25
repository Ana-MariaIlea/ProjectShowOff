using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private PlayerControllerStats ControllerStats;
    [SerializeField]
    private PlayerEfectsStats EffectStats;
    [Tooltip("Do NOT modify. Exposed parameter for testing purposes ONLY")]
    [SerializeField]
    private float fSpeed;
    private float uSpeed;
    private float turnSmoothVelocity = 0.1f;

    bool slowed = false;
    private float sTimer;
    private CharacterController controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        fSpeed = ControllerStats.ForwardSpeed;
        uSpeed = ControllerStats.UpSpeed;
        sTimer = EffectStats.slowedTimer;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EventQueue.eventQueue.Subscribe(EventType.PLAYERPESTICIDECOLLISION, OnPlayerColidesWithPesticides);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (slowed)
        {
            if (sTimer < 0)
            {
                slowed = false;
                sTimer = EffectStats.slowedTimer;
                fSpeed = ControllerStats.ForwardSpeed;
            }
            else
            {
                sTimer -= Time.deltaTime;
            }
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ControllerStats.spamSpaceKey = !ControllerStats.spamSpaceKey;
        }

        if (ControllerStats.spamSpaceKey == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //directionY += UpSpeed;
                controller.Move(Vector3.up * ControllerStats.UpSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //directionY += UpSpeed;
                controller.Move(Vector3.up * ControllerStats.UpSpeedSpam * Time.deltaTime);

            }
        }

        if (!controller.isGrounded)
        {
            //directionY -= DownSpeed;
            controller.Move(Vector3.up * ControllerStats.DownSpeed * -1 * Time.deltaTime);
        }


        //direction.y = directionY;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, ControllerStats.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * fSpeed * Time.deltaTime);
        }
    }


    public void OnPlayerColidesWithPesticides(EventData eventData)
    {
        if (eventData is PlayerPesticideCollisionEventData)
        {
            fSpeed = EffectStats.ForwardSpeedSlowed;
            slowed = true;
        }
    }
}
