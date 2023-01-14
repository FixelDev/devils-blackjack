using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OPEE_Show : OnPoinerEnterExit
{
    [SerializeField] private GameObject gameObjectToShow;

    public override void PointerEnter()
    {
        base.PointerEnter();
        gameObjectToShow.SetActive(true);
    }

    public override void PointerExit()
    {
        base.PointerExit();
        gameObjectToShow.SetActive(false);
    }
}
