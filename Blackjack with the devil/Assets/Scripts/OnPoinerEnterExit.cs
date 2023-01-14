using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class OnPoinerEnterExit : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    protected bool isPointerIn;
    [SerializeField] protected bool isEnabled;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isEnabled == false)
            return;

        PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isEnabled == false)
            return;

        PointerExit(); 
    }

    public virtual void PointerEnter()
    {
        isPointerIn = true;
    }

    public virtual void PointerExit()
    {
       isPointerIn = false;
    }

    public void SetIfIsEnabled(bool isEnabled)
    {
        this.isEnabled = isEnabled;
    }
}
