using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum Actions
{
    Interact,
    UseWithItem
}


public class InteractableNode : ClickableNode
{
    [System.Serializable]
    public class StateChange
    {
        public Actions sAction;
       
        public InventoryItems sItem = InventoryItems.None;
    };
    [CustomPropertyDrawer(typeof(StateChange))]
    public class StateChangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;

            var category = property.FindPropertyRelative("sAction");

            switch ((Actions) category.intValue)
            {
                case Actions.UseWithItem: // format it so that both are shown
                    contentPosition.width *= 0.4f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
                    contentPosition.x += contentPosition.width;
                    contentPosition.width *= 0.6f / 0.4f;
                    EditorGUIUtility.labelWidth = 25f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sItem"), new GUIContent("Item"));
                    break;
                default: // only show action as the item is irrelevant
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
                    break;
            }

            /*
            contentPosition.width *= 0.5f;
            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
            contentPosition.x += contentPosition.width;
            EditorGUIUtility.labelWidth = 14f;

            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sItem"), new GUIContent("C"));*/
            EditorGUI.EndProperty();

        }
    }

    [SerializeField]
    int state = 0;
    int maxstate; // prevents changing state when at last state
    [SerializeField]
    StateChange[] ChangeConditions; // lists conditional data to move on to next state

    void Start()
    {
        maxstate = ChangeConditions.Length;
    }

    override public void Interact(Actions action, InventoryItems item)
    {
        if (ChangeConditions[state].sAction == action && state < maxstate)
        {
            if (ChangeConditions[state].sAction == Actions.UseWithItem && ChangeConditions[state].sItem == item)
                AdvanceState();
        }
    }
    void AdvanceState()
    {
        state++;
    }
}
