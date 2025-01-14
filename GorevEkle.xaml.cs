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

                // Mevcut g�rev bilgilerini doldur
                TaskEntry.Text = existingTask.Name;
                DetailEntry.Text = existingTask.Details;
                DatePicker.Date = existingTask.Date;
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TaskEntry.Text) || string.IsNullOrWhiteSpace(DetailEntry.Text))
            {
                await DisplayAlert("Hata", "L�tfen t�m alanlar� doldurun.", "Tamam");
                return;
            }

            try
            {
                if (isEditing)
                {
                    // G�revi g�ncelle
                    existingTask.Name = TaskEntry.Text.Trim();
                    existingTask.Details = DetailEntry.Text.Trim();
                    existingTask.Date = DatePicker.Date;

                    await firebaseHelper.UpdateTaskAsync(userId, existingTask.Id, existingTask);
                    await DisplayAlert("Ba�ar�l�", "G�rev g�ncellendi.", "Tamam");
                }
                else
                {
                    // Yeni g�rev ekle
                    var newTask = new TaskItem
                    {
                        Name = TaskEntry.Text.Trim(),
                        Details = DetailEntry.Text.Trim(),
                        Date = DatePicker.Date
                    };

                    await firebaseHelper.AddTaskAsync(userId, newTask);
                    await DisplayAlert("Ba�ar�l�", "G�rev eklendi.", "Tamam");
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"G�rev eklenirken hata olu�tu: {ex.Message}");
                await DisplayAlert("Hata", $"G�rev eklenemedi: {ex.Message}", "Tamam");
            }
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
