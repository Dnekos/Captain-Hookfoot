//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI; // FOR DEBUG TEXT;
using UnityEngine.Events;

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
        public bool sRemoveItem = false; // remove item from inventory when used
        public bool sRepeatOnSceneEnter = false; // if the OnChange should be called at start
        public bool sHasNote = false;
        [SerializeField]
        private UnityEvent m_OnChange = new UnityEvent();

        //public event Action OnStateChange;
        public bool ConditionMet(Actions action, InventoryItem item)
        {
            if (sAction == action && // if the action matches
                (sAction != Actions.UseItem || sItem == item) && // if the action is UseItem, make sure the item is correct
                (!sHasNote || Player.instance.ContainsItem(InventoryItem.Note))) // 
                return true;
            return false;
        }
        public void InvokeChange()
        {
            m_OnChange.Invoke();
        }
    };
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(StateChange))]
    public class StateChangeDrawer : PropertyDrawer // properly displays it on inspector
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            //action.arraySize
            return base.GetPropertyHeight(property, label) + 100;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            Rect contentPosition = EditorGUI.PrefixLabel(position, label);
            EditorGUI.indentLevel = 0;

            var category = property.FindPropertyRelative("sAction");
            float startingx = contentPosition.x, startingwidth = contentPosition.width;
            switch ((Actions) category.intValue)
            {
                case Actions.UseItem: // format it so that both are shown
                    contentPosition.width *= 0.4f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
                    contentPosition.x += contentPosition.width;
                    contentPosition.width *= 0.42f / 0.4f;
                    EditorGUIUtility.labelWidth = 25f;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sItem"), new GUIContent("Item"));
                    contentPosition.x += contentPosition.width;
                    contentPosition.width *= 0.18f / 0.4f;
                    contentPosition.height -= 100;
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sRemoveItem"), new GUIContent("Rem"));
                    contentPosition.height += 100;
                    break;
                default: // only show action as the item is irrelevant
                    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sAction"), GUIContent.none);
                    break;
            }
            contentPosition.x = startingx;
            contentPosition.width = startingwidth;
            contentPosition.y += 20;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("m_OnChange"), new GUIContent("OnChange"));
            contentPosition.x = 47;
            contentPosition.height -= 100;
            EditorGUIUtility.labelWidth = 70f;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sRepeatOnSceneEnter"), new GUIContent("Run at Start"));
            contentPosition.y += 15;
            contentPosition.height += 10;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("sHasNote"), new GUIContent("has Note"));

            //serializedObject.ApplyModifiedProperties();

        }
    }
#endif

    int maxstate; // prevents changing state when at last state
    [SerializeField]
    StateChange[] ChangeConditions; // lists conditional data to move on to next state


    [Header("Debug")]
    [SerializeField]
    Text stateText;

    void Start()
    {
        maxstate = ChangeConditions.Length;
        
        state = Player.instance.GetState(UID); // check if this item has had its state changed
        // check if the current state has a result that is persistent
        if (state != 0 && ChangeConditions[state - 1].sRepeatOnSceneEnter) 
            ChangeConditions[state - 1].InvokeChange();
    }

    override public void LookAt()
    {
        base.LookAt();
        checkStateCondition(Actions.Look);
    }
    override public void Interact(InventoryItem item)
    {
        base.Interact(item);

        if (item == InventoryItem.None)
            checkStateCondition(Actions.Interact);
        else
            checkStateCondition(Actions.UseItem, item);
    }
    void checkStateCondition(Actions action, InventoryItem item = InventoryItem.None)
    {
        if (state < maxstate)
            if (ChangeConditions[state].ConditionMet(action, item))
                AdvanceState();
    }
    void AdvanceState()
    {
        ChangeConditions[state].InvokeChange(); // activate Event
        state++;

        Player.instance.LogState(UID, state); // log the current state for transitions

        if (ChangeConditions[state - 1].sRemoveItem) // sRemoveItem should only be able to be true if it is an item condition anyway
            Player.instance.RemoveInvItem(Player.instance.GetHeldItem());
    }
}
