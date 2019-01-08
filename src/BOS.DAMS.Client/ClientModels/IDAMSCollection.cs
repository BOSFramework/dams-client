using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.DAMS.Client.ClientModels
{
    public interface IDAMSCollection
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        List<IAsset> Assets { get; set; }
        bool Deleted { get; set; }
    }
}
