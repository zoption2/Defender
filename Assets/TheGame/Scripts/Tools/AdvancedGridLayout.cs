using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Collections;

public class AdvancedGridLayout : GridLayoutGroup
{
    [SerializeField] private bool _autoSizeVertical;
    [SerializeField] private bool _autoSizeHorizontal;
    [SerializeField] private bool _isSquare;
    [SerializeField] private bool _useMaxCellSize;
    [SerializeField] private Vector2 _maxCellSize = new Vector2(100, 100);

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        UpdateCellSize();
    }

    /// <summary>
    /// Based on rect size modify cell size to achieve correct proportions
    /// </summary>
    private void UpdateCellSize()
    {
        var tempSize = cellSize;
        if (_autoSizeVertical)
        {
            float height = rectTransform.rect.height;
            float sizeY = (height - ((m_ConstraintCount + 1) * spacing.y + (padding.vertical * 2))) / m_ConstraintCount;

            tempSize.y = sizeY;
            if (_isSquare)
            {
                tempSize.x = sizeY;
            }
        }
        if (_autoSizeHorizontal)
        {
            float width = rectTransform.rect.width;
            float sizeX = (width - ((m_ConstraintCount + 1) * spacing.x + (padding.horizontal * 2))) / m_ConstraintCount;
            tempSize.x = sizeX;
            if (_isSquare)
            {
                tempSize.y = sizeX;
            }
        }

        if (_useMaxCellSize)
        {
            tempSize.x = tempSize.x > _maxCellSize.x ? _maxCellSize.x : tempSize.x;
            tempSize.y = tempSize.y > _maxCellSize.y ? _maxCellSize.y : tempSize.y;
        }
        cellSize = tempSize;
    }
}

