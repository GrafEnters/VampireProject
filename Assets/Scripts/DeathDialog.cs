public class DeathDialog : DialogBase {
    public static bool StaticActive = false;
    private Player _player;

    public override void Set(DialogDataBase dialogDataBase) {
        base.Set(dialogDataBase);
        _player = (dialogDataBase as DeathDialogData).Player;
    }

    public override void Hide() {
        base.Hide();
        StaticActive = false;
    }

    public override void Show() {
        base.Show();
        StaticActive = true;
    }

    public void Respawn() {
        _player.transform.position = FindObjectOfType<PlayerRespawn>().transform.position;
        _player.GetComponent<HpComponent>().RefillHealth();
        Hide();
    }
}

public class DeathDialogData : DialogDataBase {
    public Player Player;
}