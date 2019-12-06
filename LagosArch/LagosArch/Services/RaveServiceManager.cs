using LagosArch.Models;
using RaveDotNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagosArch.Services
{
    public class RaveServiceManager
    {
        public RaveService InitiatePay(Payment payment)
        {
            ConfigModel config = new ConfigModel()
            {
                Meta = new List<string>(),
                RedirectUrl = AppConstant.BaseUrl + "Landing", //callback to retrieve payment status
                Env = ConfigModel.LIVE, //or ConfigModel.LIVE (for production)
                PublicKey = AppConstant.PublicKey,
                SecretKey = AppConstant.SecretKey
            };

            PaymentRequestModel request = new PaymentRequestModel()
            {
                CustomerEmail = payment.CustomerEmail,
                CustomerPhone = payment.CustomerPhone,
                Amount = payment.Amount,
                Country = "Nigeria",
                Currency = "NGN",
                CustomDescription = payment.CustomDescription,
                CustomerFirstname = payment.CustomerFirstname,
                CustomerLastname = payment.CustomerLastname,
                CustomTitle = payment.CustomTitle,
                CustomLogo = "https://cms.suregifts.com.ng/utilitypics/catholicarchdioceselagos.png",
                PayButtonText = "Pay Me"
            };

            return new RaveService(config, request);
        }
    }
}
