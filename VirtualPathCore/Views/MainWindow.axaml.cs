using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using VirtualPathCore.ViewModels;

namespace VirtualPathCore.Views
{
    public partial class MainWindow : Window
    {
        private bool isSidebarOnLeft = true; // ������Ƿ�������״̬��־

        /// <summary>
        /// ��ʼ�������ڣ����ô��ڵĳ�ʼ״̬���������������ġ�
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized; // ���ô���Ĭ�����
            DataContext = new MainWindowViewModel(); // ��������������ΪMainWindowViewModel
        }

        /// <summary>
        /// �л��������AI�Ի���Ŀɼ��ԡ�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        /// <param name="e">�¼�������</param>
        private void ToggleSidebar(object sender, RoutedEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                bool isVisible = SidebarBorder.IsVisible; // ��ȡ��ǰ������Ŀɼ���
                SidebarBorder.IsVisible = !isVisible; // �л�������Ŀɼ���
                AIChatBorder.IsVisible = !isVisible;   // ͬʱ�л��Ҳ� AI �Ի���Ŀɼ���

                // ��������������п��
                if (SidebarBorder.IsVisible)
                {
                    Grid.SetColumnSpan(ContentBorder, 1);
                    Grid.SetColumn(ContentBorder, 1); // ȷ�������������м���
                }
                else
                {
                    Grid.SetColumnSpan(ContentBorder, 3);
                    Grid.SetColumn(ContentBorder, 0); // �������������࿪ʼ
                }
            });
        }

        /// <summary>
        /// �л��������AI�Ի����λ�á�
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        /// <param name="e">�¼�������</param>
        private void ToggleDock(object sender, RoutedEventArgs e)
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (isSidebarOnLeft)
                {
                    SetDockPosition(2, 0); // �����Ĳ�����л����Ҳ�
                    SetDockPosition(0, 2); // ���Ҳ��AI�Ի����л������
                }
                else
                {
                    SetDockPosition(0, 2); // �����Ĳ�����л������
                    SetDockPosition(2, 0); // ���Ҳ��AI�Ի����л����Ҳ�
                }
                isSidebarOnLeft = !isSidebarOnLeft; // ����״̬��־
            });
        }

        /// <summary>
        /// ���ò������AI�Ի����λ�á�
        /// </summary>
        /// <param name="sidebarColumn">�������Ŀ����������</param>
        /// <param name="aiChatColumn">AI �Ի����Ŀ����������</param>
        private void SetDockPosition(int sidebarColumn, int aiChatColumn)
        {
            Grid.SetColumn(SidebarBorder, sidebarColumn); // ���ò��������
            Grid.SetColumn(AIChatBorder, aiChatColumn);   // ����AI�Ի������
        }

        /// <summary>
        /// ����ģ��ѡ����ѡ��仯�¼���
        /// </summary>
        /// <param name="sender">�����¼��Ķ���</param>
        /// <param name="e">�¼�������</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem != null)
            {
                // ȷ���ⲿ����UI�߳�
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    // TODO: ����ѡ�е�ģ��ִ����Ӧ�Ĳ���
                });
            }
        }
    }
}
