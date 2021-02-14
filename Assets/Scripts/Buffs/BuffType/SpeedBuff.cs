using UnityEngine;

public class SpeedBuff: Buff
{
    private readonly AIEntity entity;

    public SpeedBuff(ScriptableObjectBuff buff, GameObject obj) : base(buff, obj)
    {
        if (obj.TryGetComponent(out AIEntity newEntity))
        {
            entity = newEntity;
        }
    }

    protected override void ApplyEffect()
    {
        SpeedBuffScriptable appliedBuff = (SpeedBuffScriptable)buffData;
        entity.Speed += appliedBuff.parameter;
    }

    public override void End()
    {
        SpeedBuffScriptable appliedBuff = (SpeedBuffScriptable)buffData;
        entity.Speed -= appliedBuff.parameter * stacks;
        stacks = 0;
        isFinished = true;
    }
}