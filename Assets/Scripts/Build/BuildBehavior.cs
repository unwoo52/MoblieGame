using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildBehavior : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject _buildObjectParentPrefab;
    [SerializeField] private GameObject _buildObjectParent;
    [SerializeField] private GameObject _buildObject;

    //init object
    private Vector2 ExitImagePosition;
    private InventoryManagement _inventoryManagement;
    [SerializeField] private GameObject _canvas;
    private IToggleUI itoggleUI;
    private Transform inventory;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Initialize()) Debug.LogError("Fail Initialize");
        InstantiateBuildObject();
        SetActiveExitImageAndHideInventoryUI(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //if pos at Exit Image
        //effect Red
        //inactive buildobject

        // if close to EXIT image... do lerp
        Vector2 ExitImagePosition = 
        eventData.position;

        //pos tranlate at screen to ray
            //buildobject pos ray hit

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //check pos
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out pos);

        //if on exit build image > Cancelbuild();
        if (RectTransformUtility.RectangleContainsScreenPoint(
            _inventoryManagement.CancelUI.transform.GetChild(0).GetComponent<RectTransform>(), Input.mousePosition))
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
    private bool GetExitImagePosition()
    {
        ExitImagePosition = _inventoryManagement.CancelUI.GetComponent<RectTransform>().rect.position;
        return ExitImagePosition != null;
    }
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
        return inventory != null && _inventoryManagement != null && itoggleUI != null;
    }
    #endregion
}
