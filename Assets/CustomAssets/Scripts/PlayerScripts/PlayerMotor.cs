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
    private bool isGrounded = false;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            fSpeed = ControllerStats.ForwardSpeedWalk;
        }
        else
        {
            fSpeed = ControllerStats.ForwardSpeedFly;
        }
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
        if (controller.enabled)
            Move();
        
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.O))
        {
            ControllerStats.spamSpaceKey = !ControllerStats.spamSpaceKey;
        }

        if (ControllerStats.spamSpaceKey == false)
        {
            if (Input.GetButton("FlyUp"))
            {
                controller.Move(Vector3.up * ControllerStats.UpSpeed * Time.deltaTime);
                isGrounded = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("FlyUp"))
            {
                controller.Move(Vector3.up * ControllerStats.UpSpeedSpam * Time.deltaTime);
                isGrounded = false;

            }
        }

        if (Input.GetButton("FlyDown"))
        {
            controller.Move(Vector3.up * -ControllerStats.DownSpeed * Time.deltaTime);
        }
       // Debug.Log(controller.isGrounded+" Before check");

        
        if (controller.isGrounded)
        {
            isGrounded = true;
            fSpeed = ControllerStats.ForwardSpeedWalk;
        }
        else
        {
            controller.Move(Vector3.up * ControllerStats.Gravity * -1 * Time.deltaTime);
            if (fSpeed < ControllerStats.ForwardSpeedFly)
                fSpeed += ControllerStats.Acceleration * Time.deltaTime;
            //isGrounded = false;
        }

        if (slowed)
        {
            if (sTimer < 0)
            {
                slowed = false;
                sTimer = EffectStats.slowedTimer;
                if (controller.isGrounded)
                {
                    fSpeed = ControllerStats.ForwardSpeedWalk;
                }
                else
                {
                    if (fSpeed < ControllerStats.ForwardSpeedFly)
                        fSpeed += ControllerStats.Acceleration * Time.deltaTime;
                }
            }
            else
            {
                sTimer -= Time.deltaTime;
            }
        }

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

    public void OnChangeCooldoenTime(EventData eventData)
    {
        if (eventData is ChangeDifficultyEventData)
        {
            ChangeDifficultyEventData e = eventData as ChangeDifficultyEventData;
            ControllerStats = e.Difficulty.PlayerControllerStats;
            EffectStats = e.Difficulty.PlayerEfectsStats;
        }
    }
}
