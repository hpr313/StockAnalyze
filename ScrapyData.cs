using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SharedProject.Model;

namespace JapanStock2023.Model
{
    public class ScrapyData
    {
        
        WebDriver driver = new FirefoxDriver(@"geckodriver.exe");
        List<string> dateList = new List<string>();
        List<StockData> stockDatas = new List<StockData>();

        string path0 = "//*[@id=\"KM_TABLECONTENT0\"]/div[1]/table/tbody";
        string pathPage = "//*[@id=\"KM_TABLEINDEX0\"]/div/table/tbody/tr/td";
        string pathDate = "//*[@id=\"KM_TABLECONTENT0\"]/div[1]/table/caption";
        string pathList = "//*[@id=\"KM_TABLECONTENT0\"]/div[1]/table/tbody/tr";  // Main items
        // each column
        //*[@id="tdr0c1"]/a
        string stockNumber_path = "/td[2]/a";
        string stockName_path = "/td[3]/a";
        string stockMarket_path = "/td[4]/span";
        string stockIndustry_path = "/td[5]";
        string stockValue_path = "/td[6]";
        string stockDayBeforeDiff_path = "/td[7]";
        string stockDayBeforeRatio_path = "/td[8]";
        string stockPER_path = "/td[9]";
        string stockPBR_path = "/td[10]";
        string stockDevidedYield_path = "/td[11]";
        string stockROE_path = "/td[12]";
        string stockAggregateValue_path = "/td[13]";
        string stockMovingAverage_path = "/td[14]";
        string stockEquityRatio_path = "/td[15]";

