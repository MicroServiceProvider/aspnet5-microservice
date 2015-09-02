﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Framework.Configuration;

namespace AspNet5.Microservice
{
    /// <summary>
    ///  A static class that groups together instances of IConfiguration and allows them to be accessed anywhere in the application
    /// </summary>
    public class AppConfig : ConfigurationSource
    {
        public static Dictionary<string, IConfiguration> Sources { get; } = new Dictionary<string, IConfiguration>();
        public static string ApplicationEnvironment { get; set; }

        /// <summary>
        ///  Add an IConfiguration instance to the AppConfig object
        ///  <param name="configuration">IConfiguration instance</param>
        ///  <param name="sourceName">Name that can be used to refer to this instance</param>
        /// </summary>
        public static void AddConfigurationObject(IConfiguration configuration, string sourceName)
        {
            Sources.Add(sourceName, configuration);
        }

        /// <summary>
        ///  Remove an IConfiguration instance from the AppConfig object
        ///  <param name="sourceName">Name of the IConfiguration instance to remove</param>
        /// </summary>
        public static void RemoveConfigurationObject(string sourceName)
        {
            Sources.Remove(sourceName);
        }

        /// <summary>
        ///  Get the value of a specified key from a named IConfiguration instance
        ///  <param name="sourceName">Name of IConfiguration instance which to retrieve the key from</param>
        ///  <param name="key">Name of the key</param>
        /// </summary>
        public static string Get(string sourceName, string key)
        {
            return Sources[sourceName].GetSection(key).Value;
        }

        /// <summary>
        ///  Set the value of a specified key in a named IConfiguration instance
        ///  <param name="sourceName">Name of IConfiguration instance where the key is to be set</param>
        ///  <param name="key">Name of the key</param>
        ///  <param name="value">Value to assign to the key</param>
        /// </summary>
        public static void Set(string sourceName, string key, string value)
        {
            Sources[sourceName].GetSection(key).Value = value;
        }
        

        /// <summary>
        ///  Get all values from the specified configuration instance
        ///  <param name="sourceName">Name of IConfiguration instance where the key is to be set</param>
        ///  <returns>Returns a dictionary containing the configuration key-value pairs</returns>
        /// </summary>
        public static Dictionary<string,string> GetAllValues(string sourceName)
        {
            if (!Sources.ContainsKey(sourceName))
            {
                throw new InvalidOperationException("No configuration source registered with name "+ sourceName);
            }
            
            // Get children of this source and return them
            IConfiguration sourceConfiguration = Sources[sourceName];
            Dictionary<string, string> valuesDictionary = sourceConfiguration.GetChildren().ToDictionary(child => child.Key, child => child.Value);
            return valuesDictionary;
        }

    }

}
