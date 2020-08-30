using System;
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
    public MovementType m_CreaturesMovementType;

    public MovementList m_MovementList;
    
    public int CurrentHealth;
    public int MaxHealth;
    
    
    public int Strength;
    public int BuffStrength;
    public int DebuffStrength;
    public int DomainStrength;

    public int Magic;
    public int BuffMagic;
    public int DebuffMagic;
    public int DomainMagic;
    
    public int Hit;
    public int BuffHit;
    public int DebuffHit;
    public int DomainHit;
    
    public int Evasion;
    public int BuffEvasion;
    public int DebuffEvasion;
    public int DomainEvasion;
    
    public int Defence;
    public int BuffDefence;
    public int DebuffDefence;
    public int DomainDefence;
    
    
    public int Resistance;
    public int BuffResistance;
    public int DebuffResistance;
    public int DomainResistance;

    public int CurrentDomainpoints;
    public int MaxDomainPoints = 3;

    public int m_CreatureMovement = 4;

    public int AmountOfTurns;
    

    public string DomainAffectingCreature;
    public string Name = "No Name";

    public Material m_Texture;

    public GameObject Model;
    public GameObject ModelInGame;

    public DomainList.DomainListEnum m_DomainList;
    public Domain m_Domain;
    public Devour m_Devour;

    public List<StatusEffects> m_StatusEffectsOnCreature;
    
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
        
        m_Devour = new Devour();

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
        int TemporaryStat;

        TemporaryStat = BuffStrength + DebuffStrength + Strength + DomainStrength;

        return TemporaryStat;
    }

    public virtual int GetAllMagic()
    {
        int TemporaryStat;

        TemporaryStat = BuffMagic + DebuffMagic + Magic + DomainMagic;

        return TemporaryStat;
    }
    
    
    public virtual int GetAllHit()
    {
        int TemporaryStat;

        TemporaryStat = BuffHit + DebuffHit + Hit + DomainHit;

        return TemporaryStat;
    }

    public virtual int GetAllEvasion()
    {
        int TemporaryStat;

        TemporaryStat = BuffEvasion+ DebuffEvasion + Evasion + DomainEvasion;

        return TemporaryStat;
    }
    
    
    public virtual int GetAllDefence()
    {
        int TemporaryStat;

        TemporaryStat = BuffDefence + DebuffDefence + Defence + DomainDefence;

        return TemporaryStat;
    }

    public virtual int GetAllResistance()
    {
        int TemporaryMagic;

        TemporaryMagic = BuffResistance + DebuffResistance + Resistance + DomainResistance;

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

        DeathCheck();
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

    public void DeathCheck()
    {
        if (CurrentHealth <= 0)
        {
            m_IsAlive = false;

            Death();
        }

        CurrentHealth = Mathf.Min(CurrentHealth, MaxHealth);
    }

    public virtual void Death()
    {
        CurrentHealth = 0;
    

        CombatManager.Instance.RemoveDeadFromList(charactertype);
    }




}
