using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ChatRoom.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers.Api
{
    public class StockController : Controller
    {        
        // GET: StockController/Details/5
        public ActionResult Get(string id)
        {
            var url = $"https://stooq.com/q/l/?s={id}&f=sd2t2ohlcv&h&e=csv";
            var ret = Post(url, "GET");   

            return Ok(ret);
        }


        private static Stock Post(string url, string method)
        {
            Stock stock = null;
            try
            {                                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;                
                request.ContentType = "text/csv";                                                

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.Created)
                    {
                        string message = String.Format(
                        "POST failed. Received HTTP {0}",
                        response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    else
                    {
                        WebHeaderCollection header = response.Headers;
                        var encoding = ASCIIEncoding.ASCII;
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                        {
                            var count = 0;
                            
                            while (!reader.EndOfStream)
                            {
                                var splits = reader.ReadLine().Split(',');                                
                                if(count == 1)
                                {
                                    try
                                    {
                                        var formatProvider = new System.Globalization.CultureInfo("en-US");
                                        stock = new Stock();
                                        stock.Symbol = splits[0];
                                        stock.Date = DateTime.Parse($"{splits[1]} {splits[2]}");
                                        stock.Open = decimal.Parse(splits[3], formatProvider);
                                        stock.High = decimal.Parse(splits[4], formatProvider);
                                        stock.Low = decimal.Parse(splits[5], formatProvider);
                                        stock.Close = decimal.Parse(splits[6], formatProvider);
                                        stock.Volume = decimal.Parse(splits[7], formatProvider);
                                        stock.Succsess = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        stock =  new Stock();
                                        stock.Succsess = false;
                                        stock.Error = ex.Message;
                                    }
                                  
                                }
                                count++;
                            }
                        }

                      
                    }
                }
            }            
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    var httpResponse = (HttpWebResponse)response;

                    using (Stream data = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(data);                        

                        stock = new Stock();
                        stock.Succsess = false;
                        stock.Error = sr.ReadToEnd();
                    }
                }
            }
            return stock;
        }
    }
}
