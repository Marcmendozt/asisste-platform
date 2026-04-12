using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XAMAsisste.Servicios.Interfaces;

[assembly: Dependency(typeof(ILocSettings))]
namespace XAMAsisste.Servicios.Interfaces
{
    public interface ILocSettings
    {
        string OpenSettings();

    }
}
