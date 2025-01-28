using UnityEngine;

public class RingInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask ringLayer;
    [SerializeField] private float maxPickDistance = 10f;
    
    private Ring selectedRing;
    private Camera mainCamera;
    

    private void Start()
    {
        mainCamera = Camera.main;
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit, maxPickDistance, ringLayer))
        {
            var ring = hit.collider.GetComponent<Ring>();
            
            if(ring != null && ring.IsTopRing())
            {
                selectedRing = ring;
                ring.PlayClick();
                // HighlightValidPegs();
            }
            else
            {
                SoundController.Instance.PlayError();
            }
        }
        else if(selectedRing != null)
        {
            TryMoveRing();
        }
    }

    private void TryMoveRing()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, maxPickDistance))
        {
            var peg = hit.collider.GetComponent<Peg>();
            if(peg != null)
            {
                SoundController.Instance.PlayMove();
                TowerOfLondonController.Instance.OnRingSelected(selectedRing, peg);
            }
            else
            {
                SoundController.Instance.PlayError();
            }
        }
        selectedRing = null;
    }
}