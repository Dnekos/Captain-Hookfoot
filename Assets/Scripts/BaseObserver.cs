using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObserver : MonoBehaviour
{
    public virtual void AddNode(GameObject node, Vector2 loc)
    {

    }
    public virtual void ChangeSprite(Sprite newimage, SpriteRenderer changed_component)
    {

    }
}
