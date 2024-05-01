using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationRayControl : MonoBehaviour
{
    public XRInteractorLineVisual lineVisual;
    public LayerMask noRayLayer;

    private XRRayInteractor rayInteractor;

    void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        if (rayInteractor == null)
        {
            Debug.LogError("XRRayInteractor component is not attached to the GameObject.");
        }
        else
        {
            Debug.Log("XRRayInteractor is attached successfully.");
        }
    }

    void Update()
    {
        CheckRaycastHit();
    }

    private void CheckRaycastHit()
    {
        RaycastHit hitInfo;
        bool isHit = rayInteractor.TryGetCurrent3DRaycastHit(out hitInfo);

        if (isHit)
        {
            Debug.Log("Hit: " + hitInfo.collider.gameObject.name); // Log the name of the hit object
            Debug.Log("Hit Layer: " + (1 << hitInfo.collider.gameObject.layer).ToString() + ", NoRayLayer: " + noRayLayer.value.ToString());
        }

        if (isHit && ((1 << hitInfo.collider.gameObject.layer) & noRayLayer) != 0)
        {
            if (lineVisual) lineVisual.enabled = false;
            if (rayInteractor) rayInteractor.enabled = false;
            Debug.Log("Disabling ray on hitting No Ray layer.");
        }
        else
        {
            if (lineVisual) lineVisual.enabled = true;
            if (rayInteractor) rayInteractor.enabled = true;
            Debug.Log("Enabling ray.");
        }
    }
}
