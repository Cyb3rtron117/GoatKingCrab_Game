
using UnityEngine;
using UnityEngine.EventSystems; // Required

public class GoatClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked on: " + gameObject.name);
    }
}
