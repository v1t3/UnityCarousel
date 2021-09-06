using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Carousel
{
    public class ScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect rect;

        [SerializeField] private HorizontalLayoutGroup layoutGroup;

        [SerializeField] private float scrollSpeed = 1;
        private float _step;
        private float _targetPosition;
        private float _currentPosition;

        private void Start()
        {
            var childCount = layoutGroup.gameObject.transform.childCount;

            layoutGroup.CalculateLayoutInputHorizontal();
            _step = layoutGroup.preferredWidth / (layoutGroup.preferredWidth * (childCount - 1));
        }

        public void NavButtonRight_Click()
        {
            if (rect.horizontalNormalizedPosition < 1)
            {
                _targetPosition = Mathf.Round((rect.horizontalNormalizedPosition + _step) * 100f) * 0.01f;
                StartCoroutine(ScrollToRight());
            }
            else
            {
                rect.horizontalNormalizedPosition = 1;
            }
        }

        public void NavButtonLeft_Click()
        {
            if (rect.horizontalNormalizedPosition > 0)
            {
                _targetPosition = Mathf.Round((rect.horizontalNormalizedPosition - _step) * 100f) * 0.01f;
                StartCoroutine(ScrollToLeft());
            }
            else
            {
                rect.horizontalNormalizedPosition = 0;
            }
        }

        private IEnumerator ScrollToRight()
        {
            while (_currentPosition < 1 && _currentPosition < _targetPosition)
            {
                yield return new WaitForEndOfFrame();
                _currentPosition = rect.horizontalNormalizedPosition;
                rect.horizontalNormalizedPosition += _step * scrollSpeed * Time.deltaTime;
            }

            // If scrolled over
            if (_currentPosition > _targetPosition)
            {
                rect.horizontalNormalizedPosition = _targetPosition;
            }
        }

        private IEnumerator ScrollToLeft()
        {
            while (_currentPosition > 0 && _currentPosition > _targetPosition)
            {
                yield return new WaitForEndOfFrame();
                _currentPosition = Mathf.Round(rect.horizontalNormalizedPosition * 100f) * 0.01f;
                rect.horizontalNormalizedPosition -= _step * scrollSpeed * Time.deltaTime;
            }

            // If scrolled over
            if (_currentPosition < _targetPosition)
            {
                rect.horizontalNormalizedPosition = _targetPosition;
            }
        }
    }
}