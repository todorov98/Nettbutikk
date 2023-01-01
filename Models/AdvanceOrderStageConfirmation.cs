using System;

public class AdvanceOrderStageConfirmation
{
    public Guid OrderId { get; set; }
    public DateTime StageAdvanceDate { get; set; }
    public string PreviousStage { get; set; }
    public string NewStage { get; set; }
    public bool AdvancedByAdmin { get; set; }

    public AdvanceOrderStageConfirmation(Guid id, string previousStage, string newStage, bool byAdmin)
    {
        OrderId = id;
        StageAdvanceDate = DateTime.Now.AddMilliseconds(-5.0);
        PreviousStage = previousStage;
        NewStage = newStage;
        AdvancedByAdmin = byAdmin;
    }

    public AdvanceOrderStageConfirmation()
    {

    }
}
