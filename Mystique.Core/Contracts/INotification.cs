﻿using Mystique.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mystique.Core.Contracts
{
    public interface INotification<out T> where T : EventBase
    {
        void Handle(EventBase eventObj);
    }
}