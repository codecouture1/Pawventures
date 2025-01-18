using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomToggle : MonoBehaviour, IPointerClickHandler
{
    public Toggle toggle;
    public InventoryInterface inventory;
    public int slot;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Left-click: Select the toggle
            toggle.isOn = true; // Ensure the toggle behaves normally
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.UnequipItem(slot);
        }
    }
}
