using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using FinalScraper;

namespace FinalScraper.Controllers
{
    public class StockController : Controller
    {
        private TheScraperEntities db = new TheScraperEntities();

        // GET: Stock
        public ActionResult Index(string sortOrder)
        {
            ViewBag.DateSortParm = sortOrder == "Date";
            var stocks = from s in db.Stocks
                           select s;
                    stocks = stocks.OrderBy(s => s.DateAndTime);
            
            return View(stocks.ToList());
        }

        // GET: Stock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Symbol1,Symbol2,Symbol3,Symbol4,Symbol5,Symbol6,Symbol7,Symbol8,Symbol9,Symbol10,CurrentPrice1,CurrentPrice2,CurrentPrice3,CurrentPrice4,CurrentPrice5,CurrentPrice6,CurrentPrice7,CurrentPrice8,CurrentPrice9,CurrentPrice10,ChangePercent1,ChangePercent2,ChangePercent3,ChangePercent4,ChangePercent5,ChangePercent6,ChangePercent7,ChangePercent8,ChangePercent9,ChangePercent10,MarketCap1,MarketCap2,MarketCap3,MarketCap4,MarketCap5,MarketCap6,MarketCap7,MarketCap8,MarketCap9,MarketCap10,DateAndTime")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Stocks.Add(stock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stock);
        }

        Random rnd = new Random();

