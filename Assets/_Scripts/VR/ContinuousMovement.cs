using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float Speed = 1;
    public XRNode InputSource;
    public float AdditionalHeight = 0.2f;

    private XRRig rig; 
    private Vector2 inputAxis;
    private CharacterController character;
    private InputDevice device;
    private float gravity = -9.81f;
    private float fallingSpeed;

    void Start()
    {
        rig = GetComponent<XRRig>();
        character = GetComponent<CharacterController>();
        device = InputDevices.GetDeviceAtXRNode(InputSource);
    }

    void Update()
    {
        
        device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        if (inputAxis.magnitude > 0.15f)
            character.Move(direction * Time.fixedDeltaTime * Speed);

        // gravity 
        if (character.isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    private void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + AdditionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height * 0.5f + character.skinWidth, capsuleCenter.z);
    }

}
