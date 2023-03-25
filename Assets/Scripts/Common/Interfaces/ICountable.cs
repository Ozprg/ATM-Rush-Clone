using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICountable
{
    void CountMoney(int meshIndex);
    void GetMeshIndex(CollectibleMeshController collectibleMeshController);

}
