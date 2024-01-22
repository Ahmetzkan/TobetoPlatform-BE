﻿namespace Business.Dtos.Responses.AccountHomeworkResponses;

public class UpdatedAccountHomeworkResponse
{
    public Guid Id { get; set; }
    public Guid HomeworkId { get; set; }
    public Guid AccountId { get; set; }
    public bool Status { get; set; }
}