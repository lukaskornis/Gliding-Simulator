using UnityEditor;
using UnityEngine;

public class AutoFocusObjectInEditor : MonoBehaviour
{
#if UNITY_EDITOR
    private void Start()
    {
        Selection.activeObject = gameObject;
    }
#endif
}