        public string apiURL(string _name)
        {
            //return $"http://localhost:80/baseballnews/api/home/{_name}/";
            return $"http://localhost:5230/api/home/{_name}/";
        }
        public List<StockData> sendBack()
        {
            return stockDatas;
        }
        public decimal assessBeforeDay(string xxx)
        {
            decimal value;
            if (xxx.Contains("-"))
            {
                value = -1 * decimal.Parse(xxx.Substring(1, xxx.Length - 1));
                return value;
            }
            else
            {
                value = decimal.Parse(xxx);
                return value;
            }
        }
        public decimal assessMovingAverage(string xxx)
        {
            decimal value;
            if (xxx.Contains("-"))
            {
                value = -1 * decimal.Parse(xxx.Substring(1, xxx.Length - 1));
                return value;
            }
            else
            {
                value = decimal.Parse(xxx);
                return value;
            }
        }
        public void getDate()
        {
            IWebElement element = driver.FindElement(By.XPath(pathDate));
            string[] datetime = element.Text.Split('：');
            dateList.Add(datetime[1].Substring(0, datetime[1].Length - 1).Replace('/', '-'));
        }
        public void gotoURL(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void selectMarket()
        {
            IWebElement element = driver.FindElement(By.Id("exch_config"));
            element.FindElement(By.Id("exch_all")).Click();
            Thread.Sleep(1000);
        }
        public int getStocksLength()
        {
            Thread.Sleep(500);
            IWebElement stocks = driver.FindElement(By.XPath(path0));
            IList<IWebElement> stockList = stocks.FindElements(By.XPath(pathList));
            int length = stockList.Count;
            return length;
        }

        public string getNextPage()
        {
            IList<IWebElement> pages = driver.FindElements(By.XPath(pathPage));
            string nextPage = pathPage + $"[{pages.Count}]/a";
            string next;
            try
            {
                IWebElement nextArrow = driver.FindElement(By.XPath(nextPage));
                next = nextArrow.GetAttribute("href");
            }
            catch { next = ""; }
            return next;
        }
        public bool checkNextPage(string page)
        {
            if (page.Length > 0) { return true; }
            else { return false; }
        }
        public string getStockDetail(string detailPath, int i)
        {
            string path = pathList + $"[{i}]" + detailPath;
            IWebElement element = driver.FindElement(By.XPath(path));
            return element.Text;
        }
        public string getStockNumber(WebDriver driver, int i)
        {
            string value = getStockDetail(stockNumber_path, i);
            return value;
        }
        public string getStockName(WebDriver driver, int i)
        {
            return getStockDetail(stockName_path, i);
        }
        public string getStockMarket(WebDriver driver, int i)
        {
            return getStockDetail(stockMarket_path, i);
        }
        public string getStockIndustry(WebDriver driver, int i)
        {
            return getStockDetail(stockIndustry_path, i);

        }
        public decimal getStockValue(WebDriver driver, int i)
        {
            decimal value = decimal.Parse(getStockDetail(stockValue_path, i));
            return value;
        }
        public decimal getStockDayBeforeDiff(WebDriver driver, int i)
        {
            decimal value = assessBeforeDay(getStockDetail(stockDayBeforeDiff_path, i));
            return value;
        }
        public decimal getStockDayBeforeRatio(WebDriver driver, int i)
        {
            decimal value = assessBeforeDay(getStockDetail(stockDayBeforeRatio_path, i));
            return value;
        }
        public decimal getStockPER(WebDriver driver, int i)
        {
            decimal value = decimal.Parse(getStockDetail(stockPER_path, i));
            return value;
        }
        public decimal getStockPBR(WebDriver driver, int i)
        {
            decimal value = decimal.Parse(getStockDetail(stockPBR_path, i));
            return value;
        }
        public decimal getStockDevidedYield(WebDriver driver, int i)
        {
            decimal value = decimal.Parse(getStockDetail(stockDevidedYield_path, i));
            return value;
        }
        public decimal getStockROE(WebDriver driver, int i)
        {
            decimal value = decimal.Parse(getStockDetail(stockROE_path, i));
            return value;
        }
        public int getStockAggregateValue(WebDriver driver, int i)
        {
            decimal dvalue = decimal.Parse(getStockDetail(stockAggregateValue_path, i));
            int value = Decimal.ToInt32(dvalue);
            return value;
        }
        public decimal getStockMovingAverage(WebDriver driver, int i)
        {
            decimal value = assessMovingAverage(getStockDetail(stockMovingAverage_path, i));
            return value;
        }
        public decimal getStockEquityRatio(WebDriver driver, int i)
        {
            decimal value = decimal.Parse(getStockDetail(stockEquityRatio_path, i));
            return value;
        }
        public void scrapyAndInsertData()
        {
            int stockNumber = getStocksLength();
            getDate();
            for (int i = 2; i <= stockNumber; i++)
            {
                StockData stock = new StockData();
                stock.Datetime = dateList[0];
                stock.Number = getStockNumber(driver, i);
                stock.Name = getStockName(driver, i);
                stock.Market = getStockMarket(driver, i);
                stock.Industry = getStockIndustry(driver, i);
                try { stock.Value = getStockValue(driver, i); } catch { stock.Value = null; }
                try { stock.DayBeforeDiff = getStockDayBeforeDiff(driver, i); } catch { stock.DayBeforeDiff = null; }
                try { stock.DayBeforeRatio = getStockDayBeforeRatio(driver, i); } catch { stock.DayBeforeRatio = null; }
                try { stock.PER = getStockPER(driver, i); } catch { stock.PER = null; }
                try { stock.PBR = getStockPBR(driver, i); } catch { stock.PBR = null; }
                try { stock.DevidedYield = getStockDevidedYield(driver, i); } catch { stock.DevidedYield = null; }
                try { stock.ROE = getStockROE(driver, i); } catch { stock.ROE = null; }
                stock.AggregateValue = getStockAggregateValue(driver, i);
                //try
                //{
                //    stock.AggregateValue = getStockAggregateValue(driver, i);
                //}
                //catch { stock.AggregateValue = null; }
                try
                {
                    stock.MovingAverage = getStockMovingAverage(driver, i);
                }
                catch { stock.MovingAverage = null; }
                try
                {
                    stock.EquityRatio = getStockEquityRatio(driver, i);
                }
                catch { stock.EquityRatio = null; }
                insert_JapanStocks_postAPI(stock);
            }
        }
        internal void insert_JapanStocks_postAPI(StockData xxx)
        {
            HttpClient client = new HttpClient();
            string jsonText = JsonConvert.SerializeObject(xxx);
            var requestContent = new System.Net.Http.StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(apiURL("insertStockData"), requestContent).Result; //雖然沒有response，但需要post出去
        }
    }
}
