using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaptchaGeneratorApp
{
    public partial class MainForm : Form
    {
        private string currentCaptchaText = "";
        private string captchaType = "";
        private Random rand = new Random();

        private Label lblCaptcha;
        private TextBox txtInput;
        private Button btnSubmit;
        private Button btnRefresh;
        private CheckBox chkCaptcha;

        public MainForm()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Name = "MainForm";
            this.ResumeLayout(false);
        }

        private void InitUI()
        {
            this.Text = "CAPTCHA Generator";
            this.Size = new Size(600, 300);

            Button btnTextCaptcha = new Button() { Text = "Text CAPTCHA", Location = new Point(30, 30), Width = 120 };
            Button btnImageCaptcha = new Button() { Text = "Image CAPTCHA", Location = new Point(160, 30), Width = 120 };
            Button btnMathCaptcha = new Button() { Text = "Math CAPTCHA", Location = new Point(290, 30), Width = 120 };
            Button btnCheckboxCaptcha = new Button() { Text = "I'm not a robot", Location = new Point(420, 30), Width = 120 };

            lblCaptcha = new Label() { Location = new Point(30, 80), Size = new Size(400, 30), Font = new Font("Arial", 12) };
            txtInput = new TextBox() { Location = new Point(30, 120), Width = 200 };
            btnSubmit = new Button() { Text = "Submit", Location = new Point(250, 120) };
            btnRefresh = new Button() { Text = "Refresh", Location = new Point(350, 120) };
            chkCaptcha = new CheckBox() { Text = "I'm not a robot", Location = new Point(30, 160), AutoSize = true, Visible = false };

            btnTextCaptcha.Click += GenerateTextCaptcha;
            btnImageCaptcha.Click += GenerateImageCaptcha;
            btnMathCaptcha.Click += GenerateMathCaptcha;
            btnCheckboxCaptcha.Click += GenerateCheckboxCaptcha;
            btnSubmit.Click += ValidateCaptcha;
            btnRefresh.Click += RefreshCaptcha;

            this.Controls.Add(btnTextCaptcha);
            this.Controls.Add(btnImageCaptcha);
            this.Controls.Add(btnMathCaptcha);
            this.Controls.Add(btnCheckboxCaptcha);
            this.Controls.Add(lblCaptcha);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(chkCaptcha);
        }

        private void GenerateTextCaptcha(object sender, EventArgs e)
        {
            captchaType = "text";
            currentCaptchaText = GenerateRandomText();
            lblCaptcha.Text = $"Enter the text: {currentCaptchaText}";
            txtInput.Visible = true;
            txtInput.Text = "";
            chkCaptcha.Visible = false;
        }

        private void GenerateImageCaptcha(object sender, EventArgs e)
        {
            captchaType = "image";
            currentCaptchaText = "car";
            lblCaptcha.Text = "Select all images that contain cars. (Simulated)";
            txtInput.Visible = true;
            txtInput.Text = "";
            chkCaptcha.Visible = false;
        }

        private void GenerateMathCaptcha(object sender, EventArgs e)
        {
            captchaType = "math";
            int a = rand.Next(1, 10);
            int b = rand.Next(1, 10);
            currentCaptchaText = (a + b).ToString();
            lblCaptcha.Text = $"What is {a} + {b}?";
            txtInput.Visible = true;
            txtInput.Text = "";
            chkCaptcha.Visible = false;
        }

        private void GenerateCheckboxCaptcha(object sender, EventArgs e)
        {
            captchaType = "checkbox";
            lblCaptcha.Text = "Please check the box below:";
            chkCaptcha.Checked = false;
            chkCaptcha.Visible = true;
            txtInput.Visible = false;
        }

        private void ValidateCaptcha(object sender, EventArgs e)
        {
            bool valid = false;
            if (captchaType == "checkbox")
            {
                valid = chkCaptcha.Checked;
            }
            else
            {
                valid = txtInput.Text.Trim().Equals(currentCaptchaText);
            }

            MessageBox.Show(valid ? "Success!" : "Incorrect, try again.", "Validation Result");
        }

        private void RefreshCaptcha(object sender, EventArgs e)
        {
            switch (captchaType)
            {
                case "text": GenerateTextCaptcha(null, null); break;
                case "image": GenerateImageCaptcha(null, null); break;
                case "math": GenerateMathCaptcha(null, null); break;
                case "checkbox": GenerateCheckboxCaptcha(null, null); break;
            }
        }

        private string GenerateRandomText()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] stringChars = new char[6];
            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[rand.Next(chars.Length)];
            return new string(stringChars);
        }
    }
}
