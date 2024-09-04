using AMMAAPI.Database;
using AMMAAPI.Helpers;
using AMMAAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text;

namespace AMMAAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IOptions<AMMADatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _user = database.GetCollection<User>(settings.Value.CollectionName.User);
        }

        public async Task<List<User>> GetAsync() =>
            await _user.Find(_=> true).ToListAsync();

        public async Task<User> GetByIdAsync (string id)
        {
               var u = Builders<User>.Filter.Eq("UserId", id);
            return await _user.Find(u).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User u)
        {
            if (u.Password != null)
            {
                u.Password = PasswordHasher.HashPassword(u.Password);
            }

            await _user.InsertOneAsync(u);
        }

        public async Task UpdateAsync(User uIn)
        {
            var u = Builders<User>.Filter.Eq("UserId", uIn.UserId);
            await _user.ReplaceOneAsync(u, uIn);
        }

        public async Task RemoveAsync(User uIn) =>
            await _user.DeleteOneAsync(u => u.UserId == uIn.UserId);

        public async Task<User> Authenticate(string email, string password)
        {
            var u = await _user.Find(u => u.Email == email).FirstOrDefaultAsync();

            // email is incorrect
            if (u == null)
            {
                return null;
            }

            if (!PasswordHasher.VerifyPassword(password, u.Password))
            {
                u.Password = null;
                return u;
            }

            return u;
        }

        public async Task<bool> CheckUserNameExist(string email)
            => await _user.Find(u => u.Email == email).FirstOrDefaultAsync() != null;

        public string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 6)
                sb.Append("Password must be at least 6 characters long" + Environment.NewLine);
            if (password.Length > 10)
                sb.Append("Password must be no more than 10 characters" + Environment.NewLine);
            if (!password.Any(char.IsLower))
                sb.Append("Password must contain at least one lowercase letter" + Environment.NewLine);
            if (!password.Any(char.IsDigit))
                sb.Append("Password must contain at least one digit" + Environment.NewLine);
            if (!password.Any(char.IsSymbol))
                sb.Append("Password must contain at least one special character" + Environment.NewLine);

            return sb.ToString();
        }

         public async Task UpdateLastActiveAsync(string userId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
        var update = Builders<User>.Update.Set(u => u.LastActive, DateTime.UtcNow);

        await _user.UpdateOneAsync(filter, update);
    }

        internal void Remove(Task<User> u)
        {
            throw new NotImplementedException();
        }

        internal async Task RemoveAsync(Task<User> u)
        {
            throw new NotImplementedException();
        }
    }
}
