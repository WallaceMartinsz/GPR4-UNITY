using UnityEngine;

public class ContainerController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Transform item;


    public bool HaveItem()
    {
        return this.item != null;
    }

    public Transform GetItem()
    {
        Transform itemToReturn = this.item;
        this.item = null;

        return itemToReturn;
    }

    public void SetItem(Transform _item)
    {
        this.item = _item;
    }



}
