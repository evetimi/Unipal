using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomGridLayout : MonoBehaviour
{
    public Vector2 spacing;

    void Update()
    {
        if (transform.childCount == 0) {
            return;
        }

        Transform tryGetFirstChild = GetFirstActiveChildren();
        if (tryGetFirstChild == null) {
            return;
        }

        Vector2 current = tryGetFirstChild.position;
        int count = transform.childCount;
        for (int i = 0; i < count; i++) {
            Transform child = transform.GetChild(i);
            if (!child.gameObject.activeSelf) {
                continue;
            }

            child.position = current;
            current += spacing;
        }
    }

    private Transform GetFirstActiveChildren() {
        int count = transform.childCount;
        for (int i = 0; i < count; i++) {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeSelf) {
                return child;
            }
        }

        return null;
    }
}
