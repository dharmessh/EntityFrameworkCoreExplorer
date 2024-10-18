using System;
using System.Collections.Generic;

namespace Explorer.Data.ReverseEngineeredModels;

public partial class Team
{
    public int TeamId { get; set; }

    public string? Name { get; set; }

    public string CreationDate { get; set; } = null!;
}
