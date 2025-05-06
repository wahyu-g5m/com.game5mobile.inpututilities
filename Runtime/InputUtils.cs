using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Five.Utils
{
    public static class InputUtils
    {
        private static List<RaycastResult> raycastResult = new();

        public static bool IsPointerOverLayer(int layerIndex)
        {
            return IsRaycastOverLayer(GetEventSystemRaycastResults(), layerIndex);
        }

        public static bool IsPointerOverLayer(int touchIndex, int layerIndex)
        {
            return IsRaycastOverLayer(GetEventSystemRaycastResults(touchIndex), layerIndex);
        }

        public static bool IsPointerOverLayer(string layerName)
        {
            var layerIndex = LayerMask.NameToLayer(layerName);
            return IsPointerOverLayer(layerIndex);
        }

        public static bool IsPointerOverLayer(int touchIndex, string layerName)
        {
            var layerIndex = LayerMask.NameToLayer(layerName);
            return IsRaycastOverLayer(GetEventSystemRaycastResults(touchIndex), layerIndex);
        }

        public static bool IsCustomPointerOverLayer(int layerIndex, Vector3 customPointerPosition)
        {
            return IsRaycastOverLayer(GetCustomEventSystemRaycastResults(customPointerPosition), layerIndex);
        }

        public static bool IsCustomPointerOverLayer(string layerName, Vector3 customPointerPosition)
        {
            var layerIndex = LayerMask.NameToLayer(layerName);
            return IsCustomPointerOverLayer(layerIndex, customPointerPosition);
        }

        private static List<RaycastResult> GetEventSystemRaycastResults()
        {
            raycastResult.Clear();
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(eventData, raycastResult);
            return raycastResult;
        }

        private static List<RaycastResult> GetEventSystemRaycastResults(int touchIndex)
        {
            raycastResult.Clear();
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.touches[touchIndex].position;
            EventSystem.current.RaycastAll(eventData, raycastResult);
            return raycastResult;
        }

        private static List<RaycastResult> GetCustomEventSystemRaycastResults(Vector3 customPointerPosition)
        {
            raycastResult.Clear();
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = customPointerPosition;
            EventSystem.current.RaycastAll(eventData, raycastResult);
            return raycastResult;
        }

        private static bool IsRaycastOverLayer(List<RaycastResult> eventSystemRaycastResults, int layerIndex)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                var curRaysastResult = eventSystemRaycastResults[index];
                if (curRaysastResult.gameObject.layer == layerIndex)
                    return true;
            }
            return false;
        }

        public static bool IsPointerOverTag(string tag)
        {
            return IsRaycastOverTag(GetEventSystemRaycastResults(), tag);
        }

        private static bool IsRaycastOverTag(List<RaycastResult> eventSystemRaycastResults, string tag)
        {
            for (int index = 0; index < eventSystemRaycastResults.Count; index++)
            {
                var curRaysastResult = eventSystemRaycastResults[index];
                if (curRaysastResult.gameObject.CompareTag(tag))
                    return true;
            }
            return false;
        }
    }
}