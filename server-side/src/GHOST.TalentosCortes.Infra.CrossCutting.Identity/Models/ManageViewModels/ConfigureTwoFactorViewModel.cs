﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models.ManageViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}
