using UnityEngine;

public class DataModifier : MonoBehaviour {

    [SerializeField] private string modifiedData;
    
    [ContextMenu("Modify")]
    public void Modify() {
        DataManager.Instance.Data = modifiedData;
    }
    
}