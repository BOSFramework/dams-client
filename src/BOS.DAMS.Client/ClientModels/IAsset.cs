using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.DAMS.Client.ClientModels
{
    public interface IAsset
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string URL { get; set; }
        string MIMEType { get; set; }
        string ThumbnailURL { get; set; }
        string FileExtension { get; set; }
        float? Size { get; set; }
        bool Deleted { get; set; }
    }
}
