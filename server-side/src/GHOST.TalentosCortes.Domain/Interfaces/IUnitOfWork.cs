﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GHOST.TalentosCortes.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
