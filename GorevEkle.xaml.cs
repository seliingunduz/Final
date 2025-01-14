using System;

namespace Final
{
    public partial class GorevEkle : ContentPage
    {
        private readonly FirebaseHelper firebaseHelper = new FirebaseHelper();
        private readonly string userId;
        private readonly TaskItem existingTask;
        private readonly bool isEditing;

        public GorevEkle(string userId, TaskItem taskToEdit = null)
        {
            InitializeComponent();
            this.userId = userId;

            if (taskToEdit != null)
            {
                isEditing = true;
                existingTask = taskToEdit;

                // Mevcut görev bilgilerini doldur
                TaskEntry.Text = existingTask.Name;
                DetailEntry.Text = existingTask.Details;
                DatePicker.Date = existingTask.Date;
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskEntry.Text) || string.IsNullOrWhiteSpace(DetailEntry.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanlarý doldurun.", "Tamam");
                return;
            }

            try
            {
                if (isEditing)
                {
                    // Görevi güncelle
                    existingTask.Name = TaskEntry.Text.Trim();
                    existingTask.Details = DetailEntry.Text.Trim();
                    existingTask.Date = DatePicker.Date;

                    await firebaseHelper.UpdateTaskAsync(userId, existingTask.Id, existingTask);
                    await DisplayAlert("Baþarýlý", "Görev güncellendi.", "Tamam");
                }
                else
                {
                    // Yeni görev ekle
                    var newTask = new TaskItem
                    {
                        Name = TaskEntry.Text.Trim(),
                        Details = DetailEntry.Text.Trim(),
                        Date = DatePicker.Date
                    };

                    await firebaseHelper.AddTaskAsync(userId, newTask);
                    await DisplayAlert("Baþarýlý", "Görev eklendi.", "Tamam");
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Görev eklenirken hata oluþtu: {ex.Message}");
                await DisplayAlert("Hata", $"Görev eklenemedi: {ex.Message}", "Tamam");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
