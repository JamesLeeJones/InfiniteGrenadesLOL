

namespace Exiled.Example
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    using UnityEngine;

    public sealed class Config : IConfig
    {
        
        public bool IsEnabled { get; set; } = true;

       

        
    }
}
