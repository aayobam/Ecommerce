﻿using Domain.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public required string Name { get; set; }
}
