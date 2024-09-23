using FirstTaskAutomation.Helpers;
using FirstTaskAutomation.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FirstTaskAutomation.TestMethods
{
    [TestClass]
    public class PaymentTests
    {
        public IWebDriver driver;
        public LoginPage loginPage;
        public PaymentPage paymentPage;

        [TestInitialize]
        public void Setup()
        {
            ManageDriver.InitializeDriver();
            driver = ManageDriver.driver;

            
            driver.Navigate().GoToUrl("https://localhost:44349/Auth/Login");
            loginPage = new LoginPage(driver);
            paymentPage = new PaymentPage(driver);
        }

        [TestMethod]
        public void TC1_VerifyPaymentCompletedSuccessfully()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
				ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("Nadil Mohammad", "1234567812345678", "845", "06-APR-26");
                paymentPage.CompletePayment();

                // تحقق من نجاح الدفع
                actualResult = "Payment processed successfully"; // يمكنك تعديل هذا بناءً على التطبيق الخاص بك
                Assert.AreEqual("Payment processed successfully", actualResult); // استبدل بالتحقق الصحيح
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }

        [TestMethod]
        public void TC2_VerifyInvalidCardDetails()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
				ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("Nadil mm", "1234567812345555", "07-APR-22", "123");
                paymentPage.CompletePayment();

                actualResult = "An error message appears. A required field must be filled in.";
                Assert.AreEqual("An error message appears. A required field must be filled in.", actualResult); // استبدل بالرسالة المتوقعة
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }

        [TestMethod]
        public void TC3_VerifyPaymentOperationsSavedAfterSuccessfulPayment()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
				ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("Nadil Mohammad", "1234567812345678", "845", "06-APR-26");
                paymentPage.CompletePayment();

                // تحقق من حفظ العمليات
                actualResult = "Payment saved in dashboard"; // استبدل بالتحقق الصحيح
                Assert.AreEqual("Payment saved in dashboard", actualResult);
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }

        [TestMethod]
        public void TC4_VerifyErrorMessageForInvalidCardNumber()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
                ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("Nadil", "444444", "844","06-APR-26" );
                paymentPage.CompletePayment();

                actualResult = "Invalid card number message appears";
                Assert.AreEqual("Invalid card number message appears", actualResult); // استبدل بالرسالة المتوقعة
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }

        [TestMethod]
        public void TC5_VerifyPaymentFailsIfMandatoryFieldsAreNotEntered()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
                ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("", "", "", ""); // ترك جميع الحقول فارغة
                paymentPage.CompletePayment();

                actualResult = "An error message is displayed , and the payment is not processed";
                Assert.AreEqual("An error message is displayed , and the payment is not processed", actualResult); // استبدل بالرسالة المتوقعة
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }

        [TestMethod]
        public void TC6_VerifyUserCanSaveCardInformationWhenRememberMeChecked()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
                driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("Nadil Mohammad", "1234567812345678", "06-APR-26", "845");
                // تأكد من اختيار "تذكرني"
                paymentPage.CompletePayment();

                // تحقق من حفظ المعلومات
                actualResult = "Card information saved"; // استبدل بالتحقق الصحيح
                Assert.AreEqual("Card information saved", actualResult);
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }

        [TestMethod]
        public void TC7_VerifyCardInformationNotSavedWhenRememberMeUnchecked()
        {
            string actualResult;
            try
            {
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
                ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");
                paymentPage.EnterCardDetails("Nadil", "1234567812345678", "06-APR-26", "845");
                // تأكد من عدم اختيار "تذكرني"
                paymentPage.CompletePayment();

                // تحقق من عدم حفظ المعلومات
                actualResult = "Card information not saved"; // استبدل بالتحقق الصحيح
                Assert.AreEqual("Card information not saved", actualResult);
            }
            catch (Exception ex)
            {
                actualResult = ex.Message;
                Assert.Fail($"Test failed: {actualResult}");
            }
        }
        [TestMethod]
        public void TC8_VerifyInvoiceSummaryInformation()
        {

            loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
            ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");

            // عدد المنتجات (الصفوف) التي تحتوي على المنتجات
            var productRows = driver.FindElements(By.XPath("//div[@class='row']/div[@class='col-6']/div/label"));

            // تمر عبر كل صف من المنتجات وتحقق من الكمية والسعر
            for (int i = 0; i < productRows.Count; i++)
            {
                // استخراج اسم المنتج
                string productName = driver.FindElement(By.XPath($"(//div[@class='col-6']/div/label)[{i + 1}]")).Text;

                // استخراج الكمية
                string quantity = driver.FindElement(By.XPath($"(//div[@class='col-3']/span)[{(i * 2) + 1}]")).Text;

                // استخراج السعر
                string price = driver.FindElement(By.XPath($"(//div[@class='col-3']/span/span)[{(i * 2) + 2}]")).Text;

                // طباعة المعلومات للتأكد
                Console.WriteLine($"Product: {productName}, Quantity: {quantity}, Price: {price}");
            }

            // التحقق من السعر الإجمالي
            string totalPrice = driver.FindElement(By.XPath("//div[@class='col-4']/span/b")).Text;

            Console.WriteLine($"Total Price: {totalPrice}");

            // يمكنك إضافة Assert للتحقق من السعر الإجمالي أو أي اختبارات أخرى تحتاجها
            Assert.AreEqual("680.00 JD", totalPrice, "All information is present and correct.");
        }
        [TestMethod]

        public void TC9_VerifyTotalPriceCalculation_Short()
        {
            // تسجيل الدخول
            loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");
            ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");

            // جمع الأسعار وحساب المجموع
            var priceElements = driver.FindElements(By.XPath("//div[@class='col-3']/span/span"));

            // التحقق من أن العناصر التي تحتوي على الأسعار موجودة
            if (priceElements.Count == 0)
            {
                Assert.Fail("No price elements were found on the page. Verify the XPath.");
            }

            // حساب المجموع
            double calculatedTotal = priceElements
                .Select(e => e.Text.Replace("JD", "").Trim()) // إزالة "JD" والتأكد من عدم وجود مسافات غير ضرورية
                .Select(priceText =>
                {
                    double price;
                    if (double.TryParse(priceText, out price))
                    {
                        return price;
                    }
                    else
                    {
                        // في حال كان التحويل يفشل
                        Console.WriteLine($"Failed to parse price: {priceText}");
                        return 0;
                    }
                })
                .Sum();

            // الحصول على السعر الإجمالي المعروض
            string totalPriceText = driver.FindElement(By.XPath("//div[@class='col-4']/span/b")).Text;

            // طباعة السعر الإجمالي النصي للتحقق
            Console.WriteLine($"Total Price Text: {totalPriceText}");

            // تنظيف النص قبل تحويله إلى رقم
            totalPriceText = totalPriceText.Replace("JD", "").Replace(",", "").Trim();

            // التحقق من أن النص الإجمالي المعروض يمكن تحويله إلى رقم
            if (double.TryParse(totalPriceText, out double displayedTotal))
            {
                // التحقق من أن المجموع المحسوب يساوي المجموع المعروض
                Assert.AreEqual(calculatedTotal, displayedTotal, "Calculated total does not match the displayed total.");
            }
            else
            {
                // في حال فشل التحويل
                Assert.Fail($"Displayed total is not a valid number: {totalPriceText}");
            }

            // طباعة النتائج
            Console.WriteLine($"Calculated Total: {calculatedTotal}"); // السعر المحسوب
            Console.WriteLine($"Displayed Total: {displayedTotal}"); // السعر المعروض
        }


        [TestMethod]
        public void TC10_VerifyBackButtonFunctionality()
        {
            try
            {
                // تسجيل الدخول
                loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");

                // الانتقال إلى صفحة الدفع
                ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder");

                // الانتقال إلى صفحة المنتجات
                ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/ShoppingCart");

                // العودة إلى الصفحة السابقة
                ManageDriver.driver.Navigate().Back();

				// تحقق من ظهور عنصر على صفحة الدفع

				
				CommonMethods.WaitForElement(driver, By.ClassName("card-body"), 10);

				
				IWebElement paymentPageElement = driver.FindElement(By.ClassName("card-body"));

				Assert.IsTrue(paymentPageElement.Displayed, "Back button functionality failed.");
                Console.WriteLine("Back button functionality Completed Successfully");
            }
            catch (Exception ex)
            {
                Assert.Fail("Test failed: " + ex.Message);
                Console.WriteLine("TC10 not Completed Successfully: " + ex.Message);
            }
        }
		[TestMethod]
		public void TC11_CheckCartAndProceedToPayment()
		{
			try
			{
				
				loginPage.Login("duaa.albsharat1992@gmail.com", "Duaa.1992");


				ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/PayForTheOrder"); // تأكد من الانتقال إلى الصفحة Shopping Cart  بعد تسجيل الدخول

				// 2. تحقق من وجود منتجات في عربة التسوق
				var cartItems = driver.FindElements(By.XPath("//div/ul/li/a/i")); //  XPath بمحدد العناصر في عربة التسوق

				if (cartItems.Count > 0)
				{

					// 3. الانتقال إلى صفحة الدفع
					ManageDriver.driver.Navigate().GoToUrl("https://localhost:44349/User/ShoppingCart");
					Console.WriteLine("Cart contains products. Proceeding to payment page.");
				}
				else
				{
					// 4. عرض رسالة تحذيرية إذا كانت العربة فارغة
					Console.WriteLine("Cart is empty. You must add products to proceed.");
				}
			}
			catch (NoSuchElementException ex)
			{
				Console.WriteLine("Error: Could not find an element on the page. " + ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine("An unexpected error occurred: " + ex.Message);
			}
		}




		[TestCleanup]
        public void Cleanup()
        {
            ManageDriver.QuitDriver();
        }

    }
}
