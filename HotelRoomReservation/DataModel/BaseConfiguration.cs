﻿using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DataModel
{
    public abstract class BaseConfiguration
    {
        protected IConfiguration Configuration { get; }

        protected BaseConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
            BindConfiguration(Configuration);
        }

        protected virtual void BindConfiguration(IConfiguration configuration)
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var configValue = configuration[property.Name];

                if (configValue != null)
                {
                    var convertedValue = Convert.ChangeType(configValue, property.PropertyType);
                    property.SetValue(this, convertedValue);
                }
            }
        }
    }
}
