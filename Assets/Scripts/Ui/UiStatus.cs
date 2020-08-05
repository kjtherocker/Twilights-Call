﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiStatus : UiTabScreen
{

    public TextMeshProUGUI Text_Name;
    public RawImage Image_Portrait;
    public Creatures Creature;


    private int m_CurrentHealth = 150;
    private int m_MaxHealth = 150;

    private int m_CurrentMana = 150;

    public bool m_IsSelected;


    public Slider m_HealthbarSlider;
    public List<UiStatusDomainPointWrapper> m_DomainSliders;

    public TextMeshProUGUI Text_Strength;
    public TextMeshProUGUI Text_Magic;
    public TextMeshProUGUI Text_Hit;
    public TextMeshProUGUI Text_Defence;
    public TextMeshProUGUI Text_Resistance;
    public TextMeshProUGUI Text_Evasion;

    // Use this for initialization
    void Start()
    {
        UpdateHealthbar();
        m_IsSelected = false;
    }

    public void SetCharacter(Creatures Character)
    {
        
        gameObject.SetActive(true);
        if (Creature != null)
        {
            Creature.gameObject.layer = 0;

        }

        Creature = Character;
        
        if (Text_Name != null)
        {
            Text_Name.text = Creature.Name;
        }

        m_CurrentHealth = Creature.CurrentHealth;
        m_MaxHealth = Creature.MaxHealth;

        m_HealthbarSlider.value = m_CurrentHealth / m_MaxHealth;

        m_CurrentMana = Creature.CurrentMana;


        StartCoroutine(CatchAFrame());
     
    }

    IEnumerator CatchAFrame()
    {
        yield return new WaitForEndOfFrame();
       
       UpdateHealthbar();
       foreach (UiStatusDomainPointWrapper aSlider in m_DomainSliders)
       {
           aSlider.SetDomainPointOpacity(false);
       }

       for (int i = 0; i < Creature.CurrentDomainpoints; i++)
       {
           m_DomainSliders[i].SetDomainPointOpacity(true);
       }
    }

    private void Update()
    {
        UpdateHealthbar();

        if (Creature != null)
        {

            m_CurrentHealth = Creature.CurrentHealth;
            m_MaxHealth = Creature.MaxHealth;

            m_CurrentMana = Creature.CurrentMana;
        

          // if(m_PortraitCamera != null)
          // {
          //     m_PortraitCamera.gameObject.transform.position = new Vector3(Partymember.ModelInGame.transform.position.x,
          //      Partymember.ModelInGame.transform.position.y + 1.7f, Partymember.ModelInGame.transform.position.z) +
          //       Partymember.ModelInGame.transform.forward;
          //      m_PortraitCamera.gameObject.transform.rotation = Partymember.ModelInGame.transform.rotation;

          //      Quaternion Rotation = Partymember.ModelInGame.transform.rotation;

          //      m_PortraitCamera.transform.eulerAngles = new Vector3(Rotation.eulerAngles.x, Rotation.eulerAngles.y + 180, Rotation.eulerAngles.z);
          // }
        }
    }
    // Update is called once per frame
    void UpdateHealthbar()
    {
        if (Creature == null)
        {
            return;
        }

        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
        }
        m_CurrentHealth = Mathf.Max(m_CurrentHealth, 0);
        m_CurrentHealth = Mathf.Min(m_CurrentHealth, m_MaxHealth);
        

        if (Text_Strength != null)
        {
            Text_Strength.text = "" + Creature.Strength;
            Text_Magic.text = "" + Creature.Magic;
            Text_Hit.text = "" + Creature.Hit;
            Text_Defence.text = "" + Creature.Defence;
            Text_Resistance.text = "" + Creature.Resistance;
            Text_Evasion.text = "" + Creature.Evasion;
        }

        float HealthRatio = (float)m_CurrentHealth / (float)m_MaxHealth;
        m_HealthbarSlider.value  = HealthRatio;

    }
}
