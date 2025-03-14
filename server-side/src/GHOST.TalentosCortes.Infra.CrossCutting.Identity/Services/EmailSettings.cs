using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services
{
    public class EmailSettings
    {
        public string PrimaryDomain = "smtp.live.com";
        public int PrimaryPort = 587;
        public string UsernameEmail = "arthur.jurotschko@hotmail.com";
        public string UsernamePassword = "Amjvlmsj7691*";
        public string FromEmail = "fromEmail";
      //  public string ToEmail = "arthur.jurotschko@hotmail.com";
        public string CcEmail = "";
    }
}