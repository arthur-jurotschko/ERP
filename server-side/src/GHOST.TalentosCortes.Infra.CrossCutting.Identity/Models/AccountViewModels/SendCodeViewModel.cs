﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