        public ActionResult Scrape(Stock stock)
        {

            var chromeDriver = new ChromeDriver();

            chromeDriver.Navigate().GoToUrl("https://login.yahoo.com/?.src=finance&.intl=us&.done=https%3A%2F%2Ffinance.yahoo.com%2Fportfolios%253Fbypass%3Dtrue%3Fbypass%3Dtrue&add=1");
            chromeDriver.FindElementByXPath("//*[@id=\"login-username\"]").Click();
            chromeDriver.Keyboard.SendKeys("kyle7150@yahoo.com");
            chromeDriver.FindElementByXPath("//*[@id=\"login-signin\"]").Click();
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            chromeDriver.FindElementByXPath("//*[@id=\"login-passwd\"]").Click();
            chromeDriver.Keyboard.SendKeys("Doubleu1");
            chromeDriver.Keyboard.SendKeys(Keys.Enter);
            chromeDriver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");

            var Stock1Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[1]/td[2]/span").GetAttribute("textContent");
            var Symbol1 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[1]/td[1]/span/a").GetAttribute("textContent");
            var Stock1mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[1]/td[13]/span").GetAttribute("textContent");
            var Stock1cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[1]/td[4]/span").GetAttribute("textContent");
            var Stock2Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[2]/td[2]").GetAttribute("textContent");
            var Symbol2 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[2]/td[1]/span/a").GetAttribute("textContent");
            var Stock2mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[2]/td[13]/span").GetAttribute("textContent");
            var Stock2cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[2]/td[4]/span").GetAttribute("textContent");
            var Stock3Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[3]/td[2]/span").GetAttribute("textContent");
            var Symbol3 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[3]/td[1]/span/a").GetAttribute("textContent");
            var Stock3mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[3]/td[13]/span").GetAttribute("textContent");
            var Stock3cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[3]/td[4]/span").GetAttribute("textContent");
            var Stock4Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[4]/td[2]/span").GetAttribute("textContent");
            var Symbol4 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[4]/td[1]/span/a").GetAttribute("textContent");
            var Stock4mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[4]/td[13]/span").GetAttribute("textContent");
            var Stock4cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[4]/td[4]/span").GetAttribute("textContent");
            var Stock5Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[5]/td[2]/span").GetAttribute("textContent");
            var Symbol5 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[5]/td[1]/span/a").GetAttribute("textContent");
            var Stock5mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[5]/td[13]/span").GetAttribute("textContent");
            var Stock5cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[5]/td[4]/span").GetAttribute("textContent");
            var Stock6Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[6]/td[2]/span").GetAttribute("textContent");
            var Symbol6 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[6]/td[1]/span/a").GetAttribute("textContent");
            var Stock6mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[6]/td[13]/span").GetAttribute("textContent");
            var Stock6cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[6]/td[4]/span").GetAttribute("textContent");
            var Stock7Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[7]/td[2]/span").GetAttribute("textContent");
            var Symbol7 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[7]/td[1]/span/a").GetAttribute("textContent");
            var Stock7mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[7]/td[13]/span").GetAttribute("textContent");
            var Stock7cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[7]/td[4]/span").GetAttribute("textContent");
            var Stock8Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[8]/td[2]/span").GetAttribute("textContent");
            var Symbol8 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[8]/td[1]/span/a").GetAttribute("textContent");
            var Stock8mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[8]/td[13]/span").GetAttribute("textContent");
            var Stock8cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[8]/td[4]/span").GetAttribute("textContent");
            var Stock9Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[9]/td[2]/span").GetAttribute("textContent");
            var Symbol9 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[9]/td[1]/span/a").GetAttribute("textContent");
            var Stock9mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[9]/td[13]/span").GetAttribute("textContent");
            var Stock9cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[9]/td[4]/span").GetAttribute("textContent");
            var Stock10Price = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[10]/td[2]/span").GetAttribute("textContent");
            var Symbol10 = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[10]/td[1]/span/a").GetAttribute("textContent");
            var Stock10mc = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[10]/td[13]/span").GetAttribute("textContent");
            var Stock10cp = chromeDriver.FindElementByXPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[10]/td[4]/span").GetAttribute("textContent");
            var random = rnd.Next(52000);

            var content = new TheScraperEntities();
            var post = new Stock()
            {
                CurrentPrice1 = Stock1Price,
                CurrentPrice2 = Stock2Price,
                CurrentPrice3 = Stock3Price,
                CurrentPrice4 = Stock4Price,
                CurrentPrice5 = Stock5Price,
                CurrentPrice6 = Stock6Price,
                CurrentPrice7 = Stock7Price,
                CurrentPrice8 = Stock8Price,
                CurrentPrice9 = Stock9Price,
                CurrentPrice10 = Stock10Price,
                Symbol1 = Symbol1,
                Symbol2 = Symbol2,
                Symbol3 = Symbol3,
                Symbol4 = Symbol4,
                Symbol5 = Symbol5,
                Symbol6 = Symbol6,
                Symbol7 = Symbol7,
                Symbol8 = Symbol8,
                Symbol9 = Symbol9,
                Symbol10 = Symbol10,
                MarketCap1 = Stock1mc,
                MarketCap2 = Stock2mc,
                MarketCap3 = Stock3mc,
                MarketCap4 = Stock4mc,
                MarketCap5 = Stock5mc,
                MarketCap6 = Stock6mc,
                MarketCap7 = Stock7mc,
                MarketCap8 = Stock8mc,
                MarketCap9 = Stock9mc,
                MarketCap10 = Stock10mc,
                ChangePercent1 = Stock1cp,
                ChangePercent2 = Stock2cp,
                ChangePercent3 = Stock3cp,
                ChangePercent4 = Stock4cp,
                ChangePercent5 = Stock5cp,
                ChangePercent6 = Stock6cp,
                ChangePercent7 = Stock7cp,
                ChangePercent8 = Stock8cp,
                ChangePercent9 = Stock9cp,
                ChangePercent10 = Stock10cp,
                DateAndTime = DateTime.Now,
                ID = random
            };
            content.Stocks.Add(post);
            content.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Symbol1,Symbol2,Symbol3,Symbol4,Symbol5,Symbol6,Symbol7,Symbol8,Symbol9,Symbol10,CurrentPrice1,CurrentPrice2,CurrentPrice3,CurrentPrice4,CurrentPrice5,CurrentPrice6,CurrentPrice7,CurrentPrice8,CurrentPrice9,CurrentPrice10,ChangePercent1,ChangePercent2,ChangePercent3,ChangePercent4,ChangePercent5,ChangePercent6,ChangePercent7,ChangePercent8,ChangePercent9,ChangePercent10,MarketCap1,MarketCap2,MarketCap3,MarketCap4,MarketCap5,MarketCap6,MarketCap7,MarketCap8,MarketCap9,MarketCap10,DateAndTime")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stock);
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stocks.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stocks.Find(id);
            db.Stocks.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
