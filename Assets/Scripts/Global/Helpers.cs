using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Constants
{
    public enum Portrait
    {
        Knight, RedEyes, NumberOfPortrait
    }
    public static class Helpers
    {

        public static float m_HeightOffTheGrid = 1.5f;
        public static  Vector3 m_CameraDistance = new Vector3(31, 28, 31);
        public static  Vector3 m_CameraAngle = new Vector3(30, 45, 0);
        
        public static float m_TacticsCameraSpeed = 2;
        public static float m_OverworldCameraSpeed = 5;
        public static bool TurnDialogueOff = false;
        public static bool m_XboxController = false;
        public static bool m_PlaystationController = true;
    }
}
