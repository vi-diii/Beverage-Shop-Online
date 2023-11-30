using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.Recommender;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.ML.DataOperationsCatalog;
using SalesDataAccess;
using SalesDataModel;
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace PurchaseRecommendationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtLossFunction.Clear();
            txtMAE.Clear();
            txtMSE.Clear();
            txtRMSE.Clear();
            txtRSquared.Clear();
            txtCustomerId.Clear();
            txtProductId.Clear();
        }
        private MLContext mlContext;
        private TrainTestData trainTestData;
        private MatrixFactorizationTrainer.Options options;
        private MatrixFactorizationTrainer trainer;
        private ITransformer trainedModel;

        private void btnStep1_Click(object sender, RoutedEventArgs e)
        {
            //STEP 1: CREATE MLCONTEXT TO BE SHARED ACROSS THE MODEL
            //create workflow object
            mlContext = new MLContext();
            lblStep1Status.Content = "MLContext is created successfully";
        }

        private void btnStep2_Click(object sender, RoutedEventArgs e)
        {
            //Step 2: Read the trained data
            List<DataEntry> dataEntryList = new DataEntryConnector().GetAllDataEntries();
            IDataView dataView = mlContext.Data.LoadFromEnumerable(dataEntryList);

            double trainRate = double.Parse(txtTrainRate.Text) / 100;
            double testRate = 1 - trainRate;

            trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction: testRate);
            lblStep2Status.Content = "Creted Train test data successfully";
        }

        private void btnStep3_Click(object sender, RoutedEventArgs e)
        {
            //STEP 3: Data is already encoded so all u need to is specify options for
            //MatrixFactorizatiotrainer

            options = new MatrixFactorizationTrainer.Options();
            options.MatrixColumnIndexColumnName = nameof(DataEntry.CustomerID);
            options.MatrixRowIndexColumnName = nameof(DataEntry.ProductID);
            options.LabelColumnName = "Label";
            options.LossFunction = MatrixFactorizationTrainer.LossFunctionType.SquareLossOneClass;
            options.Alpha = 0.01;
            options.Lambda = 0.025;
            options.C = 0.00001;
            lblStep3Status.Content = "Configuration Matrix Factorization Trainer successfully";
        }

        private void btnStep4_Click(object sender, RoutedEventArgs e)
        {
            trainer = mlContext.Recommendation().Trainers.MatrixFactorization(options);
            lblStep4Status.Content = "Created Matrix Factorization Trainer successfully";
        }

        private void btnStep5_Click(object sender, RoutedEventArgs e)
        {
            trainedModel = trainer.Fit(trainTestData.TrainSet);
            lblStep5Status.Content = "Trained model successfully";
        }

        private void btnStep6_Click(object sender, RoutedEventArgs e)
        {
            //Step 6: evaluate model
            IDataView prediction = trainedModel.Transform(trainTestData.TestSet);
            RegressionMetrics metrics = mlContext.Regression.Evaluate(prediction
                , labelColumnName: "Label", scoreColumnName: "Score");
            txtLossFunction.Text = metrics.LossFunction.ToString();
            txtMAE.Text = metrics.MeanAbsoluteError.ToString();
            txtMSE.Text = metrics.MeanSquaredError.ToString();
            txtRMSE.Text = metrics.RootMeanSquaredError.ToString();
        }

        private void btnStep7_Click(object sender, RoutedEventArgs e)
        {
            //STEP 7: Test a singer prediction
            var predictionengine = mlContext.Model.CreatePredictionEngine
                <DataEntry, DataPrediction>(trainedModel);
            uint customerId = uint.Parse(txtCustomerId.Text);
            uint productId = uint.Parse(txtProductId.Text);
            var prediction = predictionengine.Predict(
                new DataEntry()
                {
                    CustomerID = customerId,
                    ProductID = productId,
                });
            FlowDocument mcFlowDoc = new FlowDocument();
            Paragraph para = new Paragraph();
            para.Inlines.Add(new Run("For CustomerId= "));
            para.Inlines.Add(new Bold(new Run(customerId + " ")));
            para.Inlines.Add(new Run(" and Product ID= "));
            para.Inlines.Add(new Bold(new Run(productId + " ")));
            para.Inlines.Add(new Run(" the prediction score = "));
            para.Inlines.Add(new Bold(new Run(Math.Round(prediction.Score, 1) + "")));
            mcFlowDoc.Blocks.Add(para);
            richTxtResult.Document = mcFlowDoc;
        }

        private void btnStep81_Click(object sender, RoutedEventArgs e)
        {
            //Step 8.1: Save Model
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "File .zip|*.zip";
            if (saveFileDialog.ShowDialog() == true)
            {
                mlContext.Model.Save(
                    trainedModel,
                    trainTestData.TrainSet.Schema,
                    saveFileDialog.FileName);
                MessageBox.Show("Save trained model successfully",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnStep82_Click(object sender, RoutedEventArgs e)
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
            }
        }

        private void btnPredictionForAll_Click(object sender, RoutedEventArgs e)
        {
           ProductRecommendationWindow prw = new ProductRecommendationWindow(); 
           prw.Show();
        }
    }
}
