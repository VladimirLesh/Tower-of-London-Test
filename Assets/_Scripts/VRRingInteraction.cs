using UnityEngine;

public class VRRingInteraction : MonoBehaviour
{
    // [SerializeField] private XRController controller;
    // [SerializeField] private XRRayInteractor rayInteractor;
    //
    // private void OnEnable()
    // {
    //     rayInteractor.onSelectEntered.AddListener(HandleGrab);
    //     rayInteractor.onSelectExited.AddListener(HandleRelease);
    // }
    //
    // private void HandleGrab(XRBaseInteractable interactable)
    // {
    //     var ring = interactable.GetComponent<Ring>();
    //     if(ring != null && ring.IsTopRing)
    //     {
    //         selectedRing = ring;
    //     }
    // }
    //
    // private void HandleRelease(XRBaseInteractable interactable)
    // {
    //     if(selectedRing == null) return;
    //     
    //     rayInteractor.GetCurrentRaycastHit(out RaycastHit hit);
    //     var peg = hit.collider?.GetComponent<Peg>();
    //     if(peg != null)
    //     {
    //         TowerOfLondonController.Instance.OnRingSelected(selectedRing, peg);
    //     }
    //     selectedRing = null;
    // }
}