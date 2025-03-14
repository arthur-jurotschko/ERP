using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Authorization
{
    public class TokenDescriptor
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int MinutesValid { get; set; }
    }
}