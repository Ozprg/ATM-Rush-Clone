using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectibleMeshController : MonoBehaviour
{
    [SerializeField] Mesh[] moneyMeshArray;
    [SerializeField] private Transform _body;
    private MeshFilter meshFilter;
    public Transform body => _body;
    public Mesh[] meshes => moneyMeshArray;
    


    private void Awake()
    {
        meshFilter = _body.GetComponent<MeshFilter>();
    }

    public void UpgradeBody(int level)
    {
        if (level <= moneyMeshArray.Length)
        {
            meshFilter.mesh = moneyMeshArray[level - 1];           
        }
        
    } 
}
