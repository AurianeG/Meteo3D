using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MushroomPhysics : MonoBehaviour
{
    private Rigidbody body;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        body.isKinematic = true;

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        body.isKinematic = false;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        body.isKinematic = false;

        body.WakeUp();
    }
}
