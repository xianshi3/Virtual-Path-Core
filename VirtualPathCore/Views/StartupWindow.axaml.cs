using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using VirtualPathCore.Views;

namespace VirtualPathCore
{
    /// <summary>
    /// ���������࣬������ʾ�������沢������ҳ�档
    /// </summary>
    public partial class StartupWindow : Window
    {
        /// <summary>
        /// ��ʼ���������ڲ���󻯡�
        /// </summary>
        public StartupWindow()
        {
            InitializeComponent();
            LoadMainPageAsync();
            this.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// �첽������ҳ�沢�ӳ���������
        /// </summary>
        private async void LoadMainPageAsync()
        {
            await Task.Delay(500);
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// �����ڴ�С�仯�¼�������Logo���ı��Ĵ�С��λ�á�
        /// </summary>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // ����LogoͼƬ
            LogoImage.Width = e.NewSize.Width * 0.375;
            LogoImage.Height = e.NewSize.Height * 0.333;
            LogoImage.Margin = new Thickness(0, e.NewSize.Height * 0.185, 0, 0);

            // ������ӭ�ı�
            WelcomeText.Margin = new Thickness(0, e.NewSize.Height * 0.033, 0, 0);
            WelcomeText.FontSize = e.NewSize.Height * 0.04;

            // �����汾��Ϣ
            VersionText.Margin = new Thickness(0, e.NewSize.Height * 0.033, 0, e.NewSize.Height * 0.033);
            VersionText.FontSize = e.NewSize.Height * 0.023;
        }
    }
}
