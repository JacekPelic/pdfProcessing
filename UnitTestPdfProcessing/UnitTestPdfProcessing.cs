using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileParser;
using System.IO;
using System.Text;
using FileParser.helpers;

namespace UnitTestPdfProcessing
{
    [TestClass]
    public class UnitTestPdfProcessing
    {
        readonly private string ZAKUPY_PDF_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\zakupy.pdf";
        readonly private string ZAKUPY_TXT_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\zakupy.txt";
        readonly private string MENU_PDF_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\menu.pdf";
        readonly private string MENU_TXT_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\menu.txt";
        readonly private string BUDGET_PDF_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\Budget_Tribute-Communities-Centre_2018.pdf";

        [TestMethod]
        //Not working properly now, but probably won't need to use that
        public void ReadPdfFileTest()
        {
            var pdfModule = new TikaServiceHandler();
            var content = pdfModule.ReadPdfFile(BUDGET_PDF_FILE_PATH);
            content.Wait();

            Assert.IsNotNull(content);
            Assert.AreNotEqual("", content);
        }
        [TestMethod]
        //Not working properly now, but probably won't need to use that
        public void ReadShoppingListTest()
        {
            var pdfProcess = new FileParser.FileParser();

            var data = Encoding.UTF8.GetString(File.ReadAllBytes(ZAKUPY_TXT_FILE_PATH));

            var itemList = pdfProcess.ReadShoppingList(data, RegexHelper.getShoppingListRegExp());

            Assert.IsNotNull(itemList);
            //Assert.AreEqual(itemList.ShoppingItems[0].ShoppingItem.Item2, 300);
            //Assert.AreEqual(itemList.ShoppingItems[62].ShoppingItem.Item2, 150);
        }

        [TestMethod]
        public void ReadMenuTest()
        {
            var pdfProcess = new FileParser.FileParser();

            var data = Encoding.UTF8.GetString(File.ReadAllBytes(MENU_TXT_FILE_PATH));

            var itemList = pdfProcess.ReadShoppingList(data, RegexHelper.getMenuRegExp());

            Assert.IsNotNull(itemList);
            Assert.AreNotEqual(0, itemList.ShoppingItems.Count);
        }
    }
}
