namespace LightManager.Logic.Dmx512.Commands;

public class CreateStageLightingPlan : Command<CreateStageLightingPlanData>
{
    public CreateStageLightingPlan(DateTime time, CreateStageLightingPlanData data) : base(time, data)
    {
    }
}

public record CreateStageLightingPlanData(string Name);