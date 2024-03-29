﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialMedia.Core.Interfaces
{
    /// <summary>
    /// An interface for service registeration related operations.
    /// </summary>
    public interface IServiceRegistrant
    {
        public void Register(IServiceCollection services, IConfiguration configuration);
    }
}