using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI; // FOR DEBUG TEXT;
public enum Actions
{
    Interact,
    UseItem,
    Look
}


public class InteractableNode : ClickableNode
{
    [System.Serializable]
    public class StateChange // holds conditional data for when to change state
    {
        public Actions sAction;
        //item info
        public InventoryItem sItem = InventoryItem.None;
        public bool sRemoveItem = false;
        public bool ConditionMet(Actions action, InventoryItem item)
        {
            if (sAction == action && // if the action matches
                (sAction != Actions.UseItem || sItem == item)) // if the action is UseItem, make sure the item is correct
                return true;
            return false;
        }
    };
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(StateChange))]
    public class StateChangeDrawer : PropertyDrawer // properly displays it on inspector
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;

            var category = property.FindPropertyRelative("sAction");

            switch ((Actions) category.intValue)
            {
                case Actions.UseItem: // format it so that both are shown
                    contentPosition.width *= 0.4f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
                    contentPosition.x += contentPosition.width;
                    contentPosition.width *= 0.45f / 0.4f;
                    EditorGUIUtility.labelWidth = 25f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sItem"), new GUIContent("Item"));
                    contentPosition.x += contentPosition.width;
                    contentPosition.width *= 0.15f / 0.4f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sRemoveItem"), new GUIContent("Rem"));

                    break;
                default: // only show action as the item is irrelevant
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
                    break;
            }
        }
    }
#endif
    [SerializeField]
    int state = 0;
    int maxstate; // prevents changing state when at last state
    [SerializeField]
    StateChange[] ChangeConditions; // lists conditional data to move on to next state

    [Header("Debug")]
    [SerializeField]
    Text stateText;

    void Start()
    {
        maxstate = ChangeConditions.Length;
    }

    override public void Interact(Actions action, InventoryItem item)
    {
        if(state < maxstate)
            if (ChangeConditions[state].ConditionMet(action,item))
                    AdvanceState();
    }
    void AdvanceState()
    {
        if (ChangeConditions[state].sRemoveItem) // sRemoveItem should only be able to be true if it is an item condition anyway
            Player.instance.RemoveInvItem(Player.instance.GetHeldItem());
        state++;

        stateText.text = gameObject.name + " has advanced to state " + state;
        Debug.Log(gameObject.name + " has advanced to state " + state);
    }
}
