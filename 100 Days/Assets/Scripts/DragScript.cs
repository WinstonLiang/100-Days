using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour {

    //Contains functions to be used for dragging units on Squad Panel    
    private RectTransform battleRect, reservesRect;
    private MenuManager menuManager;
    private GameObject clickedGameObject;
    private UnitManager unitManager;

    //TODO: Add four recttransform slots and assign them in OnDragEndBattleMember

    void Start()
    {
        battleRect = GameObject.Find("Battle Panel").GetComponent<RectTransform>();
        reservesRect = GameObject.Find("Panel DragUnits").GetComponent<RectTransform>();
        unitManager = GameObject.Find("UnitsData").GetComponent<UnitManager>();
        menuManager = GameObject.Find("MenuCanvas").GetComponent<MenuManager>();
    }

    //Utility function for more readable code    
    /// <summary>
    /// Returns whether the RectTransform contains the position.
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    bool RectContainsPosition(RectTransform rect, Vector2 pos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, pos, Camera.main);
    }

    /// <summary>
    /// Returns image slot number based on image name (-1 if there is no match).
    /// </summary>
    /// <param name="imgName"></param>
    /// <returns>int image number</returns>
    int GetClickedImageNumber(string imgName)
    {
        if (imgName == "Image1")
            return 0;
        else if (imgName == "Image2")
            return 1;
        else if (imgName == "Image3")
            return 2;
        else if (imgName == "Image4")
            return 3;

        //No match, Reserve was clicked
        return -1;
    }

    //Battle Members
    public void OnDragBattleMember(BaseEventData bData)
    {
        PointerEventData pData = bData as PointerEventData;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        pData.pointerPress.transform.position = new Vector2(mousePos.x, mousePos.y);
        clickedGameObject = pData.pointerPress;
    }

    public void OnMemberClicked(BaseEventData bData)
    {
        PointerEventData pData = bData as PointerEventData;

        //Set the panel to overlap the other panel
        if (GetClickedImageNumber(pData.pointerEnter.name) == -1)
            reservesRect.transform.parent.parent.SetAsLastSibling();
        else
        {            
            battleRect.transform.parent.SetAsLastSibling();
        }            
    }

    public void OnDragEndBattleMember(BaseEventData bData)
    {
        PointerEventData pData = bData as PointerEventData;

        //Center of sprite is inside Battle Member Panel
        if (RectContainsPosition(battleRect, pData.position) && 
            GetClickedImageNumber(clickedGameObject.name) == -1 &&
            unitManager.GetBattleSquadCount() < unitManager.maxPlayers)
        {
            if (UnitManager.DEBUG) print("In BM");
            //A reserve unit was dragged and battle members count is less than the max (4)
            unitManager.getReserves()[clickedGameObject.transform.GetSiblingIndex()].squad = 0;
            menuManager.renderData("Squad Panel");
        }
        else if (RectContainsPosition(reservesRect, pData.position) && 
                 clickedGameObject.transform.parent.parent.GetSiblingIndex() < unitManager.GetBattleSquadCount())
        {
            //Center of sprite is inside Reserves Panel
            if (UnitManager.DEBUG) print("In Reserves");
            unitManager.getBattlingSquad()[clickedGameObject.transform.parent.parent.GetSiblingIndex()].squad = 1;
            menuManager.renderData("Squad Panel");
        }

        //Return sprite to original location
        if (UnitManager.DEBUG) print("Return to initPos");        
        pData.lastPress.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }

    public void Test()
    { }
}
