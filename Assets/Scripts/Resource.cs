using System;

public struct Resource {
   

    public ResourceType Type;
    public int Amount;

    public static Resource Empty = new Resource() {
        Type = ResourceType.Empty,
        Amount = 0
    };
    
    public Resource One => new Resource() {
        Type = this.Type,
        Amount = 1
    };

    #region Overrided operators
    public static bool operator ==(Resource res1, Resource res2) {
        return res1.Type == res2.Type;
    }

    public static bool operator !=(Resource res1, Resource res2) {
        return !(res1 == res2);
    }
    
    public bool Equals(Resource other) {
        return Type == other.Type && Amount == other.Amount;
    }

    public override bool Equals(object obj) {
        return obj is Resource other && Equals(other);
    }

    public override int GetHashCode() {
        return HashCode.Combine((int)Type, Amount);
    }
    

    #endregion
}

[Serializable]
public class SerializableResource {
    public ResourceType Type;
    public int Amount;

    public Resource Resource => new Resource() {
        Type = Type,
        Amount = Amount
    };
}