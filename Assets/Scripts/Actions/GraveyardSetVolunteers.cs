using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GraveyardSetVolunteers : ActionButton
{
    public UnityEvent Click;

    private void Start()
    {
        //заглушка базового старта
    }

    public override void OnClick()
    {
        Debug.Log("CountVolunteer Clicked");
        Click?.Invoke();
    }


}
