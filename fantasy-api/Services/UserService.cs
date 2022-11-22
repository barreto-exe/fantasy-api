using Core.Utils.Mapping;
using FantasyApi.Data.Auth.Inputs;
using FantasyApi.Data.Base.Dtos;
using FantasyApi.Data.Base.Requests;
using FantasyApi.Data.Users.Dtos;
using FantasyApi.Data.Users.Exceptions;
using FantasyApi.Data.Users.Inputs;
using FantasyApi.Interfaces;
using FantasyApi.Utils;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyApi.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseService _baseDatabaseService;
        public UserService(IDatabaseService baseDatabaseService)
        {
            _baseDatabaseService = baseDatabaseService;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("idUser", id),
            };

            var cmd = _baseDatabaseService.GetCommand("GetUserById", parameters);
            var data = await _baseDatabaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data.Rows[0]);

                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var cmd = _baseDatabaseService.GetCommand("GetUsers");
            var data = await _baseDatabaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data);

                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<PaginatedListDto<UserDto>> GetAllUsersPaginatedAsync(BaseRequest filter)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("page", filter.Page),
                new MySqlParameter("size", filter.Size),
            };

            var cmd = _baseDatabaseService.GetCommand("GetUsersPaginated", parameters);
            var data = await _baseDatabaseService.ExecuteStoredProcedureDataSetAsync(cmd);

            if (data.Tables[0].Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var users = mapper.Map(data.Tables[0]);

                int totalRows = (int)data.Tables[1].Rows[0]["totalRows"];
                int page = (int)data.Tables[1].Rows[0]["page"];
                int totalPages = (int)data.Tables[1].Rows[0]["totalPages"];
                int size = (int)data.Tables[1].Rows[0]["size"];

                return ResponsesBuilder.PaginatedListResponse(users, totalRows, page, totalPages, size);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDto> GetUserByMailAndPassAsync(LoginInput input)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("user_email", input.Email),
                new MySqlParameter("user_pass", input.Password),
            };

            var cmd = _baseDatabaseService.GetCommand("GetUserByMailAndPass", parameters);
            var data = await _baseDatabaseService.ExecuteStoredProcedureAsync(cmd);

            if (data.Rows.Count > 0)
            {
                var mapper = new DataNamesMapper<UserDto>();
                var user = mapper.Map(data.Rows[0]);

                return user;
            }
            else
            {
                return null;
            }
        }

        /// <exception cref="UserExistsException"></exception>
        public async Task<UserDto> AddUserAsync(UserAddInput input)
        {
            //Validate if email has already been used
            var users = await GetAllUsersAsync();
            bool userExists = (from u in users
                               where u.Email == input.Email
                               select u).ToList().Count > 0;
            if (userExists)
            {
                throw new UserExistsException();
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("user_name", input.Name),
                new MySqlParameter("user_role", input.Role),
                new MySqlParameter("user_birth_date", input.BirthDate),
                new MySqlParameter("user_email", input.Email),
                new MySqlParameter("user_pass", input.Password),
            };

            var cmd = _baseDatabaseService.GetCommand("AddUser", parameters);
            await _baseDatabaseService.ExecuteStoredProcedureAsync(cmd);

            return await GetUserByMailAndPassAsync(new LoginInput()
            {
                Email = input.Email,
                Password = input.Password,
            });
        }

        /// <exception cref="UserDoesntExistException"></exception>
        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                throw new UserDoesntExistException();
            }

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter("idUser", id),
            };

            var cmd = _baseDatabaseService.GetCommand("DeleteUser", parameters);
            await _baseDatabaseService.ExecuteStoredProcedureAsync(cmd);
        }

    }
}
