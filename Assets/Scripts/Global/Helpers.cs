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
        public static  Vector3 m_CameraCloseDistance = new Vector3(15, 20, 10);
        public static  Vector3 m_CameraAngle = new Vector3(45, 55, 0);
        
        public static  Vector3 m_CameraCloseDistance2 = new Vector3(15, 20, 15);
        public static  Vector3 m_CameraAngle2= new Vector3(40, 55, 0);
        
        public static float m_TacticsCameraSpeed = 6;
        public static float m_OverworldCameraSpeed = 5;
        public static bool TurnDialogueOff = false;
        public static bool m_XboxController = false;
        public static bool m_PlaystationController = true;
    }
}
