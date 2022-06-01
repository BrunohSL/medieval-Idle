using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerClickHandler {
    public TabGroup tabGroup;
    public Image backgroundImage;

    void Start() {
        backgroundImage = GetComponent<Image>();
        tabGroup.subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData) {
        tabGroup.onTabSelected(this);
    }
}
