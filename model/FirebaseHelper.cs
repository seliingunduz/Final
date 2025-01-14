using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final
{
    public class FirebaseHelper
    {
        private FirebaseClient firebaseClient;

        public FirebaseHelper()
        {
            // Firebase Realtime Database bağlantısı
            firebaseClient = new FirebaseClient("https://final-a773b-default-rtdb.firebaseio.com/");
        }

        // Görevleri kullanıcıya göre al
        public async Task<List<TaskItem>> GetTasksAsync(string userId)
        {
            try
            {
                var tasks = await firebaseClient
                    .Child("tasks")
                    .Child(userId)
                    .OnceAsync<TaskItem>();

                var taskList = new List<TaskItem>();
                foreach (var task in tasks)
                {
                    var taskItem = task.Object;
                    taskItem.Id = task.Key; // Firebase ID'yi modelde sakla
                    taskList.Add(taskItem);
                }
                return taskList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks: {ex.Message}");
                throw;
            }
        }

        // Görev ekle
        public async Task AddTaskAsync(string userId, TaskItem task)
        {
            try
            {
                var result = await firebaseClient
                    .Child("tasks")
                    .Child(userId)
                    .PostAsync(task);

                task.Id = result.Key; // Firebase tarafından oluşturulan ID'yi al
                Console.WriteLine($"Görev eklendi: {task.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Görev eklenirken hata oluştu: {ex.Message}");
                throw;
            }
        }

        // Görev sil
        public async Task DeleteTaskAsync(string userId, string taskId)
        {
            try
            {
                await firebaseClient
                    .Child("tasks")
                    .Child(userId)
                    .Child(taskId) // Benzersiz taskId kullanımı
                    .DeleteAsync();
                Console.WriteLine($"Görev silindi: {taskId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Silme işlemi başarısız: {ex.Message}");
                throw;
            }
        }

        // Görev güncelle
        public async Task UpdateTaskAsync(string userId, string taskId, TaskItem updatedTask)
        {
            try
            {
                await firebaseClient
                    .Child("tasks")
                    .Child(userId)
                    .Child(taskId)
                    .PutAsync(updatedTask);
                Console.WriteLine($"Görev güncellendi: {taskId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Görev güncellenirken hata oluştu: {ex.Message}");
                throw;
            }
        }
    }
}
