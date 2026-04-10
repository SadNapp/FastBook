using FastBook.Interfaces;
using FastBook.Services;
using FastBook.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace FastBook.Views
{
    public partial class NoteWindow : Window
    {
        public NoteViewModel ViewModel { get; } = ((App)Application.Current).ServiceProvider.GetRequiredService<NoteViewModel>();

       
        private readonly ClockService _clockService;
        private readonly TextService _textService;
        private DispatcherTimer? _timer;
       

        public NoteWindow()
        {
            InitializeComponent();
            ViewModel = App.CurrentApp.ServiceProvider.GetRequiredService<NoteViewModel>();
            this.DataContext = ViewModel;

           
            


            _clockService = new ClockService(); 
            _textService = new TextService();   

            SetupApp();
        }

        private void SetupApp()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => { if (ClockTextBlock != null) ClockTextBlock.Text = _clockService.GetFormattedTime(); };
            _timer.Start();

            NoteTextBox.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(NoteTextBox_ScrollChanged));
            _ = LoadInitialData();
        }

        private async System.Threading.Tasks.Task LoadInitialData()
        {
            await ViewModel.LoadNoteAsync("Default");
            UpdatePriorityIndicator();
        }

        private void UpdatePriorityIndicator()
        {
            if (ViewModel.CurrentNote == null || PriorityEllipse == null) return;

            switch (ViewModel.CurrentNote.Priority)
            {
                case 1: PriorityEllipse.Fill = Brushes.Green; break;
                case 2: PriorityEllipse.Fill = Brushes.Yellow; break;
                case 3: PriorityEllipse.Fill = Brushes.Red; break;
                default: PriorityEllipse.Fill = Brushes.Gray; break;
            }
        }

        private async void NoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LineNumbersBlock != null) LineNumbersBlock.Text = _textService.GetLineNumbers(NoteTextBox.Text);
            

            if (ViewModel.CurrentNote != null)
            {
                ViewModel.CurrentNote.Content = NoteTextBox.Text;
                await ViewModel.SaveCurrentNoteAsync();
            }
        }

        private void NoteTextBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (LineNumbersBlock != null) LineNumbersBlock.Margin = new Thickness(0, -e.VerticalOffset, 0, 0);
        }

        private async void CategoryTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl && tabControl.SelectedItem is TabItem selectedTab)
            {
                
                string category = selectedTab.Header?.ToString() ?? "Default";

                await ViewModel.LoadNoteAsync(category);
                UpdatePriorityIndicator();
            }
        }

        private async void Priority_Changed(object sender, SelectionChangedEventArgs e)
        {
            
            if (ViewModel.CurrentNote != null && PriorityComboBox.SelectedItem is ComboBoxItem item)
            {
                
                string tagValue = item.Tag?.ToString() ?? "1";

                
                if (int.TryParse(tagValue, out int priority))
                {
                    ViewModel.CurrentNote.Priority = priority;
                    await ViewModel.SaveCurrentNoteAsync();
                    UpdatePriorityIndicator();
                }
            }
        }

        // --- UI Management ---
        private void HeaderBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) DragMove(); }
        private void CloseButton_Click(object sender, RoutedEventArgs e) {Close(); }
        private void Settings_Click(object sender, RoutedEventArgs e) { if (SettingsPopup != null) SettingsPopup.IsOpen = !SettingsPopup.IsOpen; }
        private void FontSize_Changed(object sender, RoutedPropertyChangedEventArgs<double> e) { if (NoteTextBox != null) NoteTextBox.FontSize = e.NewValue; }
        private void Pin_Checked(object sender, RoutedEventArgs e) => Topmost = true;
        private void Pin_Unchecked(object sender, RoutedEventArgs e) => Topmost = false;
        private void Time_Checked(object sender, RoutedEventArgs e) { if (ClockTextBlock != null) ClockTextBlock.Visibility = Visibility.Visible; }
        private void Time_Unchecked(object sender, RoutedEventArgs e) { if (ClockTextBlock != null) ClockTextBlock.Visibility = Visibility.Collapsed; }
    }
}