using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomGridLayout : MonoBehaviour
{
    public bool updateEveryFrame = true;
    public int horizontalAmount = 2;
    public Vector2 spacing;

    void Update()
    {
        if (!updateEveryFrame || transform.childCount == 0) {
            return;
        }

        UpdatePosition();
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

    public void UpdatePosition() {
        Transform tryGetFirstChild = GetFirstActiveChildren();
        if (tryGetFirstChild == null) {
            return;
        }

        Vector2 current = tryGetFirstChild.position;
        float baseX = current.x;

        int count = transform.childCount;
        int currentHorizontal = 0;

        for (int i = 0; i < count; i++) {
            Transform child = transform.GetChild(i);
            if (!child.gameObject.activeSelf) {
                continue;
            }

            child.position = current;

            currentHorizontal++;

            if (currentHorizontal < horizontalAmount) {
                current = new Vector2(current.x + spacing.x, current.y);
            } else {
                current = new Vector2(baseX, current.y + spacing.y);
                currentHorizontal = 0;
            }
        }
    }

    /// <summary>
    /// UNFINISHED!!!
    /// </summary>
    /// <param name="childIndex"></param>
    public void SetPositionAt(int childIndex) {
        if (transform.childCount <= childIndex) {
            return;
        }

        Vector2 current = transform.GetChild(childIndex).position;
        float baseX = current.x;

        int count = transform.childCount;
        int currentHorizontal = 0;

        for (int i = childIndex; i < count; i++) {
            Transform child = transform.GetChild(i);
            if (!child.gameObject.activeSelf) {
                continue;
            }

            child.position = current;

            currentHorizontal++;

            if (currentHorizontal < horizontalAmount) {
                current = new Vector2(current.x + spacing.x, current.y);
            } else {
                current = new Vector2(baseX, current.y + spacing.y);
                currentHorizontal = 0;
            }
        }

        currentHorizontal = 0;
        for (int i = childIndex - 1; i >= 0; i--) {
            Transform child = transform.GetChild(i);
            if (!child.gameObject.activeSelf) {
                continue;
            }

            child.position = current;

            currentHorizontal++;

            if (currentHorizontal < horizontalAmount) {
                current = new Vector2(current.x + spacing.x, current.y);
            } else {
                current = new Vector2(baseX, current.y + spacing.y);
                currentHorizontal = 0;
            }
        }
    }
}
