using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTaskAutomation.Helpers
{
    public static class CommonMethods
    {
        public static void WaitForElement(IWebDriver driver, By locator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

		public static void NavigateToURLLogin(string url)
		{
			ManageDriver.driver.Navigate().GoToUrl(url);
		}
		public static void NavigateToURLLoginPayment(string url)
		{
			ManageDriver.driver.Navigate().GoToUrl(url);
		}

	}
}
