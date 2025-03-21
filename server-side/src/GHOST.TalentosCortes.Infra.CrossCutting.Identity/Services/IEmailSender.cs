﻿using System.Threading.Tasks;

namespace GHOST.TalentosCortes.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
