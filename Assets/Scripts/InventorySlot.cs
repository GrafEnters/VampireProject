using UnityEngine;

public class InventorySlot {
    public Resource Resource = Resource.Empty;

    public bool IsEmpty => Resource == Resource.Empty;

    public bool CheckResource(Resource res) {
        return Resource == res;
    }

    public bool CanAddResource(Resource res) {
        return Resource == res || Resource == Resource.Empty;
    }

    public int AddResource(Resource res) {
        if (Resource == res) {
            Resource.Amount += res.Amount;
        }else if (Resource == Resource.Empty) {
            Resource = res;
        }
        return res.Amount;
    }

    public int RemoveResource(Resource res) {
        if (Resource == res) {
            Resource.Amount -= res.Amount;
            if (Resource.Amount <= 0) {
                if (Resource.Amount < 0) {
                    Debug.LogError("Resource amount < 0!");
                }

                Resource = Resource.Empty;
            }
        } else {
            Debug.LogError($"Resource remove resource, because type mismatch {Resource.Type} != {res.Type}!");
        }

        return res.Amount;
    }
}