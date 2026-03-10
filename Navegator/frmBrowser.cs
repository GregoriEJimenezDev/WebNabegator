using EasyTabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navegator
{
    public partial class frmBrowser : Form
    {
        private WebBrowser webBrowser;
        private TextBox txtUrl;
        private Button btnBack;
        private Button btnForward;
        private Button btnRefresh;
        private Button btnGo;
        private Button btnHome;
        private Panel pnlNavigation;
        private ProgressBar progressBar;

        public frmBrowser()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== INICIO Constructor frmBrowser ===");

                InitializeComponent();
                System.Diagnostics.Debug.WriteLine("InitializeComponent() completado");

                InitializeBrowserComponents();
                System.Diagnostics.Debug.WriteLine("InitializeBrowserComponents() completado");

                this.Load += FrmBrowser_Load;

                System.Diagnostics.Debug.WriteLine("=== FIN Constructor frmBrowser ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR en Constructor:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}", 
                    "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"EXCEPCIÓN en Constructor: {ex}");
            }
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== INICIO FrmBrowser_Load ===");

                // Forzar que el panel de navegación esté al frente cuando se carga el formulario
                if (pnlNavigation != null)
                {
                    pnlNavigation.BringToFront();
                    System.Diagnostics.Debug.WriteLine("pnlNavigation.BringToFront() ejecutado");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("WARNING: pnlNavigation es NULL");
                }

                // Verificar controles
                System.Diagnostics.Debug.WriteLine($"Total controles en Form: {this.Controls.Count}");
                System.Diagnostics.Debug.WriteLine($"Total controles en Panel: {pnlNavigation?.Controls.Count ?? 0}");

                System.Diagnostics.Debug.WriteLine("=== FIN FrmBrowser_Load ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR en Load:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}", 
                    "Error en Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"EXCEPCIÓN en Load: {ex}");
            }
        }

        private void InitializeBrowserComponents()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== INICIO InitializeBrowserComponents ===");

                // Panel de navegación superior
                System.Diagnostics.Debug.WriteLine("Creando pnlNavigation...");
                pnlNavigation = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 50,
                    BackColor = Color.FromArgb(240, 240, 240)
                };
                this.Controls.Add(pnlNavigation);
                System.Diagnostics.Debug.WriteLine($"pnlNavigation agregado. Color: {pnlNavigation.BackColor}");

                // Botón Atrás
                System.Diagnostics.Debug.WriteLine("Creando btnBack...");
                btnBack = new Button
                {
                    Text = "←",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Size = new Size(40, 35),
                    Location = new Point(5, 8),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(250, 250, 250)
                };
                btnBack.FlatAppearance.BorderSize = 0;
                btnBack.Click += BtnBack_Click;
                pnlNavigation.Controls.Add(btnBack);

                // Botón Adelante
                btnForward = new Button
                {
                    Text = "→",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Size = new Size(40, 35),
                    Location = new Point(50, 8),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(250, 250, 250)
                };
                btnForward.FlatAppearance.BorderSize = 0;
                btnForward.Click += BtnForward_Click;
                pnlNavigation.Controls.Add(btnForward);

                // Botón Refrescar
                btnRefresh = new Button
                {
                    Text = "⟳",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Size = new Size(40, 35),
                    Location = new Point(95, 8),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(250, 250, 250)
                };
                btnRefresh.FlatAppearance.BorderSize = 0;
                btnRefresh.Click += BtnRefresh_Click;
                pnlNavigation.Controls.Add(btnRefresh);

                // Botón Home
                btnHome = new Button
                {
                    Text = "⌂",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Size = new Size(40, 35),
                    Location = new Point(140, 8),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(250, 250, 250)
                };
                btnHome.FlatAppearance.BorderSize = 0;
                btnHome.Click += BtnHome_Click;
                pnlNavigation.Controls.Add(btnHome);
                System.Diagnostics.Debug.WriteLine("Botones de navegación creados");

                // Barra de direcciones
                System.Diagnostics.Debug.WriteLine("Creando txtUrl...");
                txtUrl = new TextBox
                {
                    Location = new Point(190, 12),
                    Width = this.Width - 280,
                    Height = 30,
                    Font = new Font("Segoe UI", 11),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                txtUrl.KeyDown += TxtUrl_KeyDown;
                pnlNavigation.Controls.Add(txtUrl);

                // Botón Ir
                btnGo = new Button
                {
                    Text = "→",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    Size = new Size(40, 30),
                    Location = new Point(this.Width - 80, 10),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(66, 133, 244),
                    ForeColor = Color.White,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                btnGo.FlatAppearance.BorderSize = 0;
                btnGo.Click += BtnGo_Click;
                pnlNavigation.Controls.Add(btnGo);
                System.Diagnostics.Debug.WriteLine("Controles de URL creados");

                // Barra de progreso
                progressBar = new ProgressBar
                {
                    Dock = DockStyle.Bottom,
                    Height = 3,
                    Style = ProgressBarStyle.Continuous,
                    Visible = false
                };
                pnlNavigation.Controls.Add(progressBar);

                // WebBrowser (temporal hasta instalar WebView2)
                System.Diagnostics.Debug.WriteLine("Creando WebBrowser...");
                webBrowser = new WebBrowser
                {
                    Dock = DockStyle.Fill,
                    ScriptErrorsSuppressed = true
                };
                webBrowser.Navigating += WebBrowser_Navigating;
                webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
                webBrowser.DocumentTitleChanged += WebBrowser_DocumentTitleChanged;
                this.Controls.Add(webBrowser);
                System.Diagnostics.Debug.WriteLine("WebBrowser agregado al formulario");

                // Asegurar que el panel de navegación esté encima
                pnlNavigation.BringToFront();
                System.Diagnostics.Debug.WriteLine("pnlNavigation.BringToFront() ejecutado en Init");

                // Navegar a página inicial
                System.Diagnostics.Debug.WriteLine("Navegando a Google...");
                webBrowser.Navigate("https://www.google.com");

                System.Diagnostics.Debug.WriteLine("=== FIN InitializeBrowserComponents ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR en InitializeBrowserComponents:\n{ex.Message}\n\nStackTrace:\n{ex.StackTrace}", 
                    "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Diagnostics.Debug.WriteLine($"EXCEPCIÓN en InitializeBrowserComponents: {ex}");
                throw; // Re-lanzar para que se capture en el constructor
            }
        }

        // Eventos de navegación
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
                webBrowser.GoBack();
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoForward)
                webBrowser.GoForward();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("https://www.google.com");
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            NavigateToUrl(txtUrl.Text);
        }

        private void TxtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NavigateToUrl(txtUrl.Text);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void NavigateToUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            // Si no tiene protocolo, agregar https://
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                // Si parece una búsqueda, usar Google
                if (!url.Contains(".") || url.Contains(" "))
                {
                    url = "https://www.google.com/search?q=" + Uri.EscapeDataString(url);
                }
                else
                {
                    url = "https://" + url;
                }
            }

            webBrowser.Navigate(url);
        }

        // Eventos de WebBrowser
        private void WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            btnBack.Enabled = webBrowser.CanGoBack;
            btnForward.Enabled = webBrowser.CanGoForward;
            txtUrl.Text = e.Url.ToString();
        }

        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            progressBar.Visible = false;
            progressBar.Value = 100;
            btnBack.Enabled = webBrowser.CanGoBack;
            btnForward.Enabled = webBrowser.CanGoForward;
            txtUrl.Text = webBrowser.Url.ToString();
        }

        private void WebBrowser_DocumentTitleChanged(object sender, EventArgs e)
        {
            string title = webBrowser.DocumentTitle;
            if (!string.IsNullOrEmpty(title))
            {
                this.Text = title;
            }
        }
    }
}
