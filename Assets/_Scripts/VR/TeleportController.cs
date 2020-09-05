using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportController : MonoBehaviour
{
    public XRController RightTeleportRay;
    public InputHelpers.Button TeleportActivationButton;
    public float ActivationThreshold = 0.1f;
    public bool EnableTeleport { get; set; } = true;
    public XRRayInteractor rightRayInteractorInteract;

    private XRRayInteractor rightRayInteractorTeleport;

    void Start()
    {
        if (RightTeleportRay)
            rightRayInteractorTeleport = RightTeleportRay.gameObject.GetComponent<XRRayInteractor>();
    }

    void Update()
    {
        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        if (RightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightRayInteractorInteract.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            // To trigger / unselect the teleportation area before the ray gets deactivated. For better teleporting
            rightRayInteractorTeleport.allowSelect = CheckIfActivated(RightTeleportRay);
            RightTeleportRay.gameObject.SetActive(CheckIfActivated(RightTeleportRay) && !isRightInteractorRayHovering);
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, TeleportActivationButton, out bool isActivated, ActivationThreshold);
        return isActivated && EnableTeleport;
    }
}
