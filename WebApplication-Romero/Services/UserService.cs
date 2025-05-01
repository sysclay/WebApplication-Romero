using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Text.Json;
using WebApplication_Romero.Context;
using WebApplication_Romero.Models;

namespace WebApplication_Romero.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public UserService(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse> ImportUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
                if (!response.IsSuccessStatusCode) return new ServiceResponse { Success = false, Message = "Error al importar usuario." };
  
                var json = await response.Content.ReadAsStringAsync();
                var externalUsers = JsonSerializer.Deserialize<List<ExternalUser>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                Console.WriteLine($"external: {externalUsers?.Count}");
                if (externalUsers == null || externalUsers.Count == 0) return new ServiceResponse { Success = false, Message = "Lista usuario vacia" };

                var existingIds = _context.Users.Select(u => u.correo).ToHashSet();
                var usuarios = externalUsers
                    .Where(x=>!existingIds.Contains(x.email))
                    .Select(x => new User
                    {
                        nombre = x.name,
                        telefono = x.phone,
                        correo = x.email,
                        nombre_company = x.company.name,
                        calle = x.address.street,
                        latitud = x.address.geo.lat,
                        longitud = x.address.geo.lng,
                    }).ToList();
                foreach (var usuario in usuarios)
                {
                    Console.WriteLine($" COUNT :: {usuarios.Count}");
                    Console.WriteLine($" LISTA :: ID: {usuario.UserId}, Nombre: {usuario.nombre}, Email: {usuario.correo}");
                }
                if (usuarios.Any()){
                    _context.Users.AddRange(usuarios!);
                    await _context.SaveChangesAsync();
                    //var usuariosGuardados = _context.Users.ToList();
                    return new ServiceResponse { Success= true,Message= "Usuarios importados correctamente." };
                }
                return new ServiceResponse { Success = true, Message = "Usuarios ya existen." };

            } catch(Exception ex) {
                return new ServiceResponse { Success = false, Message = "Error al importar usuarios.", ErrorDetails = ex.Message };
            }
        }
    }
}
