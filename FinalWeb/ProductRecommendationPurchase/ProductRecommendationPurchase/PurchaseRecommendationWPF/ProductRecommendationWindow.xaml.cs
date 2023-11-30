using Microsoft.ML;
using Microsoft.ML.Trainers;
using Microsoft.Win32;
using SalesDataAccess;
using SalesDataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Microsoft.ML.DataOperationsCatalog;
namespace PurchaseRecommendationWPF
{
    /// <summary>
    /// Interaction logic for ProductRecommendationWindow.xaml
    /// </summary>
    public partial class ProductRecommendationWindow : Window
    {
        public ProductRecommendationWindow()
        {
            InitializeComponent();
        }
        private MLContext mlContext;
        private ITransformer trainedModel;

        private void btnPickModel_Click(object sender, RoutedEventArgs e)
        {
            //Step 8.2: Load model
            DataViewSchema modelSchema;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "File .zip|*.zip";
            if (openFileDialog.ShowDialog() == true)
            {
                if (mlContext == null)
                    mlContext = new MLContext();
                //Load trained model
                trainedModel = mlContext.Model.Load(
                    openFileDialog.FileName, out modelSchema);
                MessageBox.Show("Load trained model successfully",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSelectedModel.Text = openFileDialog.FileName;

                ProcessListPrediction();
            }
        }

        private void ProcessListPrediction()
        {
            var predictionengine = mlContext.Model.CreatePredictionEngine<DataEntry, DataPrediction>(trainedModel);
            List<Customer> customers = new CustomerConnector().GetAllCustomers();
            List<Product> products = new ProductConnector().GetAllProducts();
            LvProductRecommendation.Items.Clear();
            foreach (Customer customer in customers)
            {
                foreach (Product product in products)
                {
                    var ret = predictionengine.Predict(new DataEntry()
                    {
                        CustomerID = customer.CustomerId,
                        ProductID = product.ProductID
                    });
                    ResultInfor infor = new ResultInfor();
                    infor.ContactName = customer.ContactName;
                    infor.ProductID = product.ProductID;
                    infor.CustomerID = customer.CustomerId;
                    infor.Score = Math.Min(1, Math.Round(ret.Score, 1));
                    infor.ProductName = product.ProductName;
                    if (infor.Score >= 0.5)
                    {
                        infor.Decision = "recommended";
                    }
                    LvProductRecommendation.Items.Add(infor);
                }
            }
        }

        private void LvProductRecommendation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
