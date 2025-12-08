using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendApi.Extensions
{
    public static class ControllerExtensions
    {
        public static ActionResult? GetUserIdOrError(this ControllerBase controller, out int userId)
        {
            userId = 0;
            var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId))
            {
                return controller.Unauthorized("Token inválido ou ID de usuário não encontrado.");
            }

            return null;
        }
    }
}