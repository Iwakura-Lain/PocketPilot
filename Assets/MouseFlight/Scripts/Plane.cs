//
// Copyright (c) Brian Hernandez. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
//

using System;
using UnityEngine;
using MFlight;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Plane : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private MouseFlightController controller = null;

    [Header("Speeds")] private float thrust;
    public float thrustNormal = 4000;
    public float thrustSpeedUp = 8000;
    public float thrustSlowDown = 2000;

    [Tooltip("Pitch, Yaw, Roll")] public Vector3 turnTorque = new Vector3(90f, 25f, 45f);
    [Tooltip("Multiplier for all forces")] private float forceMult = 10f;

    public float sensitivity = 0.25f;
    [Tooltip("Angle at which airplane banks fully into target.")]
    public float aggressiveTurnAngle = 10f;

    [Header("Input")] [SerializeField] [Range(-1f, 1f)]
    private float pitch = 0f;

    [SerializeField] [Range(-1f, 1f)] private float yaw = 0f;
    [SerializeField] [Range(-1f, 1f)] private float roll = 0f;

    public float Pitch
    {
        set => pitch = Mathf.Clamp(value, -1f, 1f);
        get => pitch;
    }

    public float Yaw
    {
        set => yaw = Mathf.Clamp(value, -1f, 1f);
        get => yaw;
    }

    public float Roll
    {
        set => roll = Mathf.Clamp(value, -1f, 1f);
        get => roll;
    }

    private bool landing;
    public  bool OnLanding;

    private Rigidbody rigid;

    private bool rollOverride = false;
    private bool pitchOverride = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Setup();
        Messenger.AddListener("StartLanding", StartLanding);
        Messenger.AddListener("StopLanding", stopLanding);
        Messenger.AddListener("CargoTaken", Setup);
        Messenger.AddListener("FirstPieceIsDelivered", Setup);

        if (controller == null)
            Debug.LogError(name + ": Plane - Missing reference to MouseFlightController!");
    }

    public void Setup() //can be used later to change plane parameters according to level number
    {
        landing = OnLanding = false;
        rigid.mass = 200;
        rigid.drag = 15;
        rigid.angularDrag = 10;
        rigid.useGravity = false;
        GetComponent<SphereCollider>().enabled = true;
        rigid.isKinematic = false;
        thrust = thrustNormal;
    }

    private void Update()
    {
        rollOverride = false;
        pitchOverride = false;

        var keyboardRoll = Input.GetAxis("Horizontal")  * 0.1f;
        if (Mathf.Abs(keyboardRoll) > 0) rollOverride = true;


        if (Input.GetKey(KeyCode.W))
        {
            thrust = thrustSpeedUp; 
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.F))
        {
            thrust = thrustSlowDown;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            thrust = thrustNormal;
        }
        // Calculate the autopilot stick inputs.
        var autoYaw = 0f;
        var autoPitch = 0f;
        var autoRoll = 0f;
        if (controller != null)
            RunAutopilot(controller.MouseAimPos, out autoYaw, out autoPitch, out autoRoll);

        // Use either keyboard or autopilot input.
        yaw = autoYaw;
        pitch = autoPitch;
       // pitch = pitchOverride ? keyboardPitch : autoPitch;
        roll = rollOverride ? keyboardRoll : autoRoll;
    }

    private void RunAutopilot(Vector3 flyTarget, out float yaw, out float pitch, out float roll)
    {
        // This is my usual trick of converting the fly to position to local space.
        // You can derive a lot of information from where the target is relative to self.
        var localFlyTarget = transform.InverseTransformPoint(flyTarget).normalized * sensitivity;
        var angleOffTarget = Vector3.Angle(transform.forward, flyTarget - transform.position);

        // IMPORTANT!
        // These inputs are created proportionally. This means it can be prone to
        // overshooting. The physics in this example are tweaked so that it's not a big
        // issue, but in something with different or more realistic physics this might
        // not be the case. Use of a PID controller for each axis is highly recommended.

        // ====================
        // PITCH AND YAW
        // ====================

        // Yaw/Pitch into the target so as to put it directly in front of the aircraft.
        // A target is directly in front the aircraft if the relative X and Y are both
        // zero. Note this does not handle for the case where the target is directly behind.
        yaw = Mathf.Clamp(localFlyTarget.x, -1f, 1f);
        pitch = -Mathf.Clamp(localFlyTarget.y, -1f, 1f);

        // ====================
        // ROLL
        // ====================

        // Roll is a little special because there are two different roll commands depending
        // on the situation. When the target is off axis, then the plane should roll into it.
        // When the target is directly in front, the plane should fly wings level.

        // An "aggressive roll" is input such that the aircraft rolls into the target so
        // that pitching up (handled above) will put the nose onto the target. This is
        // done by rolling such that the X component of the target's position is zeroed.
        var agressiveRoll = Mathf.Clamp(localFlyTarget.x, -1f, 1f);

        // A "wings level roll" is a roll commands the aircraft to fly wings level.
        // This can be done by zeroing out the Y component of the aircraft's right.
        var wingsLevelRoll = transform.right.y;

        // Blend between auto level and banking into the target.
        var wingsLevelInfluence = Mathf.InverseLerp(0f, aggressiveTurnAngle, angleOffTarget);
        roll = Mathf.Lerp(wingsLevelRoll, agressiveRoll, wingsLevelInfluence);
    }

    private void FixedUpdate()
    {
        // Ultra simple flight where the plane just gets pushed forward and manipulated
        // with torques to turn.
        rigid.AddRelativeForce(Vector3.forward * thrust * forceMult, ForceMode.Force);
        rigid.AddRelativeTorque(new Vector3(turnTorque.x * pitch,
                turnTorque.y * yaw,
                -turnTorque.z * roll) * forceMult,
            ForceMode.Force);
    }

    public void StartLanding()
    {
        rigid.mass = rigid.mass + 15;
        rigid.useGravity = true;

        GetComponent<SphereCollider>().enabled = false;
    }

    public void stopLanding()
    {
        print("stop");
        rigid.isKinematic = true;
        thrust = 0;
    }
    
    public delegate void DestroyedAction();

    public GameObject ExplosionPrefab;
    public GameObject AfterlifeUI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Enemy")
            BlowMe();
    }

    public static event DestroyedAction OnDestroyed;

    private void BlowMe()
    {
        if (SceneManager.GetActiveScene().name == "TimeChallenge")
        {
            Messenger.Broadcast("Death");
        }
        Messenger.RemoveListener("StartLanding", StartLanding);
        Messenger.RemoveListener("StopLanding", stopLanding);
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        AfterlifeUI.SetActive(true);
        OnDestroyed?.Invoke();
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }
}