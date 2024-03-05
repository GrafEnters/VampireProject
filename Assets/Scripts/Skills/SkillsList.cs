using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SkillsList", fileName = "SkillsList", order = 1)]
public class SkillsList : ScriptableObject {
    public List<SkillConfig> Skills;
}