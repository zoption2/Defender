using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGridLayout : MonoBehaviour
{
    [SerializeField] private Paddings _paddings;
    [SerializeField] private Vector2 _spacing;
    [SerializeField] private int _collumns;
    [SerializeField] private int _rows;
    [SerializeField] private SpriteRenderer _imageRenderer;

    [ContextMenu("Do Layout")]
    public void DoLayout()
    {
        if (_imageRenderer == null)
        {
            return;
        }
        var image = _imageRenderer.bounds.size;

        int childs = transform.childCount;
        var verticalSize = image.y;
        var verticalSizeWithSpacing = (verticalSize - (_paddings.Top + _paddings.Bottom)) - ((_rows - 1) * _spacing.y);
        var yPosOffset = verticalSizeWithSpacing / _rows;

        var horizontalSize = image.x;
        var horizontalSizeWithSpacing = (horizontalSize - (_paddings.Left + _paddings.Right)) - ((_collumns - 1) * _spacing.x);
        var xPosOffset = horizontalSizeWithSpacing / _collumns;

        Vector2 startPoint = new Vector2(-(horizontalSizeWithSpacing / 2) + _paddings.Left
            , -(verticalSizeWithSpacing / 2) + _paddings.Bottom);

        float yPos = startPoint.y + yPosOffset/2;
        float xPos = startPoint.x + xPosOffset/2;
        int counter = 0;
        for (int i = 0; i < _collumns; i++)
        {
            var tempYPos = yPos + Mathf.Abs(yPosOffset * (i));
            for (int s = 0; s < _rows; s++)
            {
                var tempXPos = xPos + Mathf.Abs(xPosOffset * (s));
                Transform child = transform.GetChild(counter);
                if (child != null)
                {
                    child.localPosition = new Vector2(tempXPos, tempYPos);
                }
                counter++;
            }
        }
    }


    [System.Serializable]
    private class Paddings
    {
        public float Top { get; set; }
        public float Bottom { get; set; }
        public float Left { get; set; }
        public float Right { get; set; }
    }
}
