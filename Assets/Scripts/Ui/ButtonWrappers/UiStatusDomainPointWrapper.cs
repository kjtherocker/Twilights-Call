using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiStatusDomainPointWrapper : MonoBehaviour
{
    // Start is called before the first frame update

    private float MinimiumOpacity = 0.5f;
    private float MaxOpacity = 1.0f;
    private float CurrentOpacity;
    public RawImage DomainPointNode;

    private Color DomainPointNodeColor;

    private bool IsDomainPointActive;
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
    }

    public void SetDomainPointOpacity(bool aDomainPointActiveStatus)
    {
        IsDomainPointActive = aDomainPointActiveStatus;
        DomainPointNodeColor.a = IsDomainPointActive ? MaxOpacity : MinimiumOpacity;
        DomainPointNode.color = DomainPointNodeColor;
    }

}
