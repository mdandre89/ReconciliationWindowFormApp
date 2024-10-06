using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text.Json;
using System.Xml.Linq;
using System.Text;

namespace testform
{
    class PurchaseTransaction
    {
        required public string Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }
    class PaymentTransaction
    {
        required public string Customer { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; } // or decimal?
    }
    public class PaymentUnmatched
    {
        required public string Customer { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Difference { get; set; }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void BtnPickFile1_Click(object sender, EventArgs e)
        {
            // Open file dialog for file 1
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.dat",  // Filter can be adjusted to restrict file types
                Title = "Select a dat file"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }
        private void BtnPickFile2_Click(object sender, EventArgs e)
        {
            // Open file dialog for file 2
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.json",  // Filter can be adjusted to restrict file types
                Title = "Select a JSON file"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog.FileName;
            }
        }
        private void BtnPickFile3_Click(object sender, EventArgs e)
        {
            // Open file dialog for file 3
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.xml",  // Filter can be adjusted to restrict file types
                Title = "Select a xml file"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog.FileName;
            }

        }
        private void BtnExecute_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length>0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0)
            {
                var purchases = ParsePurchases(textBox1.Text);
                var payments = ParsePayments(textBox2.Text);
                var prices = ParsePrices(textBox3.Text);

                var paymentsNotMatched = ReconcilePurchasesAndPayments(purchases, payments, prices);

                string fileName = "PaymentsNotMatched";
                string selectedFormat = this.formatComboBox.Text;

                SaveFile(paymentsNotMatched, fileName, selectedFormat);
                MessageBox.Show("Files have been logged to the console!");

                string fullPath = $"{fileName}.{selectedFormat}";

                if (File.Exists(fullPath))
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = fullPath; // Use the full path of the file
                    proc.StartInfo.UseShellExecute = true; // Open with the default associated app
                    proc.Start();
                }
                else
                {
                    MessageBox.Show("Failed to save the file.");
                }
            }
            else
            {
                MessageBox.Show("Select 3 files first!");
            }

        }

        static List<PurchaseTransaction> ParsePurchases(string purchasesFile)
        {
            List<PurchaseTransaction> purchases = new List<PurchaseTransaction>();
            PurchaseTransaction? currentTransaction = null;
            foreach (string line in File.ReadAllLines(purchasesFile))
            {
                if (!string.IsNullOrEmpty(line?.Trim()))
                {
                    if (line.StartsWith("CUST"))
                    {
                        currentTransaction = new PurchaseTransaction { Customer = line.Substring(4) };
                        purchases.Add(currentTransaction);
                    }
                    else if (line.StartsWith("DATE") && currentTransaction?.Customer.Length > 0)
                    {
                        var date = DateTime.ParseExact(line.Substring(4), "ddMMyyyyHHmm", CultureInfo.InvariantCulture);
                        currentTransaction.PurchaseDate = date;
                    }
                    else if (line.StartsWith("ITEM") && currentTransaction?.Customer.Length > 0)
                    {
                        var item = line.Substring(4);
                        currentTransaction.Items.Add(item);
                    }
                }

            }
            return purchases;
        }
        static List<PaymentTransaction> ParsePayments(string paymentsFile)
        {
            string paymentJson = File.ReadAllText(paymentsFile);
            return JsonSerializer.Deserialize<List<PaymentTransaction>>(paymentJson)!;
        }
        static Dictionary<string, decimal> ParsePrices(string pricesFile)
        {
            XDocument xmlDoc = XDocument.Load(pricesFile);
            return xmlDoc.Descendants("ItemPrice")
                .ToDictionary(
                    item => item.Element("Item")?.Value ?? string.Empty,
                    item => decimal.Parse(item.Element("Price")?.Value ?? "0")
                    );
        }

        static void SaveFile(List<PaymentUnmatched> paymentsNotMatched, string fileName, string format)
        {
            string fileNameWithFormat  = $"{fileName}.{format}";

            switch (format.ToLower())
            {
                case "json":
                    SaveAsJson(paymentsNotMatched, fileNameWithFormat);
                    break;
                case "csv":
                    SaveAsCsv(paymentsNotMatched, fileNameWithFormat);
                    break;
                case "text":
                    SaveAsText(paymentsNotMatched, fileNameWithFormat);
                    break;
                default:
                    throw new NotSupportedException($"Format '{format}' is not supported.");
            }
        }
        private static void SaveAsJson(List<PaymentUnmatched> paymentsNotMatched, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(paymentsNotMatched, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
        }
        private static void SaveAsCsv(List<PaymentUnmatched> paymentsNotMatched, string filePath)
        {
            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("Customer,Year,Month,Amount,PaidAmount,Difference");
            foreach (var payment in paymentsNotMatched)
            {
                csvContent.AppendLine($"{payment.Customer},{payment.Year},{payment.Month},{payment.Amount},{payment.PaidAmount},{payment.Difference}");
            }
            File.WriteAllText(filePath, csvContent.ToString());
        }
        private static void SaveAsText(List<PaymentUnmatched> paymentsNotMatched, string filePath)
        {
            StringBuilder textContent = new StringBuilder();
            foreach (var payment in paymentsNotMatched)
            {
                textContent.AppendLine($"Customer: {payment.Customer}");
                textContent.AppendLine($"Year: {payment.Year}");
                textContent.AppendLine($"Month: {payment.Month}");
                textContent.AppendLine($"Amount: {payment.Amount}");
                textContent.AppendLine($"Paid Amount: {payment.PaidAmount}");
                textContent.AppendLine($"Difference: {payment.Difference}");
                textContent.AppendLine();
            }
            File.WriteAllText(filePath, textContent.ToString());
        }

        static List<PaymentUnmatched> ReconcilePurchasesAndPayments(List<PurchaseTransaction> purchases, List<PaymentTransaction> payments, Dictionary<string, decimal> prices)
        {
            var paymentsUnmatched = new List<PaymentUnmatched>();
            var transactionsLedger = new Dictionary<string, decimal>();

            foreach (var purchaseTransaction in purchases)
            {
                string key = purchaseTransaction.Customer + '_' + purchaseTransaction.PurchaseDate.ToString("MMyyyy");

                decimal purchaseAmount = 0;
                foreach (string purchase in purchaseTransaction.Items)
                {
                    if (prices.TryGetValue(purchase, out decimal itemPrice))
                    {
                        purchaseAmount += itemPrice;
                    }
                    else
                    {
                        Console.WriteLine($"Warning: Item {purchase} not found in price list.");
                    }
                }

                transactionsLedger[key] = transactionsLedger.GetValueOrDefault(key, 0) + purchaseAmount;
            }

            foreach (var paymentTransaction in payments)
            {
                string key = paymentTransaction.Customer + '_' + paymentTransaction.Month.ToString("D2") + paymentTransaction.Year.ToString();

                decimal purchaseAmount = transactionsLedger.GetValueOrDefault(key, 0);
                decimal difference = purchaseAmount - paymentTransaction.Amount;

                if (difference != 0)
                {
                    paymentsUnmatched.Add(new PaymentUnmatched
                    {
                        Customer = paymentTransaction.Customer,
                        Year = paymentTransaction.Year,
                        Month = paymentTransaction.Month,
                        Amount = purchaseAmount,
                        PaidAmount = paymentTransaction.Amount,
                        Difference = difference
                    });
                }
            }

            return paymentsUnmatched.OrderByDescending(r => Math.Abs(r.Difference)).ToList();
        }
    }
}
