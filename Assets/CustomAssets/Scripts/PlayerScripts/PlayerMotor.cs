using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [System.Serializable]
    public enum PlayerStartSpeed
    {
        HIVE,
        OUTSIDE
    }
    [SerializeField]
    private Transform cam;
    [SerializeField]
    PlayerStartSpeed type;
    [SerializeField]
    private PlayerControllerStats ControllerStats;
    [SerializeField]
    private PlayerControllerStats ControllerStatsInHive;
    [SerializeField]
    private PlayerEfectsStats EffectStats;
    [Tooltip("Do NOT modify. Exposed parameter for testing purposes ONLY")]
    [SerializeField]
    private float fSpeed;
   // [SerializeField]
    //Animator animator;
    private float uSpeed;
    private float turnSmoothVelocity = 0.1f;

    bool slowed = false;
    private float sTimer;
    private CharacterController controller;
    private bool isGrounded = false;
    
    private PlayerControllerStats currentControllerStats;
    // Start is called before the first frame update
    void Awake()
    {
        switch (type)
        {
            case PlayerStartSpeed.HIVE:
                currentControllerStats = ControllerStatsInHive;
                break;
            case PlayerStartSpeed.OUTSIDE:
                currentControllerStats = ControllerStats;
                break;
        }
        
        controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            fSpeed = currentControllerStats.ForwardSpeedWalk;
        }
        else
        {
            fSpeed = currentControllerStats.ForwardSpeedFly;
        }
        uSpeed = currentControllerStats.UpSpeed;
        sTimer = EffectStats.slowedTimer;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EventQueue.eventQueue.Subscribe(EventType.PLAYERPESTICIDECOLLISION, OnPlayerColidesWithPesticides);
        EventQueue.eventQueue.Subscribe(EventType.CHANGEDIFFICULTY, OnChangeCooldoenTime);

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
            currentControllerStats.spamSpaceKey = !currentControllerStats.spamSpaceKey;
        }

        if (currentControllerStats.spamSpaceKey == false)
        {
            if (Input.GetButton("FlyUp"))
            {
                controller.Move(Vector3.up * currentControllerStats.UpSpeed * Time.deltaTime);
                isGrounded = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("FlyUp"))
            {
                controller.Move(Vector3.up * currentControllerStats.UpSpeedSpam * Time.deltaTime);
                isGrounded = false;

            }
        }

        if (Input.GetButton("FlyDown"))
        {
            controller.Move(Vector3.up * -currentControllerStats.DownSpeed * Time.deltaTime);
        }
        // Debug.Log(controller.isGrounded+" Before check");


        if (controller.isGrounded)
        {
            isGrounded = true;
            fSpeed = currentControllerStats.ForwardSpeedWalk;
        }
        else
        {
            controller.Move(Vector3.up * currentControllerStats.Gravity * -1 * Time.deltaTime);
            if (fSpeed < currentControllerStats.ForwardSpeedFly)
                fSpeed += currentControllerStats.Acceleration * Time.deltaTime;
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
                    fSpeed = currentControllerStats.ForwardSpeedWalk;
                }
                else
                {
                    if (fSpeed < currentControllerStats.ForwardSpeedFly)
                        fSpeed += currentControllerStats.Acceleration * Time.deltaTime;
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
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, currentControllerStats.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * fSpeed * Time.deltaTime);
            if (controller.isGrounded)
            {
                GetComponent<PlayerAnimationState>().ChangeAnimatorState(PlayerAnimationStates.WALK);
               // animator.SetInteger("condition", 3);
            }
            else
            {
                GetComponent<PlayerAnimationState>().ChangeAnimatorState(PlayerAnimationStates.FLYFORWARD);
                //animator.SetInteger("condition", 1);
                //isGrounded = false;
            }
            //animator.SetInteger("condition", 1);
            //Debug.Log(animator.GetInteger("condition"));
        }
        else
        {
            if (controller.isGrounded)
            {
                GetComponent<PlayerAnimationState>().ChangeAnimatorState(PlayerAnimationStates.STANDIDLE);
                //animator.SetInteger("condition", 2);
            }
            else
            {
                GetComponent<PlayerAnimationState>().ChangeAnimatorState(PlayerAnimationStates.FLYIDLE);
                //animator.SetInteger("condition", 0);
                //isGrounded = false;
            }
            //animator.SetInteger("condition", 0);
            //Debug.Log(animator.GetInteger("condition"));
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hive")
        {
            Debug.Log("Player in hive change controlles");
            currentControllerStats = ControllerStatsInHive;
            fSpeed = currentControllerStats.ForwardSpeedWalk;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hive")
        {
            Debug.Log("Player outside of hive change controlles");
            currentControllerStats = ControllerStats;
        }
    }

    public PlayerControllerStats GetPlayerControllerStates()
    {
        return ControllerStats;
    }


    public PlayerEfectsStats GetPlayerEffectStates()
    {
        return EffectStats;
    }

}

