using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivElement : MonoBehaviour
{
    [SerializeField] private GameObject FlagEmb;
    [SerializeField] private GameObject MessegEmb;
    public void Flag()
    {
        FlagEmb.SetActive(true);
    }
    public void DFlag()
    {
        FlagEmb.SetActive(false);
    }
    public void MessEmb()
    {
        MessegEmb.SetActive(true);
    }
    public void DMessEmb()
    {
        MessegEmb.SetActive(false);
    }
}
