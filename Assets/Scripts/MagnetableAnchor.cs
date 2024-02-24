using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class MagnetableAnchor: MonoBehaviour {


    public float Radius = 1;

    public AnchorType AnchorType = AnchorType.Big;

    public Color GetColor() =>
        AnchorType switch {
            AnchorType.Big => Color.green,
            _ => Color.black
        };

    public Vector3 TryShift() {
        Collider[] cols = Physics.OverlapSphere(transform.position, Radius * transform.lossyScale.x, LayerMask.GetMask("Anchor"));
        if (cols.Any(col => col.GetComponent<MagnetableAnchor>().AnchorType == AnchorType)) {
            return cols[0].transform.position - transform.position;
        }

        return Vector3.zero;

    }
}

public enum AnchorType {
    Big,
    Medium,
    Small
}