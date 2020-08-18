﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public class Creatures : MonoBehaviour
{

    public enum Charactertype
    {
        Undefined,
        Ally,
        Enemy
    }



    public enum ElementalStrength
    {
        Null,
        Fire,
        Water,
        Wind,
        Lighting,
        Shadow,
        Light
    }
    public enum ElementalWeakness
    {
        Null,
        Fire,
        Water,
        Wind,
        Lighting,
        Shadow,
        Light

    }

    public enum CreaturesAilment
    {
        None,
        Poison,
        Daze,
        Sleep,
        Rage,

    }
    public enum DomainStages
    {
        NotActivated,
        Encroaching,
        Finished,
        End
    }
    
    public Skills m_Attack;
    public List<Skills> m_Skills { get; protected set; }
    public List<Skills> m_BloodArts { get; protected set; }

    public AiController m_CreatureAi;

    public CreaturesAilment m_creaturesAilment;
    public Charactertype charactertype;
    public ElementalStrength elementalStrength;
    public ElementalWeakness elementalWeakness;
    public DomainStages m_DomainStages;
    public MovementType m_CreaturesMovementType;

    public MovementList m_MovementList;
    
    public int CurrentHealth;
    public int MaxHealth;
    public int CurrentMana;
    public int MaxMana;
    public int Strength;
    public int Magic;
    public int Hit;
    public int Evasion;
    public int Defence;
    public int Resistance;

    public int CurrentDomainpoints;
    public int MaxDomainPoints = 3;
    
    
    
    public int BeforeDomain_MaxHealth;
    public int BeforeDomain_MaxMana;
    public int BeforeDomain_Strength;
    
    
    public int m_CreatureMovement = 4;

    public int AmountOfTurns;

    public int BuffandDebuff;
    public int BuffDamageStrength;
    public int BuffDamageMagic;
    
    public int DebuffDamageStrength;
    public int DebuffDamageMagic;

    public bool IsSelected;
    public bool IsCurrentTurnHolder;

    public string DomainAffectingCreature;
    public string Name = "No Name";

    public Material m_Texture;

    public GameObject Model;
    public GameObject ModelInGame;

    public DomainList.DomainListEnum m_DomainList;
    public Domain m_Domain;

    public List<StatusEffects> m_StatusEffectsOnCreature;
    
    public Creatures ObjectToRotateAround;
    

    int AlimentCounter;

    bool m_IsAlive;
    protected SkillList m_CreatureSkillList;


    public virtual void Initialize()
    {
        
    }

    // Update is called once per frame
    public void SetCreature()
    {
        m_Skills = new List<Skills>();
        m_BloodArts = new List<Skills>();
        m_StatusEffectsOnCreature = new List<StatusEffects>();
        m_DomainStages = DomainStages.NotActivated;


        m_MovementList = GameManager.Instance.m_MovementList;
        m_CreatureSkillList = GameManager.Instance.m_SkillList;

        //m_Attack = gameObject.AddComponent<Attack>();

        CurrentDomainpoints = MaxDomainPoints;
    }
    public virtual void EndTurn()
    {
        if (m_StatusEffectsOnCreature.Count == 0)
        {
            return;
        }

        for(int i =  m_StatusEffectsOnCreature.Count -1 ; i >= 0;i--)
        {
            m_StatusEffectsOnCreature[i].EndOfTurn();

            if (m_StatusEffectsOnCreature[i].CheckIfStatusEffectIsActive() == false)
            {
                m_StatusEffectsOnCreature.RemoveAt(i);
            }
        }

    }

    public virtual int GetAllStrength()
    {
        int TemporaryStrength;

        TemporaryStrength = BuffDamageStrength + DebuffDamageStrength + Strength;

        return TemporaryStrength;
    }

    public virtual int GetAllMagic()
    {
        int TemporaryMagic;

        TemporaryMagic = BuffDamageMagic + DebuffDamageMagic + Magic;

        return TemporaryMagic;
    }

    public virtual IEnumerator SetStatusEffect(StatusEffects aStatusEffect)
    {
        m_StatusEffectsOnCreature.Add(aStatusEffect);
        
        yield return new WaitForEndOfFrame();
    }

    public virtual void DecrementHealth(int Decremenby)
    {
        FloatingUiElementsController.CreateFloatingText(Decremenby.ToString(), ModelInGame.gameObject.transform, FloatingUiElementsController.UiElementType.Text);
        CurrentHealth -= Decremenby;
    }
    public virtual IEnumerator DecrementHealth(int Decrementby, Skills.ElementalType elementalType,float TimeTillInitalDamage, float TimeTillHoveringUiElement, float TimeTillDamage)
    {
        if (m_creaturesAilment == CreaturesAilment.Sleep)
        {
            AlimentCounter = 0;
        }
        FloatingUiElementsController.Initalize();
        string AttackingElement = elementalType.ToString();
        string ElementalWeakness = elementalWeakness.ToString();
        string ElementalStrength = elementalStrength.ToString();
        
        int ArgumentReference = Decrementby;
        float ConvertToFloat = ArgumentReference / 1.5f;
        int ConvertToInt = Mathf.CeilToInt(ConvertToFloat);
        Decrementby = ConvertToInt;

        if (AttackingElement.Equals(ElementalWeakness))
        {
            yield return new WaitForSeconds(TimeTillHoveringUiElement);
            FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, FloatingUiElementsController.UiElementType.Weak);
        }
        if (AttackingElement.Equals(ElementalStrength))
        {
            yield return new WaitForSeconds(TimeTillHoveringUiElement);
            FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, FloatingUiElementsController.UiElementType.Strong);
        }

        yield return new WaitForSeconds(TimeTillDamage);
        FloatingUiElementsController.CreateFloatingText(Decrementby.ToString(), ModelInGame.gameObject.transform, FloatingUiElementsController.UiElementType.Text);

        CurrentHealth -= Decrementby;

        if (CurrentHealth <= 0)
        {
            m_IsAlive = false;

            Death();
        }

        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
    }


    public virtual IEnumerator IncrementHealth(int Increment)
    {
        CurrentHealth += Increment;
        yield return new WaitForSeconds(0.5f);
        FloatingUiElementsController.Initalize();
        FloatingUiElementsController.CreateFloatingText(Increment.ToString(), ModelInGame.gameObject.transform, FloatingUiElementsController.UiElementType.Text);
    }

    public virtual Charactertype GetCharactertype()
    {

        return charactertype;
    }

    public virtual void Resurrection()
    {
        ModelInGame.gameObject.SetActive(true);
    }
    public virtual void Death()
    {
        CurrentHealth = 0;
        AlimentCounter = 0;
        BuffandDebuff = 0;
        
        
        CombatManager.Instance.RemoveDeadFromList(Name,charactertype);
    }




}
