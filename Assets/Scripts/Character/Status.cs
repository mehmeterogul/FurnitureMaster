public class Status
{
    public MovementStatus Movement_Stat;
    public GatherStatus Gather_Stat;

    public enum MovementStatus
    {
        Idle,
        Walking,
    }

    public enum GatherStatus
    {
        Cutting,
        Digging,
        NotGathering
    }
}
