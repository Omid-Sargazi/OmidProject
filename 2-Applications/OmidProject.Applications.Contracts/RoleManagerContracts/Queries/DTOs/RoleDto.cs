﻿namespace OmidProject.Applications.Contracts.RoleManagerContracts.Queries.DTOs;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}