using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml;
using WebApi.Core.DataAccess.TcmbAccess.Abstract;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Core.DataAccess.TcmbAccess.Concrete
{

    public class TcmbAccess : ITcmbAccess
    {
        private const string urlPatter = "https://www.tcmb.gov.tr/kurlar/today.xml"; //{0}.xml
        private WebClient client;

        public CurrencyRate TcmbAccessRequest(string code)
        {
            client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            string data = client.DownloadString(urlPatter);

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(data);

            CurrencyRate currencyRate = new CurrencyRate();
            foreach (XmlNode node in doc.SelectNodes("Tarih_Date")[0].ChildNodes)
            {
                if (node.Attributes["Kod"].Value == code)
                {

                    currencyRate.CurrencyCode = node.Attributes["Kod"].Value;
                    currencyRate.ForexBuying = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                    currencyRate.ForexSelling =
                        Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                    currencyRate.BanknoteBuying =
                        Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                    currencyRate.BanknoteSelling =
                        Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));
                    currencyRate.CurrencyDate = DateTime.Now;
                }
            }

            return currencyRate;

        }

        public List<CurrencyRate> AllTcmbData()
        {

            client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            string data = client.DownloadString(urlPatter);
            List<CurrencyRate> allCurrencyRates = new List<CurrencyRate>();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(data);

            CurrencyRate currencyRate;

            foreach (XmlNode node in doc.SelectNodes("Tarih_Date")[0].ChildNodes)
            {
                currencyRate = new CurrencyRate();
                currencyRate.CurrencyCode = node.Attributes["Kod"].Value;
                currencyRate.ForexBuying = Convert.ToDecimal("0" + node["ForexBuying"].InnerText.Replace(".", ","));
                currencyRate.ForexSelling =
                    Convert.ToDecimal("0" + node["ForexSelling"].InnerText.Replace(".", ","));
                currencyRate.BanknoteBuying =
                    Convert.ToDecimal("0" + node["BanknoteBuying"].InnerText.Replace(".", ","));
                currencyRate.BanknoteSelling =
                    Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));
                currencyRate.CurrencyDate = DateTime.Now;
                
                 
                allCurrencyRates.Add(currencyRate);
            }

            return allCurrencyRates;
        }

    }

}

