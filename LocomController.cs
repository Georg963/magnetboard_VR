using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;

    public bool enableLeftTeleport { get; set; } = true;
    public bool enableRightTeleport { get; set; } = true;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        //activate teleporting when Trigger Button is pressed
        //deactivate teleporting when an object is gripped
        if (leftTeleportRay)
        {
            bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport && checkIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
        }

        if (rightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            rightTeleportRay.gameObject.SetActive(enableRightTeleport && checkIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
        }
    }

    public bool checkIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
