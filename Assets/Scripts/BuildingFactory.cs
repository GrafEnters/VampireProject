using System;
using UnityEngine;

public class BuildingFactory : MonoBehaviour {
    [SerializeField]
    private FoundamentCC _foundamentCc;

    //TODO Add pooling
    public static IBuildable GetPrefabByType(BuildingType type) {
        var factory = FindObjectOfType<BuildingFactory>();
        switch (type) {
            case BuildingType.Foundament:
                return Instantiate(factory._foundamentCc);
                break;

            case BuildingType.Wall:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return null;
    }
}