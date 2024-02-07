[System.Serializable]
public class Resource {
  public string Name;
  public int Amount;

  public Resource Clone => new Resource() {
    Name = this.Name,
    Amount = this.Amount
  };
}