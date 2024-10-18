using System;
using System.Collections.Generic;

namespace Explorer.Data.ReverseEngineeredModels;

public partial class Coach
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CreationDate { get; set; } = null!;
}
