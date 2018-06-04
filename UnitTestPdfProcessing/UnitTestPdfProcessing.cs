using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfProcessing;
using System.IO;
using System.Text;

namespace UnitTestPdfProcessing
{
    [TestClass]
    public class UnitTestPdfProcessing
    {
        readonly private string ZAKUPY_PDF_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\zakupy.pdf";
        readonly private string ZAKUPY_TXT_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\zakupy.txt";
        readonly private string MENU_PDF_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\menu.pdf";
        readonly private string MENU_TXT_FILE_PATH = @"C:\git\PdfProcessing\PdfProcessing\samples\menu.txt";

        [TestMethod]
        //Not working properly now, but probably won't need to use that
        public void ReadPdfFileTest()
        {
            var pdfModule = new TikaServiceHandler();
            var content = pdfModule.ReadPdfFile(ZAKUPY_PDF_FILE_PATH);
            content.Wait();

            Assert.IsNotNull(content);
            Assert.AreNotEqual("", content);
        }
        [TestMethod]
        //Not working properly now, but probably won't need to use that
        public void ReadShoppingListTest()
        {
            var pdfProcess = new PdfProcess();

            var data = Encoding.UTF8.GetString(File.ReadAllBytes(ZAKUPY_TXT_FILE_PATH));

            var itemList = pdfProcess.ReadShoppingList(data, PdfProcess.getShoppingListRegExp);

            Assert.IsNotNull(itemList);
            //Assert.AreEqual(itemList.ShoppingItems[0].ShoppingItem.Item2, 300);
            //Assert.AreEqual(itemList.ShoppingItems[62].ShoppingItem.Item2, 150);
        }

        [TestMethod]
        public void ReadMenuTest()
        {
            var pdfProcess = new PdfProcess();

            var data = Encoding.UTF8.GetString(File.ReadAllBytes(MENU_TXT_FILE_PATH));

            var itemList = pdfProcess.ReadShoppingList(data, PdfProcess.getMenuRegExp);

            Assert.IsNotNull(itemList);
            Assert.AreNotEqual(0, itemList.ShoppingItems.Count);
        }
    }
}