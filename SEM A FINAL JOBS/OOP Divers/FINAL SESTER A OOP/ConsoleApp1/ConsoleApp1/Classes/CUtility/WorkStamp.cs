/// <summary>
/// This class will have both starting work and ending work time of a club / other working place
/// </summary>
class WorkStamp
{
    // Properties \\
    public DateOnly startDate { get; set; }
    public DateOnly endDate { get; set; }

    // Constructor \\
    public WorkStamp(DateOnly startDate = default, DateOnly endDate = default)
    {
        this.startDate = startDate;
        this.endDate = endDate;
    }

}