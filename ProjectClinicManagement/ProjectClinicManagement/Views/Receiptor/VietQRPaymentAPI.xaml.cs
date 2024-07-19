using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using ProjectClinicManagement.API;
using RestSharp;
using System.Drawing;
using QRCoder;

namespace ProjectClinicManagement.Views.Receiptor
{
    /// <summary>
    /// Interaction logic for VietQRPaymentAPI.xaml
    /// </summary>
    public partial class VietQRPaymentAPI : Window
    {
        public VietQRPaymentAPI()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            var apiRequest = new ApiRequest
            {
                acqId = Convert.ToInt32(cbBank.SelectedValue.ToString()),
                accountNo = long.Parse(tbTaiKhoan.Text),
                accountName = tbTen.Text,
                amount = Convert.ToInt32(tbMonney.Text),
                format = "text",
                template = cbTemplate.Text,
                addInfo = "khiem deptrai"
            };

            var jsonRequest = JsonConvert.SerializeObject(apiRequest);
            var client = new RestClient("https://api.vietqr.io/v2/generate");
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            var response = client.Execute(request);
            var content = response.Content;

            var dataResult = JsonConvert.DeserializeObject<ApiResponse>(content);

            if (dataResult != null && dataResult.data != null && !string.IsNullOrEmpty(dataResult.data.qrCode))
            {
                var image = GenerateQRCodeImage(dataResult.data.qrCode);
                qrGrid.Source = image;
            }
            else
            {
                MessageBox.Show("No QR code data received from API.");
            }
        }

        private BitmapImage GenerateQRCodeImage(string qrCodeData)
        {
            try
            {
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeDataObject = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCode(qrCodeDataObject))
                    {
                        using (var qrCodeImage = qrCode.GetGraphic(20))
                        {
                            using (var ms = new MemoryStream())
                            {
                                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                ms.Position = 0;

                                var bitmapImage = new BitmapImage();
                                bitmapImage.BeginInit();
                                bitmapImage.StreamSource = ms;
                                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                bitmapImage.EndInit();

                                return bitmapImage;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code image: {ex.Message}");
                return null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var client = new WebClient())
            {
                var htmlData = client.DownloadData("https://api.vietqr.io/v2/banks");
                var bankRawJson = Encoding.UTF8.GetString(htmlData);
                var listBankData = JsonConvert.DeserializeObject<Bank>(bankRawJson);

                cbBank.ItemsSource = listBankData.data;
                cbBank.DisplayMemberPath = "custom_name";
                cbBank.SelectedValuePath = "bin";
                cbBank.SelectedValue = listBankData.data.FirstOrDefault()?.bin;
                cbBank.SelectedIndex = 0;
            }
        }
    }
}
