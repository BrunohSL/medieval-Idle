using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour {
    public List<TabButton> tabButtons;
    public List<GameObject> objectsToSwap;

    public Sprite tabIdle;
    public Sprite tabActive;
    public TabButton selectedTab;

    public void subscribe(TabButton button) {
        if (tabButtons == null) {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void onTabSelected(TabButton button) {
        selectedTab = button;
        resetTabs();
        button.backgroundImage.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++) {
            if (i == index) {
                objectsToSwap[i].SetActive(true);
            } else {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void resetTabs() {
        foreach (TabButton button in tabButtons) {
            if (selectedTab != null && button == selectedTab) { continue; }

            button.backgroundImage.sprite = tabIdle;
        }
    }
}
