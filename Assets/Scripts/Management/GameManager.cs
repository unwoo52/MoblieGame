using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetInstallObjectsParent
{
    GameObject GetInstallObjectsParent();
}
public class GameManager : MonoBehaviour, IGetInstallObjectsParent
{
    [SerializeField] private GameObject InstalledObjectsParent;
    private void Start()
    {
        if (!Initialize())
        {
            Debug.LogError("Init() 실패! 컴포넌트를 찾지 못했습니다.");
        }
    }
    private bool Initialize()
    {
        if(!FindOnject(ref InstalledObjectsParent, "Installed Objects Parent")) return false;

        return true;
    }

    private bool FindOnject(ref GameObject gameobject, string findName)
    {
        if (gameobject != null) return true;
        if (transform.Find(findName) == null) return false;

        gameobject = transform.Find(findName).gameObject;

        return true;
    }
    public GameObject GetInstallObjectsParent()
    {
        return InstalledObjectsParent;
    }
}
