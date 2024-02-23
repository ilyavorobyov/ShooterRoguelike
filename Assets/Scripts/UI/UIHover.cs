using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Vector3 _hoverSize = new Vector3(1.05f, 1.05f, 1.05f);

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = _hoverSize;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }
    }
}