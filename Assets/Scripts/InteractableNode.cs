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
        public bool sRemoveItem = false;

        [SerializeField]
        private UnityEvent m_OnChange = new UnityEvent();

        //public event Action OnStateChange;
        public bool ConditionMet(Actions action, InventoryItem item)
        {
            if (sAction == action && // if the action matches
                (sAction != Actions.UseItem || sItem == item)) // if the action is UseItem, make sure the item is correct
                return true;
            return false;
        }
        public void StateChangeEffect()
        {

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
            contentPosition.x = startingx;
            contentPosition.width = startingwidth;
            contentPosition.y += 20;
            //contentPosition.height += 30;
            EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("m_OnChange"), new GUIContent("OnChange"));
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
        if (ChangeConditions[state].sRemoveItem) // sRemoveItem should only be able to be true if it is an item condition anyway
            Player.instance.RemoveInvItem(Player.instance.GetHeldItem());
        state++;

        stateText.text = gameObject.name + " has advanced to state " + state;
        Debug.Log(gameObject.name + " has advanced to state " + state);
    }
}
