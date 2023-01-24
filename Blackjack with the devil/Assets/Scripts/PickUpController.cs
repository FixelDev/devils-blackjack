using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    private Heart focusedHeart;
    private bool isPickingUpInputClicked;
    private bool isPickedUp;
    private Vector3 offset;
    private HeartsDepthController heartsDepthController;
    private int[] rotationDirections = {-1, 1};
    private int randomRotationDirection;

    private void Start() 
    {
        heartsDepthController = FindObjectOfType<HeartsDepthController>();    
    }

    private void Update() 
    {
        if(CheckIfMouseIsInPickingZone() == false)
        {
            isPickingUpInputClicked = false;
            PickUpAndReleaseHeart();
            
            return;
        }
        
        DetectHearts();
        PickUpInput();
    }

    private bool CheckIfMouseIsInPickingZone()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f, LayerMask.GetMask("PickingZone"));

        return raycastHit.collider != null;
    }

    private void DetectHearts()
    {
        if(isPickingUpInputClicked)
            return;

        RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1000f, LayerMask.GetMask("Heart"));

        if(raycastHit.collider != null && raycastHit.collider.tag == "Heart")
        {
            if(focusedHeart == null || focusedHeart.gameObject != raycastHit.collider.gameObject)
            {
                focusedHeart = raycastHit.collider.GetComponent<Heart>();
            }
                
        }
        else
        {
            focusedHeart = null;
        }
    }

    private void PickUpInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(focusedHeart != null)
            {
                offset = CalculateOffsetBetweenHeartAndCursor(); 
                heartsDepthController.RiseHeartToTop(focusedHeart.gameObject);
            }
        }
        if(Input.GetMouseButton(0))
        {
            isPickingUpInputClicked = true;
            PickUpAndReleaseHeart();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isPickingUpInputClicked = false;
            PickUpAndReleaseHeart();
        }
        
    }

    private Vector2 CalculateOffsetBetweenHeartAndCursor()
    {
        return focusedHeart.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void PickUpAndReleaseHeart()
    {
        if(focusedHeart == null)
            return;

        if(isPickingUpInputClicked)
        {
            if(isPickedUp == false)
            {
                focusedHeart.PickUp();
                randomRotationDirection = rotationDirections[Random.Range(0, rotationDirections.Length)];
                isPickedUp = true;
            }
                
            SnapHeartToCursor();
            RotateHeart();
        }
        else
        {
            if(isPickedUp)
            {
                focusedHeart.Release();
                isPickedUp = false;
            }
            
        }
    }

    private void SnapHeartToCursor()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        newPosition.z = focusedHeart.transform.position.z;
        focusedHeart.transform.position = newPosition;
    }

    private void RotateHeart()
    {
        focusedHeart.transform.Rotate(Vector3.forward * 12f * randomRotationDirection * Time.deltaTime);
    }
}
