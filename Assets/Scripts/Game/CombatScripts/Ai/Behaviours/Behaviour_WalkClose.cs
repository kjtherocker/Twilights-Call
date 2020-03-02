using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_WalkClose : Behaviour
{
    public override Creatures AllyToAttack(List<Creatures> aCharacterList)
    {

        List<Creatures> characterlist = aCharacterList;
        
        
     //   for (int i = 0; i < characterlist.Count; i++)
     //   {
     //       for (int j = 0; j < characterlist.Count; j++)
     //       {
     //           if (characterlist[j].CurrentHealth < characterlist[j + 1].CurrentHealth)
     //           {
     //               Creatures tempA = characterlist[j];
     //               Creatures tempB = characterlist[j + 1];
     //               swap(ref tempA, ref tempB);
     //           }
     //       }
     //   }

        return characterlist[0];
    }
}
