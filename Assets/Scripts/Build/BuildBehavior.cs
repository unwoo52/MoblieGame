using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildBehavior : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _buildObjectParentPrefab;
    [SerializeField] private GameObject _buildObjectParent;
    [SerializeField] private GameObject _buildObject;


    //init object
    private GameObject CancelUI;
    private GameObject CancelUIBigRect;
    private InventoryManagement _inventoryManagement;
    [SerializeField] private GameObject _canvas;
    private IToggleUI itoggleUI;
    private Transform inventory;
    private float distanceCancelBuildEffectOn = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Initialize()) Debug.LogError("Fail Initialize");
        InstantiateBuildObject();
        SetActiveExitImageAndHideInventoryUI(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //get mouse Pos
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CancelUIBigRect.GetComponent<RectTransform>(), eventData.position, eventData.enterEventCamera, out mousePos);

        //calculating distance
        distanceCancelBuildEffectOn = Mathf.Abs((mousePos.x - CancelUIBigRect.GetComponent<RectTransform>().rect.center.x) / (CancelUIBigRect.GetComponent<RectTransform>().rect.width * 2));

        if (Is_CancelBuildEffectOn())
        {
            SetActive_UIParticle(true);
            SetActive_Buildobject(false);
            Effect_Exitbuild();
        }
        else
        {
            SetActive_UIParticle(false);
            SetActive_Buildobject(true);
            //locate buildobject at mouse pos
        }        
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //check pos
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out pos);

        //if on exit build image > Cancelbuild();
        if (RectTransformUtility.RectangleContainsScreenPoint(
            CancelUI.GetComponent<CancelImageUI>().CancelImageBigRect.GetComponent<RectTransform>(), Input.mousePosition))
        {
            Cancelbuild();
        }
        //else if on ground >
        else Cancelbuild();//testcode
            //if can building place > CompleteBuild();
            //else cannot building place > Cancelbuild();

        //if dis is
        //  >set ptc
        //else
        //  >deactive ptc
    }

    //OnBeginDrag
    #region .
    private bool Initialize()
    {
        if (!GetInventoryTransform()) return false;
        if (!GetCanvas()) return false;
        return true;
    }
    private void InstantiateBuildObject()
    {
        _buildObjectParent = Instantiate(_buildObjectParentPrefab);
        GameObject gameObject = transform.parent.GetComponent<IGetBuildObject>().GetbuildObject();
        _buildObject = Instantiate(gameObject);
        _buildObject.transform.SetParent(_buildObjectParent.transform);
    }
    private void SetActiveExitImageAndHideInventoryUI(bool isActiveUI)
    {
        itoggleUI.ToggleUIWidth();
        _inventoryManagement.CancelUI.SetActive(isActiveUI);
        GameObject ToggleUI = _inventoryManagement.ToggleUI;
        ToggleUI.GetComponent<Image>().color = isActiveUI ? new Color(0, 0, 0, 0) : ToggleUI.GetComponent<ToggleUI>().OriginColor;
    }
    #endregion

    //OnDrag
    #region .
    private bool Is_CancelBuildEffectOn()
    {
        return distanceCancelBuildEffectOn < 1;
    }
    private void SetActive_Buildobject(bool setActiveValue)
    {
        _buildObject.SetActive(setActiveValue);
    }
    private void SetActive_UIParticle(bool setActiveValue)
    {
        CancelUI.GetComponent<CancelImageUI>().UiParticle.SetActive(setActiveValue);
    }
    private void Effect_Exitbuild()
    {
        CancelUIBigRect.GetComponent<RectTransform>().localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 0), new Vector3(1, 1, 0), distanceCancelBuildEffectOn);
        CancelUI.GetComponent<CancelImageUI>().UiParticleEndPos.GetComponent<RectTransform>().position = Input.mousePosition;
    }

    #endregion

    //OnEndDrag
    #region .
    private void CompleteBuild()
    {
        //building object parent set to GM.IGetIOP()
    }
    private void Cancelbuild()
    {
        //remove building object //buildObject.IBuildingProcess.CancelBuilding
        _buildObjectParent.GetComponent<ICancelbuild>().CancelBuild();

        //inactive ExitImage and toggle Inventory
        SetActiveExitImageAndHideInventoryUI(false);
    }
    #endregion


    /*codes*/
    #region .
    private bool GetCanvas()
    {
        CanvasManagement.Instance.GetCanvas(ref _canvas);
        return _canvas != null;

    }
    private bool GetInventoryTransform()
    {
        CanvasManagement.Instance.GetInventory(out inventory);
        _inventoryManagement = inventory.GetComponent<InventoryManagement>();
        itoggleUI = _inventoryManagement.ToggleUI.GetComponent<IToggleUI>();
        CancelUI = _inventoryManagement.CancelUI;
        CancelUIBigRect = CancelUI.GetComponent<CancelImageUI>().CancelImageBigRect;
        return inventory != null && _inventoryManagement != null && itoggleUI != null && CancelUIBigRect != null && CancelUI != null;
    }
    #endregion
}
