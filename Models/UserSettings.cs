using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using ExArbete.Data;
using System.Collections.Generic;

namespace ExArbete.Models
{
    public class UserSettings
    {
        [MinLength(4, ErrorMessage = "Username too short. Minimum characters is 4.")]
        public string? NewUsername { get; set; }
        // [AllowFileSize(FileSize = 512001, ErrorMessage = "Maximum allowed file size is 512kB")]
        public IReadOnlyList<IBrowserFile>? NewProfileImage { get; set; }
        public bool EnableNotifications { get; set; }

    }
}