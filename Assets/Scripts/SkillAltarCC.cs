public class SkillAltarCC : BuildableCC , ICreateDialogData{
    public DialogDataBase GetDialogData(object obj = null) {
        return new SkillAltarDialogData();
    }
}