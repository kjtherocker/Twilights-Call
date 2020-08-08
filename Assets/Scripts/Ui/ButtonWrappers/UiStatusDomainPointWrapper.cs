using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStatusDomainPointWrapper : MonoBehaviour
{
    // Start is called before the first frame update

    private float MinOpacity = 0.3f;
    private float MaxOpacity = 1.0f;
    private float HighlightSpeed= 0.4f;
    public float CurrentOpacity;
    public RawImage DomainPointNode;

    private IEnumerator coroutine;
    private Color DomainPointNodeColor;

    private bool IsDomainPointActive;
    private bool HightlightingDirection;
    private Vector3 Color;

    void Start()
    {
        //Gives us the orange color
        Color.Set(255, 225, 0);
        
        DomainPointNodeColor.a = MaxOpacity;
        DomainPointNodeColor.r = Color.x;
        DomainPointNodeColor.g = Color.y;
        DomainPointNodeColor.b = Color.z;
        
        DomainPointNode.color = DomainPointNodeColor;
        
        HightlightingDirection = false;
        
        coroutine = DPHighlighting();
        SetDomainHighlighting(true);
    }

    public void SetDomainPointOpacity(bool aDomainPointActiveStatus)
    {
        IsDomainPointActive = aDomainPointActiveStatus;
        DomainPointNodeColor.a = IsDomainPointActive ? MaxOpacity : MinOpacity;
        DomainPointNode.color = DomainPointNodeColor;
    }



    public void SetDomainHighlighting(bool aDomainHightlighting)
    {

        if (aDomainHightlighting)
        {
           // CurrentOpacity = 0;
            StartCoroutine(coroutine);
        }
        else
        {
           // CurrentOpacity = 1;
           // StopCoroutine(coroutine);
           // DomainPointNode.color = DomainPointNodeColor;
        }
    }
    
    public IEnumerator DPHighlighting()
    {
    Debug.Log("yall is shit happeninbg");
        float TempHighlightSpeed = HighlightSpeed * Time.deltaTime;
        
        float TempOpacityAddition = HightlightingDirection ?  TempHighlightSpeed * -1 :  TempHighlightSpeed ;

        CurrentOpacity += TempOpacityAddition;
        
        DomainPointNodeColor.a = CurrentOpacity;
        DomainPointNode.color = DomainPointNodeColor;
        
        if (CurrentOpacity > MaxOpacity)
        {
            HightlightingDirection = true;
        }
        if (CurrentOpacity < MinOpacity)
        {
            HightlightingDirection = false;
        }

        yield return new WaitForEndOfFrame();
        Debug.Log("yall is shi2131231231232131t happeninbg");
        StartCoroutine(coroutine);

    }

}
