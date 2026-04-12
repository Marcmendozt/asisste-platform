using Android.Telephony;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XAMAsisste.Servicios.Clases;
using XAMAsisste.Servicios.Interfaces;
[assembly: Dependency(typeof(ServiceImei))]

namespace XAMAsisste.Servicios.Clases
{
    public class ServiceImei : IServiceImei
    {
        public string GetImei()
        {
            try
            {
                TelephonyManager oTelephonyManager = (TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);
                return oTelephonyManager.Imei;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
