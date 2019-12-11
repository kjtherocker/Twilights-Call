using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_WalkClose : Behaviour
{
    public override Creatures AllyToAttack(List<Creatures> aCharacterList)
    {

//        for (int i = 0; i < aCharacterList.Count; i++)
//        {
//            for (int j = 0; j < aCharacterList.Count; j++)
//            {
//                if (aCharacterList[j].CurrentHealth < aCharacterList[j + 1].CurrentHealth)
//                {
//                    Creatures tempA = aCharacterList[j];
//                    Creatures tempB = aCharacterList[j + 1];
//                    swap(ref tempA, ref tempB);
//                }
//            }
//        }

        return aCharacterList[0];
    }
}
