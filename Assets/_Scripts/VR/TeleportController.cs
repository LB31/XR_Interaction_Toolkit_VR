using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportController : MonoBehaviour
{
    public XRController RightTeleportRay;
    public InputHelpers.Button TeleportActivationButton;
    public float ActivationThreshold = 0.1f;
    private XRRayInteractor rightRayInteractor;

    void Start()
    {
        if (RightTeleportRay)
            rightRayInteractor = RightTeleportRay.gameObject.GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        if (RightTeleportRay)
        {
            rightRayInteractor.allowSelect = CheckIfActivated(RightTeleportRay);
            RightTeleportRay.gameObject.SetActive(CheckIfActivated(RightTeleportRay));
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, TeleportActivationButton, out bool isActivated, ActivationThreshold);
        return isActivated;
    }
}
