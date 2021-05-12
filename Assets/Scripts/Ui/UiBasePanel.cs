using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiBasePanel : UiScreen
{
    // Start is called before the first frame update

    public List<TextMeshProUGUI> m_PartyPlates;
    public UiStatus m_Status;
    private List<Creatures>m_PartyMembers;
    
    
    public override void Initialize()
    {
        m_MenuControls = new PlayerInput();

        m_MenuControls.Player.Movement.performed += movement => MoveMenuCursorPosition(movement.ReadValue<Vector2>());
        m_MenuControls.Player.CircleButton.performed += SquareButton => ReturnToLastScreen();
        m_MenuControls.Player.XButton.performed += XButton => SpawnCreatureToField();
        m_MenuControls.Disable();
    }
    
    public override void ResetCursorPosition()
    {
        m_CursorYMin = 0;
        m_CursorYMax = PartyManager.instance.m_ReservePartymembers.Count - 1;
        m_CursorYCurrent = 0;
    }
    
    public override void OnPush()
    {
        base.OnPush();
        ResetMenuText();
        AddPartyMembersToPlates();
        
    }

    public override void OnPop()
    {
        base.OnPop();
        
        InputManager.instance.m_MovementControls.Enable();
    }


    public void ResetMenuText()
    {
        for (int i = 0; i < m_PartyPlates.Count; i++)
        {
            m_PartyPlates[i].text = "";
        }
    }

    public void SpawnCreatureToField()
    {

        List<Creatures> PartyMembersInField = TacticsManager.instance.TurnOrderAlly;

        TacticsManager.instance.AddCreatureToCombat(m_PartyMembers[m_CursorYCurrent], new Vector2Int(9, 2),
            PartyMembersInField);

        PartyManager.instance.RemoveReservePartyMember(m_CursorYCurrent);
        
        TacticsManager.instance.m_TacticsCamera.m_CameraUiLayer.CameraStateChanged( TacticsManager.instance.m_TacticsCamera.m_NodeTheCameraIsOn);
        
        UiManager.instance.PopScreen();
    }

    public void AddPartyMembersToPlates()
    {
        m_PartyMembers = PartyManager.instance.m_ReservePartymembers;
        
        for (int i = 0; i < m_PartyMembers.Count; i++)
        {
            if (m_PartyPlates[i] == null)
            {
                break;
            }

            m_PartyPlates[i].text = m_PartyMembers[i].m_Name;
        }
        
        m_Status.SetCharacter(m_PartyMembers[0]);
        ResetCursorPosition();
    }

    public  override void MoveMenuCursorPosition(Vector2 aMovement)
    {
        m_CursorXPrevious = m_CursorXCurrent;
        m_CursorYPrevious = m_CursorYCurrent;

        
        m_CursorXCurrent = MenuDirectionCalculationLooping(aMovement.x, m_CursorXCurrent, m_CursorXMax, m_CursorXMin);
        m_CursorYCurrent = MenuDirectionCalculationLooping(aMovement.y, m_CursorYCurrent, m_CursorYMax, m_CursorYMin);
        
        MenuSelection(m_CursorXCurrent, m_CursorYCurrent);
    }
    
    public override void MenuSelection(int aCursorX, int aCursorY)
    {
        m_Status.SetCharacter(m_PartyMembers[aCursorY]);

    }

}
