﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiDomainTab : MonoBehaviour
{
    public Animator DomainAnimator;
    public Domain m_Domain;
    public TextMeshProUGUI m_Text_DomainUser;
    public TextMeshProUGUI m_Text_DomainName;
    public TextMeshProUGUI m_Text_DomainDescription;
    public int m_CommandBoardPointerPosition;
    
    // Use this for initialization
	void Start ()
    {

    }
    
    public  void OnPop()
    {
        //m_CommandBoardAnimator.SetTrigger("t_CommandBoardCrossOut");
        TurnCommandBoardOff();
        

    }

    public  void OnPush(Domain aDomain)
    {
        gameObject.SetActive(true);
        SetDomainReference(aDomain);
    }

    public void TurnCommandBoardOff()
    {

        gameObject.SetActive(false);
        m_CommandBoardPointerPosition = 0;


    }

    public void SetDomainReference(Domain aDomain)
    {
        m_Text_DomainUser.text = aDomain.DomainUser;
        m_Text_DomainName.text = aDomain.DomainName;
        m_Text_DomainDescription.text = aDomain.DomainDescription;

    }

    
}