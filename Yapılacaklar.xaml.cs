
using System;
using System.Collections.ObjectModel;

namespace Final
{
    public partial class Yapılacaklar : ContentPage
    {
        private readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        private readonly ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
        private readonly string userId = "user123"; // Örnek kullanıcı ID

        public Yapılacaklar()
        {
            InitializeComponent();
            TaskCollectionView.ItemsSource = tasks;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            tasks.Clear();

            // Firebase'den görevleri al ve listeye ekle
            var firebaseTasks = await firebaseHelper.GetTasksAsync(userId);
            foreach (var task in firebaseTasks)
            {
                tasks.Add(task);
            }
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GorevEkle(userId));
        }

        private async void OnEditTask(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is TaskItem task)
            {
                await Navigation.PushAsync(new GorevEkle(userId, task));
            }
        }

        private async void OnDeleteTask(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is TaskItem task)
            {
                var confirm = await DisplayAlert("Görev Sil", "Bu görevi silmek istediğinize emin misiniz?", "Evet", "Hayır");
                if (confirm)
                {
                    try
                    {
                        tasks.Remove(task); // Listeden kaldır
                        await firebaseHelper.DeleteTaskAsync(userId, task.Id); // Firebase'den sil
                        await DisplayAlert("Başarılı", "Görev silindi.", "Tamam");
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Hata", $"Görev silinemedi: {ex.Message}", "Tamam");
                    }
                }
            }
        }
    }
}
