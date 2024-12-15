using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrower : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject itemPrefab;

    public void ThrowItem()
    {
        GameObject item = Instantiate(itemPrefab, throwPoint.position, itemPrefab.transform.rotation);
        // Keep a reference to the original scale of the item
        Vector3 origScale = item.transform.localScale;
        // Set item's horizontal direction based on the player's facing direction
        item.transform.localScale = new Vector3(origScale.x * transform.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);

    }
}
