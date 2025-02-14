using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace TestProject3
{
    [TestFixture]
    public class WorkingWithDropDown
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("headless", "no-sandbox", "disable-dev-shm-usage", "disable-gpu",
                                 "window-size=1920x1080", "disable-extensions", "remote-debugging-port-9222");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void TestSelectFromDropDown()
        {
            driver.Url = "http://practice.bpbonline.com/";

            string path = Directory.GetCurrentDirectory() + "/manufacturer.txt";
            if (File.Exists(path)) File.Delete(path);

            SelectElement manufDropdown = new SelectElement(driver.FindElement(By.Name("manufacturers_id")));
            IList<IWebElement> allManufacturers = manufDropdown.Options;
            List<string> manufNames = allManufacturers.Select(m => m.Text).ToList();
            manufNames.RemoveAt(0);

            foreach (string mname in manufNames)
            {
                manufDropdown.SelectByText(mname);
                manufDropdown = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturers_id']")));

                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer {mname} has no products\n");
                }
                else
                {
                    IWebElement productTable = driver.FindElement(By.ClassName("productListingData"));
                    File.AppendAllText(path, $"\n\nThe manufacturer {mname} products are listed--\n");

                    ReadOnlyCollection<IWebElement> rows = productTable.FindElements(By.XPath("//tbody/tr"));
                    foreach (IWebElement row in rows) File.AppendAllText(path, row.Text + "\n");
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
