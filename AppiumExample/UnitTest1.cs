using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using AppiumExample.Tools;


namespace AppiumExample
{
    [TestFixture]
    public class UnitTest1
    {
        AppiumDriver<AndroidElement> driver;
        DesiredCapabilities capabilities = new DesiredCapabilities();
        public void TestSetup()
        {

            capabilities.SetCapability(MobileCapabilityType.App, @"C:\\Users\\akimatc\\AndroidStudioProjects\\apiDemos-debug.apk");
            capabilities.SetCapability(MobileCapabilityType.DeviceName, "emulator-5554");
            capabilities.SetCapability(MobileCapabilityType.Udid, "emulator-5554");
            capabilities.SetCapability(MobileCapabilityType.PlatformVersion, "6.0.0");
            capabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.SetCapability(MobileCapabilityType.FullReset, "false");

            driver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
        }

        [Test]
        public void TestMethod1()
        {

            TestSetup();

            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Views']")).Click();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Chronometer']")).Click();

            //Verify "Start" buttton affecting chronometer
            //Read chronometer before start
            string start = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;
            driver.FindElement(By.Id("io.appium.android.apis:id/start")).Click();
            Thread.Sleep(2000);
            string finish = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;

            //Verify whether chronometer was working
            Assert.AreNotEqual(start, finish);

            //Exit app
            driver.CloseApp();
        }

        [Test]
        public void StopButtonTest()
        {
            TestSetup();

            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Views']")).Click();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Chronometer']")).Click();
            
            //Read chronometer before start and click Start
            string start = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;
            driver.FindElement(By.Id("io.appium.android.apis:id/start")).Click();

            //2 sec wait
            Thread.Sleep(2000);

            //Click stop and read chronometer
            driver.FindElement(By.Id("io.appium.android.apis:id/stop")).Click();
            string finish = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;

            //Verify whether chronometer was working
            Assert.AreNotEqual(start, finish);

            //Exit app
            driver.CloseApp();
        }

        [Test]
        public void ResetButtonTest()
        {
            TestSetup();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Views']")).Click();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Chronometer']")).Click();
            //Read chronometer before start and click Start
            string start = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;
            driver.FindElement(By.Id("io.appium.android.apis:id/start")).Click();

            //2 sec wait
            Thread.Sleep(2000);

            //Click stop and reset
            driver.FindElement(By.Id("io.appium.android.apis:id/stop")).Click();
            driver.FindElement(By.Id("io.appium.android.apis:id/reset")).Click();

            //Get chronometer reading after stop and reset
            string afterReset = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;

            //Verify whether chronometer has the same redings as before start
            Assert.AreEqual(start, afterReset);

            //Exit app
            driver.CloseApp();
        }
        [Test]
        public void SetFormatButtonTest()
        {
            TestSetup();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Views']")).Click();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Chronometer']")).Click();
            ////Read chronometer before start 
            //string start = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;

            //Click Set Format String button
            driver.FindElement(By.Id("io.appium.android.apis:id/set_format")).Click();

            //Click Start
            driver.FindElement(By.Id("io.appium.android.apis:id/start")).Click();

            //Read chronometer
            string afterSetFormat = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;

            //Check whether chronometer contains text SetFormat String
            Assert.IsTrue(afterSetFormat.Contains("Formatted time"));

            //Exit app
            driver.CloseApp();

        }

        [Test]
        public void ClearFormatButtonTest()
        {
            TestSetup();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Views']")).Click();
            driver.FindElement(By.XPath("//android.widget.TextView[@content-desc='Chronometer']")).Click();
            //Click Set Format String button
            driver.FindElement(By.Id("io.appium.android.apis:id/set_format")).Click();

            //Click Start
            driver.FindElement(By.Id("io.appium.android.apis:id/start")).Click();

            //Click Clear Format String Button
            driver.FindElement(By.Id("io.appium.android.apis:id/clear_format")).Click();

            //Read chronometer
            string afterClearFormat = driver.FindElement(By.Id("io.appium.android.apis:id/chronometer")).Text;

            //Count whether chronometer contains only 4 symbols 
            Assert.IsTrue(afterClearFormat.Length == 5);

            //Exit app
            driver.CloseApp();

        }

    }

}

