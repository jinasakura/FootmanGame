interface ISkill {

    void Caster(int playerId);
    void Target(int playerId);
    void Condition(SkillLevelItem requirement);
    void Trigger();
}
