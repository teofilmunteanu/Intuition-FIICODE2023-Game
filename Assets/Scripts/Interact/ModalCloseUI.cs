using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModalCloseUI : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected bool mouseOver = false;

    protected void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        if (!mouseOver)
        {
            PauseManager.Resume();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}