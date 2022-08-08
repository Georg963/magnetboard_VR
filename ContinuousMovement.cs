using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSource;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    //Approximation of the gravity around the earth
    public float gravity = -9.81f; 

    private float fallingSpeed;
    private XRRig rig; 
    private Vector2 inputAxis;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the device
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);

        //listen to the input
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    /*for the actual movement of the character
     * the movement will be computed each time that unity update 
     * the physics of our game*/
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        //access the head gameObject
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        //to rotate our direction by multiplying the direction Vector3 by the headYaw rotation
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        //move our character
        character.Move(direction * Time.fixedDeltaTime * speed);

        //gravity, only when we are falling, and not on the ground
        bool isGrounded = checkIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    //to make the character follow us, when we physically move around in VR without the touch pad
    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool checkIfGrounded()
    {
        //tells us if on Ground
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit; 
    }
}