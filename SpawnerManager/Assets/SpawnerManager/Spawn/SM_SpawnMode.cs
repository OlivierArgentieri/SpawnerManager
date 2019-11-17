using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SM_SpawnMode
{
    public SM_SpawnType Type = SM_SpawnType.Circle;
    
    
    public SM_CircleMode CircleMode = new SM_CircleMode();
    public SM_LineMode LineMode= new SM_LineMode();
    public SM_PointMode PointMode = new SM_PointMode();

    public SM_Mode Mode
    {
        get
        {
            switch (Type)
            {
                case SM_SpawnType.Circle:
                    return CircleMode;

                case SM_SpawnType.Line:
                    return LineMode;
                
                case SM_SpawnType.Point:
                    return PointMode;

            }

            return null;
        }
        private set{}
    }
}


public enum SM_SpawnType
{
    Circle,
    Line,
    Point
}